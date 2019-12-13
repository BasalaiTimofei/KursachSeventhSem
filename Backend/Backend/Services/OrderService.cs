using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Backend.Context;
using Backend.Models.Database;
using Backend.Models.View;
using Microsoft.EntityFrameworkCore;

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
            await _applicationContext.Orders.AddAsync(order);
            //await _applicationContext.SaveChangesAsync();

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

        public async Task<List<OrderViewModel>> GetAll()
        {
            var orderDb = _applicationContext.Orders;
            var orderView = await orderDb.Select(order => new OrderViewModel
                {
                    Id = order.Id,
                    ProductId = order.Products.Select(w => w.ProductId).ToArray(),
                    UserId = order.UserId,
                    DateCreate = order.DateTimeCreate.ToShortDateString(),
                    FullPrice = Math.Round(order.Products.Sum(w => w.Product.Price), 1).ToString(CultureInfo.InvariantCulture)
                })
                .ToListAsync();

            return orderView;
        }
    }
}
