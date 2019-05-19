﻿[<RequireQualifiedAccess>]
module ToastmastersRecord.SampleApp.RolePlacementActors

open ToastmastersRecord.Domain.DomainTypes
open ToastmastersRecord.Domain.RolePlacements
open Common.FSharp.Envelopes
open ToastmastersRecord.SampleApp.Initialize

open Akka.Actor
open Akka.FSharp

let spawnPlacementManager system userId (rolePlacmentRequestReply:IActorRef) =
    spawn system "rolePlacement_IngestManager" <| fun (mailbox:Actor<RoleTypeId * MeetingId * MemberId * RoleRequestId>) ->
        let placements =     
            use context = new ToastmastersRecord.Data.ToastmastersEFDbContext () 
            context.RolePlacements
            |> Seq.toList
        let rec loop (placements:ToastmastersRecord.Data.Entities.RolePlacementEntity list) = actor {
            let! roleTypeId, MeetingId.Id(meetingId), memberId, roleRequestId = mailbox.Receive ()
            let placement = placements |> List.find (fun p -> p.MeetingId = meetingId && p.RoleTypeId = int roleTypeId)

            (memberId, roleRequestId)
            |> RolePlacementCommand.Assign
            |> envelopWithDefaults
                (userId) 
                (TransId.create ()) 
                (StreamId.box placement.Id) 
            |> rolePlacmentRequestReply.Ask
            |> fun t -> mailbox.Sender () <! t.Result

            return! loop (placements |> List.where (fun p -> p <> placement))
        }        
        loop placements

let spawnRoleConfirmationReaction system (actorGroups:ActorGroups) = 
    (fun (mailbox:Actor<Envelope<RolePlacementEvent>>) cmdenv -> 
        match cmdenv.Item with
        | RolePlacementEvent.Assigned (mid, rid) -> 
            cmdenv 
            |> Envelope.reuseEnvelope cmdenv.StreamId cmdenv.Version (fun evt -> RolePlacementCommand.Complete)
            |> actorGroups.RolePlacementActors.Tell
        | _ -> ()) 
    |> actorOf2
    |> spawn system "RoleConfirmation" 
