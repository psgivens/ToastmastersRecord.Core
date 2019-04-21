
open ToastmastersRecord.SampleApp.Initialize
open ToastmastersRecord.SampleApp.AggregateInformation

open System
open Akka.FSharp

open ToastmastersRecord.Data
open ToastmastersRecord.Domain

open ToastmastersRecord.SampleApp.IngestMembers
open ToastmastersRecord.SampleApp.IngestMeetings
open ToastmastersRecord.SampleApp.IngestMessages
[<EntryPoint>]
let main argv =

    // System set up
    NewtonsoftHack.resolveNewtonsoft ()  
    ToastmastersEFDbInitializer.Initialize ()  
    let system = Configuration.defaultConfig () |> System.create "sample-system"
            
    let actorGroups = composeActors system
    
    // Sample data
    let userId = Persistence.Users.findUserId "ToastmastersRecord.SampleApp.Initialize" 
    
    let clubRosterFile = "/home/psgivens/Downloads/tm/Toastmasters/Club-Roster20171126.csv"
    ingestMembers system userId actorGroups clubRosterFile
    actorGroups |> ingestSpeechCount system userId "/home/psgivens/Downloads/tm/Toastmasters/ConfirmedSpeechCount.csv"

    actorGroups |> ingestMemberMessages system userId 
    actorGroups |> ingestMeetings system userId
    actorGroups |> ingestHistory system userId
    actorGroups |> generateMessagesToMembers system userId
    actorGroups |> ingestDaysOff system userId
    actorGroups |> ingestRoleRequests system userId

    let date = "12/12/2017" |> DateTime.Parse
    actorGroups |> calculateHistory system userId date 
    actorGroups |> generateStatistics system userId

    printfn "Press enter to continue"
    System.Console.ReadLine () |> ignore
    printfn "%A" argv
    0 // return an integer exit code
