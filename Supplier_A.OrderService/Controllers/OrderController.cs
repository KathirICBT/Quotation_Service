using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using Supplier_A.Model;
using Supplier_A.OrderService.Data;
using Supplier_A.OrderService.Kafka;
using System.Text.Json;

namespace Supplier_A.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(OrderDbContext dbContext, IKafkaProducer producer) : ControllerBase
    {
        [HttpGet]
        public async Task<List<OrderModel>> GetOrders()
        {
            return await dbContext.Orders.ToListAsync();
        }

        [HttpPost]
        public async Task<OrderModel> CreateOrder(OrderModel order)
        {
            order.OrderDate = DateTime.Now;
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();

            await producer.ProduceAsync("order-topic", new Message<string, string>
            {
                Key = order.Id.ToString(),
                Value = JsonConvert.SerializeObject(order)
            });
            return order;
        }
    }
}
