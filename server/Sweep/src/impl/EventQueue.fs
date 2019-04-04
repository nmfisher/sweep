  // let processEvent event = 
  //   try
  //     let generated = StubbleBuilder().Build().Render(event.Content, event.Params)
  //     Mailer.Send(generated, event.)
  //   with
  //   | e ->
  //     ()

  // let dequeue = 
  //   let ctx = Sql.GetDataContext()
  //   let unprocessed = 
  //     query {      
  //       for event in ctx.SweepDevelopment.Event do
  //       join listener in ctx.SweepDevelopment.Listener on (listener.EventName = event.eventName && listener.OrganizationId = event.organizationId)
  //       where isNull event.ProcessedOn
  //       select (event)
  //     } 
  //     |> Seq.map (fun x -> x.MapTo<EventModel.Event>(deserializeEvent))
  //     |> Seq.toList

  //   unprocessed
  //   |> Seq.map processEvent 