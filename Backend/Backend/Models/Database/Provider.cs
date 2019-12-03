using System.Collections.Generic;

namespace Backend.Models.Database
{
    public class Provider
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