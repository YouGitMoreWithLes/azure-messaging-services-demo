using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace ServiceBusProducer
{
    public class Program
    {


        public static async Task Main(string[] args)
        {
            try
            {
                // connection string to your Service Bus namespace
                string connectionString = Environment.GetEnvironmentVariable("SERVICE_BUS_CONNECTION_STRING", EnvironmentVariableTarget.User) ?? throw new InvalidOperationException("SERVICE_BUS_CONNECTION_STRING environment variable is not set");

                // name of your Service Bus queue
                string queueName = Environment.GetEnvironmentVariable("SERVICE_BUS_TOPIC_NAME") ?? "example-topic";

                // number of messages to be sent to the queue
                int numOfMessages = 10;

                // the client that owns the connection and can be used to create senders and receivers
                ServiceBusClient? client;

                // the sender used to publish messages to the queue
                ServiceBusSender? sender;

                // Create the clients that we'll use for sending messages.
                // The client is created with options to enable connection pooling for better performance
                client = new ServiceBusClient(connectionString, new ServiceBusClientOptions
                {
                    TransportType = ServiceBusTransportType.AmqpWebSockets,
                    RetryOptions = new ServiceBusRetryOptions
                    {
                        MaxRetries = 3,
                        MaxDelay = TimeSpan.FromSeconds(10)
                    }
                });
                sender = client.CreateSender(queueName);

                // create a batch for better performance
                using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

                for (int i = 1; i <= numOfMessages; i++)
                {
                    var message = new ServiceBusMessage($"Message {i}");
                    message.ApplicationProperties.Add("MessageId", i);
                    message.ApplicationProperties.Add("MessageType", "Sample");
                    message.TimeToLive = TimeSpan.FromHours(1);

                    if (!messageBatch.TryAddMessage(message))
                    {
                        // if the message is too large for the batch
                        throw new Exception($"Message {i} is too large to fit in the batch.");
                    }

                    Console.WriteLine($"Added message {i} to the batch");
                }

                try
                {
                    // Use the producer client to send the batch of messages to the Service Bus queue
                    await sender.SendMessagesAsync(messageBatch);
                    Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
                }
                finally
                {
                    // Cleaning up resources
                    await sender.DisposeAsync();
                    await client.DisposeAsync();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error occurred: {exception.Message}");
                Console.WriteLine($"Stack trace: {exception.StackTrace}");
            }
            finally
            {
                Console.WriteLine("Press any key to end the application");
                Console.ReadKey();
            }
        }
    }
}