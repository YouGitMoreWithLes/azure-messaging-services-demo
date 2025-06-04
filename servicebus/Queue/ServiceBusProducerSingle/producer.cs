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
                string queueName = Environment.GetEnvironmentVariable("SERVICE_BUS_QUEUE_NAME") ?? "example-queue";

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

                try
                {
                    int count = 0;
                    do
                    {
                        Console.WriteLine($"Hit any key to send a message, hit 'x' to end");
                        var command = Console.ReadLine();

                        if (command?.ToLower() == "x")
                        {
                            Console.WriteLine("Exiting...");
                            break;
                        }

                        var message = new ServiceBusMessage($"Message {count++}");
                        message.ApplicationProperties.Add("MessageId", count);
                        message.ApplicationProperties.Add("MessageType", "Sample");
                        // message.ApplicationProperties.Add("MessageWait", count * 250);
                        message.TimeToLive = TimeSpan.FromHours(1);

                        await sender.SendMessageAsync(message);

                        Console.WriteLine($"Message {count} sent successfully");

                    } while (true);
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