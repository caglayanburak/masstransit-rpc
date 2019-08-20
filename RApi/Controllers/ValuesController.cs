using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RApi.Models;

namespace RApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IBusControl _bus;
        IRequestClient<Order> _client;

        public OrdersController(IBusControl bus)
        {
            _bus = bus;
            _bus.Start();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody]Order order)
        {
            IRequestClient<Order, Order> client =
                            _bus.CreateRequestClient<Order, Order>(new Uri("rabbitmq://localhost/trendyol_rpc"), TimeSpan.FromSeconds(10));

            var response = await client.Request(new Order { Id = order.Id, OrderCode = "tt" });
            Console.WriteLine($"{order.Id} - test:" + response.Id);

            return response;
        }

        private void CreateOrder(Order orderModel)
        {
            _bus.Send(orderModel).Wait();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
