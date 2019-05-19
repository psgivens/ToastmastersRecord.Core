module ToastmastersRecord.SampleApp.Schedule.MemberManagement

open Akka.Actor
open Akka.FSharp

open Common.FSharp.Envelopes
open ToastmastersRecord.Domain.DomainTypes
open ToastmastersRecord.SampleApp.Initialize
open Common.FSharp.Actors

open ToastmastersRecord.Domain.MemberManagement

let createMember system userId (actorGroups:ActorGroups) name = 
    let memberRequestReply = RequestReplyActor.spawnRequestReplyActor<MemberManagementCommand,MemberManagementEvent> system "memberManagement" actorGroups.MemberManagementActors
    {   MemberDetails.ToastmasterId = -1 |> TMMemberId.box
        Name = name
        DisplayName = name
        Awards = ""
        Email= ""
        HomePhone = ""
        MobilePhone= ""
        PaidUntil = defaultDate
        ClubMemberSince = defaultDate
        OriginalJoinDate = defaultDate
        SpeechCountConfirmedDate = defaultDate
        PaidStatus = ""
        CurrentPosition = ""
        }
    |> MemberManagementCommand.Create
    |> envelopWithDefaults
        (userId)
        (TransId.create ())
        (StreamId.create ())
    |> memberRequestReply.Ask
    |> Async.AwaitTask
    |> Async.Ignore
    |> Async.RunSynchronously

    memberRequestReply <! "Unsubscribe"



