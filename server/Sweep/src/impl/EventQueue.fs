// namespace Sweep

// open Sweep.Data

// module Mailer = 
//    let apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
//    let client = new SendGridClient(apiKey);
//    let from = new EmailAddress("test@example.com", "Example User");
            
//   let send content recipients =
    
//             var subject = "Sending with SendGrid is Fun";
//             var to = new EmailAddress("test@example.com", "Example User");
//             var plainTextContent = "and easy to do anywhere, even with C#";
//             var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
//             var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//             var response = await client.SendEmailAsync(msg); 


// module EventQueue =
//   let processEvent event = 
//     try
//       let generated = StubbleBuilder().Build().Render(event.Content, event.Params)
//       Mailer.Send(generated, event)
//     with
//     | e ->
//       ()

//   let dequeue = 
//     let ctx = Sql.GetDataContext()
//     let unprocessed = 
//       query {      
//         for event in ctx.SweepDevelopment.Event do
//         join listener in ctx.SweepDevelopment.Listener on (listener.EventName = event.eventName && listener.OrganizationId = event.organizationId)
//         where isNull event.ProcessedOn
//         select (event)
//       } 
//       |> Seq.map (fun x -> x.MapTo<Event.Event>(deserializeEvent))
//       |> Seq.toList

//     unprocessed
//     |> Seq.map processEvent 