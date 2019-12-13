using System.Collections.Generic;
using Backend.Interfaces;

namespace Backend.Models.Database
{
    public class Provider : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public Provider()
        {
            Products = new List<Product>();
        }
    }
}