# Open multiple copies of the ServiceBusConsumer project

Start-Process cmd -ArgumentList '/k', 'dotnet run --project ./ServiceBusConsumer.csproj' -WorkingDirectory ./servicebus/Topic/ServiceBusConsumer
Start-Process cmd -ArgumentList '/k', 'dotnet run --project ./ServiceBusConsumer.csproj' -WorkingDirectory ./servicebus/Topic/ServiceBusConsumer
Start-Process cmd -ArgumentList '/k', 'dotnet run --project ./ServiceBusConsumer.csproj' -WorkingDirectory ./servicebus/Topic/ServiceBusConsumer2

# Open the ServiceBusConsumer project
Start-Process cmd -ArgumentList '/k', 'dotnet run --project ./ServiceBusProducer.csproj' -WorkingDirectory ./servicebus/Topic/ServiceBusProducerSingle
