using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace consumidor
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var conn = factory.CreateConnection())
            {
                using (var canal = conn.CreateModel())
                {
                    canal.QueueDeclare(queue: "RAUBER", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(canal);

                    consumer.Received += Consumer_Received;

                    canal.BasicConsume(queue: "RAUBER", autoAck: true, consumer: consumer);
                    //canal.Close();
                    //canal.Dispose();
                }
                //conn.Close();
                //conn.Dispose();
            }

            Console.WriteLine("mensagem enviada");
            Console.ReadLine();
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            byte[] body = e.Body.ToArray();
            var mensagem = Encoding.UTF8.GetString(body);
            Console.WriteLine(mensagem);
        }
    }
}
