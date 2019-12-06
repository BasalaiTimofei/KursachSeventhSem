using System;
using System.Collections.Generic;

namespace Backend.Models.Database
{
    public sealed class OrderDatabaseModel
    {
        public string Id { get; set; }

        public DateTime DateTimeCreate { get; set; }

        public string UserId { get; set; }
        public UserDatabaseModel User { get; set; }

        public List<OrderProductDatabaseModel> Products { get; set; }

        public OrderDatabaseModel()
        {
            Products = new List<OrderProductDatabaseModel>();
        }
    }
}