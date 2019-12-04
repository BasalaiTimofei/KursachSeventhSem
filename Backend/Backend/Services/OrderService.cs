using System;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;

namespace Backend.Services
{
    public class OrderService
    {
        private readonly ApplicationContext _applicationContext;

        public OrderService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Create(string userId, string[] productId)
        {
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                DateTimeCreate = DateTime.Now,
            };
            await _applicationContext.SaveChangesAsync();

            foreach (var item in productId)
            {
                await _applicationContext.OrderProducts.AddAsync(new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = item
                });
            }
            await _applicationContext.SaveChangesAsync();
        }
    }
}
