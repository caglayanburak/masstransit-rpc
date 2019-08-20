using System;
using MassTransit;

namespace RConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
             var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "trendyol_rpc_skipped", e =>
                {
                    e.PrefetchCount = 1;
                    e.Consumer<OrderReceivedConsumer>();
                });
            });
            busControl.Start();
            Console.ReadLine();
        Console.ReadLine();
        }
    }
}
