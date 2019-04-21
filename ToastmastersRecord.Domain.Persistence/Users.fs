module ToastmastersRecord.Domain.Persistence.Users

open ToastmastersRecord.Data

let findUserId name =
    use context = new ToastmastersEFDbContext () 
    query { for user in context.Users do
            where (user.Name = name)
            select user.Id
            exactlyOneOrDefault }
    |> Common.FSharp.Envelopes.UserId.box

