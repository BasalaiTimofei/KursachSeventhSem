using System;
using System.Collections.Generic;
using Backend.Interfaces;

namespace Backend.Models.Database
{
    public sealed class Product : IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// Цена в BYN
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Краткое описание
        /// </summary>
        public string Description { get; set; }
        public DateTime DateTimeCreate { get; set; }
        
        /// <summary>
        /// Сылки на картинки
        /// </summary>
        public string UrlImage { get; set; }
        public ProductInformation ProductInformation { get; set; }

        public string ProviderId { get; set; }
        public Provider Provider { get; set; }

        public List<Assessment> Assessments { get; set; }
        public List<BasketProduct> Baskets { get; set; }
        public List<OrderProduct> Orders { get; set; }
        public List<Comment> Comments { get; set; }


        public Product()
        {
            Assessments = new List<Assessment>();
            Baskets = new List<BasketProduct>();
            Orders = new List<OrderProduct>();
            Comments = new List<Comment>();
        }
    }
}