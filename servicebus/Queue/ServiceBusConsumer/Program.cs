using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace ServiceBusConsumer
{
    class Program
    {
        // handle received messages
        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received message: {body}");
            
            // Display message properties
            if (args.Message.ApplicationProperties.TryGetValue("MessageId", out var messageId))
            {
                Console.WriteLine($"MessageId: {messageId}");
            }
            if (args.Message.ApplicationProperties.TryGetValue("MessageType", out var messageType))
            {
                Console.WriteLine($"MessageType: {messageType}");
            }
            if (args.Message.ApplicationProperties.TryGetValue("MessageWait", out var messageWait))
            {
                Console.WriteLine($"MessageWait: {messageWait}");
                Thread.Sleep(Convert.ToInt32(messageWait));
            }

            // complete the message, marking it as processed
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Error Source: {args.ErrorSource}");
            Console.WriteLine($"Error Message: {args.Exception.Message}");
            return Task.CompletedTask;
        }

        static async Task Main(string[] args)
        {
            try
            {
                // connection string to your Service Bus namespace
                string connectionString = Environment.GetEnvironmentVariable("SERVICE_BUS_CONNECTION_STRING", EnvironmentVariableTarget.User) ?? throw new InvalidOperationException("SERVICE_BUS_CONNECTION_STRING environment variable is not set");

                // name of your Service Bus queue
                string queueName = Environment.GetEnvironmentVariable("SERVICE_BUS_QUEUE_NAME") ?? "example-queue";

                // the client that owns the connection and can be used to create senders and receivers
                ServiceBusClient? client;

                // the processor that reads and processes messages from the queue
                ServiceBusProcessor? processor;

                // Create the client with retry options
                client = new ServiceBusClient(connectionString, new ServiceBusClientOptions
                {
                    TransportType = ServiceBusTransportType.AmqpWebSockets,
                    RetryOptions = new ServiceBusRetryOptions
                    {
                        MaxRetries = 3,
                        MaxDelay = TimeSpan.FromSeconds(10)
                    }
                });

                // Create the processor with custom options
                processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions
                {
                    MaxConcurrentCalls = 1, // Process one message at a time
                    AutoCompleteMessages = false, // Manual completion for better control
                    PrefetchCount = 10 // Pre-fetch messages for better performance
                });

                try
                {
                    // Add handler to process messages
                    processor.ProcessMessageAsync += MessageHandler;
                    // Add handler to process any errors
                    processor.ProcessErrorAsync += ErrorHandler;

                    // Start processing
                    await processor.StartProcessingAsync();
                    Console.WriteLine("Started processing messages...");
                    Console.WriteLine("Press any key to stop processing...");
                    Console.ReadKey();

                    // Stop processing
                    Console.WriteLine("\nStopping the processor...");
                    await processor.StopProcessingAsync();
                    Console.WriteLine("Stopped processing");
                }
                finally
                {
                    // Cleanup processing resources
                    await processor.DisposeAsync();
                    await client.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }
    }
}
