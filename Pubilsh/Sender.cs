using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var queue = "QueueTest";
                channel.QueueDeclare(queue, false, false, false, null);

                string m = "frist app with rabbitmq";

                var body = Encoding.UTF8.GetBytes(m);

                channel.BasicPublish("", queue, null, body);

                Console.WriteLine(m);
            }
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        #region MyRegion

        public void SenderQueue(object message, string queue, string exchange = "", string hostName = "localHost")
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue, false, false, false, null);

                var body = Encoding.UTF8.GetBytes((char[])message);
                channel.BasicPublish(exchange, queue, null, body);
            }
        }
        #endregion

    }
}
