# Azure Messaging Examples

## Project Structure

```
servicebus/
├── Queue/
│   ├── ServiceBusConsumer/           # Queue message consumer
│   ├── ServiceBusProducerBatch/      # Batch message producer
│   └── ServiceBusProducerSingle/     # Single message producer
└── Topic/
    ├── ServiceBusConsumer/           # Topic subscriber 1
    ├── ServiceBusConsumer2/          # Topic subscriber 2
    ├── ServiceBusProducerBatch/      # Batch message publisher
    └── ServiceBusProducerSingle/     # Single message publisher

```

## Prerequisites

1. PowerShell 7.0 or later
2. Azure CLI installed and configured
3. .NET 8.0 SDK
4. Azure subscription with appropriate permissions
5. Environment variables set:
   - `SERVICE_BUS_CONNECTION_STRING`: Your Azure Service Bus connection string (the script below will set this if you run it)
   - `SERVICE_BUS_QUEUE_NAME`: (Optional) Your queue name
   - `SERVICE_BUS_TOPIC_NAME`: (Optional) Your topic name
   - `SERVICE_BUS_SUBSCRIPTION_NAME`: (Optional) Your subscription name

## Notes

- Each script should be run from the root directory of the project
- Make sure you have the necessary Azure permissions before running the scripts
- Check the individual project README files for specific configuration requirements

This repository contains examples of different Azure messaging services implementations including Service Bus (Queues and Topics), Event Grid, and Event Hubs.

## PowerShell Scripts

The following PowerShell scripts are available in the root folder. To run any script, first open PowerShell and navigate to the root directory:

```powershell
.\ScriptName.ps1
```

### Available Scripts

#### [CreateAzureResources.ps1](./CreateAzureResources.ps1)
**Purpose**: Use this script to create the Azure resources required to run the examples/demos

```powershell
.\CreateAzureResources.ps1
```

#### [RunServicebusQueueSingleExample.ps1](./RunServicebusQueueSingleExample.ps1)

**Purpose**: Example/demo for sending messages one at a time

```powershell
.\RunServicebusQueueSingleExample.ps1
```

#### [RunServicebusQueueBatchExample.ps1](./RunServicebusQueueBatchExample.ps1)

**Purpose**: Example/demo for sending messages in a batch to improve performance

```powershell
.\RunServicebusQueueBatchExample.ps1
```

#### [RunServicebusQueueMultipleExample.ps1](./RunServicebusQueueMultipleExample.ps1)

**Purpose**: Example/demo for showing how consumers react when there are multiple listeners on a single queue

```powershell
.\RunServicebusQueueMultipleExample.ps1
```

#### [RunServicebusTopicExample.ps1](./RunServicebusTopicExample.ps1)

**Purpose**: Example/demo for how topic consumers react when there are multiple listeners

```powershell
.\RunServicebusTopicExample.ps1
```