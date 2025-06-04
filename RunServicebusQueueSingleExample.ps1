# Open the ServiceBusConsumer project
Start-Process cmd -ArgumentList '/k', 'dotnet run --project ./ServiceBusConsumer.csproj' -WorkingDirectory ./servicebus/Queue/ServiceBusConsumer

# Open the ServiceBusConsumer project
Start-Process cmd -ArgumentList '/k', 'dotnet run --project ./ServiceBusProducer.csproj' -WorkingDirectory ./servicebus/Queue/ServiceBusProducerSingle
