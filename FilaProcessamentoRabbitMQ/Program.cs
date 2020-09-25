using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilaProcessamentoRabbitMQ
{
    public class RAUBER
    {
        public string nome { get; set; }
        public int idade { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<DateTime, RAUBER> listaRauber = new Dictionary<DateTime, RAUBER>();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var conn = factory.CreateConnection())
            {
                using (var canal = conn.CreateModel())
                {
                    canal.QueueDeclare(queue: "RAUBER", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    string msg = "teste rauber para mandar uma mensagem";
                    for (int i = 0; i < 100000; i++)
                    {
                        var corpo = Encoding.UTF8.GetBytes($"{msg} // {i}");
                        canal.BasicPublish(exchange: "", routingKey: "RAUBER", basicProperties: null, body: corpo);
                    }

                    //canal.Close();
                    //canal.Dispose();
                }
                //conn.Close();
                //conn.Dispose();
            }

            Console.WriteLine("mensagem enviada");
            Console.ReadLine();
        }
    }
}
