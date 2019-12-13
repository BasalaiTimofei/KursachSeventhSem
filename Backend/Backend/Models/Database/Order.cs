using System;
using System.Collections.Generic;
using Backend.Interfaces;

namespace Backend.Models.Database
{
    public sealed class Order : IEntity
    {
        public string Id { get; set; }

        public DateTime DateTimeCreate { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public List<OrderProduct> Products { get; set; }

        public Order()
        {
            Products = new List<OrderProduct>();
        }
    }
}