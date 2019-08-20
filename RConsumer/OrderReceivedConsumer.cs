using MassTransit;
using RApi.Models;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace RConsumer
{
    class OrderReceivedConsumer : IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            var orderCommand = context.Message;

            Console.Out.WriteLine($"OrderService:Order code: {orderCommand.OrderCode} Order ID {orderCommand.Id} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");
            Thread.Sleep(5000);
            await context.RespondAsync<Order>(new Order
            {
                Id = context.Message.Id,
                OrderCode=context.Message.OrderCode
            });

        }
    }
}