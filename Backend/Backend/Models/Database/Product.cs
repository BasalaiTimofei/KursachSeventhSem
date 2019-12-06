using System;
using System.Collections.Generic;

namespace Backend.Models.Database
{
    public sealed class ProductDatabaseModel
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
        public string[] UrlImages { get; set; }
        public ProductInformationDatabaseModel ProductInformation { get; set; }

        public string ProviderId { get; set; }
        public ProviderDatabaseModel Provider { get; set; }

        public List<AssessmentDatabaseModel> Assessment { get; set; }
        public List<BasketProductDatabaseModel> Baskets { get; set; }
        public List<OrderProductDatabaseModel> Orders { get; set; }
        public List<CommentDatabaseModel> Comments { get; set; }


        public ProductDatabaseModel()
        {
            Assessment = new List<AssessmentDatabaseModel>();
            Baskets = new List<BasketProductDatabaseModel>();
            Orders = new List<OrderProductDatabaseModel>();
            Comments = new List<CommentDatabaseModel>();
        }
    }
}