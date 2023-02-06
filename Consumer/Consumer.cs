using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    public class Consumer
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var queue = "QueueTest";

                channel.QueueDeclare(queue, false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    string data = Encoding.UTF8.GetString(body);
                    Console.WriteLine(data);
                };

                channel.BasicConsume(queue, true, consumer);
            }

            Console.ReadKey();
        }
    }
}
