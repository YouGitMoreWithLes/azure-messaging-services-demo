# BITCO Messaging Examples

This repository contains examples of different messaging patterns using Azure messaging services: Service Bus (Queues and Topics), Event Grid, and Event Hubs.

## Prerequisites

- PowerShell Core (pwsh.exe)
- .NET 8.0 SDK
- Azure subscription with appropriate permissions
- Environment variables set up for Azure Service Bus connection strings

## Available Scripts

### Service Bus Scripts

#### CreateServiceBus.ps1
```powershell
.\CreateServiceBus.ps1
```
Description: [Add description of what this script creates in Azure]

#### RunServicebusQueueSingleExample.ps1
```powershell
.\RunServicebusQueueSingleExample.ps1
```
Description: [Add description of the single message queue example]

#### RunServicebusQueueBatchExample.ps1
```powershell
.\RunServicebusQueueBatchExample.ps1
```
Description: [Add description of the batch message queue example]

#### RunServicebusQueueMultipleExample.ps1
```powershell
.\RunServicebusQueueMultipleExample.ps1
```
Description: [Add description of the multiple message queue example]

#### RunServicebusTopicExample.ps1
```powershell
.\RunServicebusTopicExample.ps1
```
Description: [Add description of the topic/subscription example]

## Environment Variables

Before running the scripts, make sure to set up the following environment variables:

```powershell
# PowerShell commands to set environment variables
$env:SERVICE_BUS_CONNECTION_STRING="your-connection-string"
$env:SERVICE_BUS_QUEUE_NAME="your-queue-name"      # For queue examples
$env:SERVICE_BUS_TOPIC_NAME="your-topic-name"      # For topic examples
```

## Project Structure

- `/servicebus`
  - `/Queue` - Service Bus Queue examples
    - `ServiceBusConsumer` - Queue message consumer
    - `ServiceBusProducerBatch` - Batch message producer
    - `ServiceBusProducerSingle` - Single message producer
  - `/Topic` - Service Bus Topic examples
    - `ServiceBusConsumer` - First topic subscriber
    - `ServiceBusConsumer2` - Second topic subscriber
    - `ServiceBusProducerBatch` - Batch message producer
    - `ServiceBusProducerSingle` - Single message producer
- `/eventgrid` - Event Grid examples
- `/eventhub` - Event Hub examples

## Running the Examples

1. First, set up your environment variables as shown above
2. Run the appropriate PowerShell script for the example you want to test
3. The script will execute the corresponding .NET projects
4. Follow any on-screen instructions provided by the applications

## Documentation

For more detailed information about the messaging patterns and architecture, refer to:
- `Messaging-Architecture.md` - Detailed architecture documentation
- `Examples.md` - Specific examples and use cases
