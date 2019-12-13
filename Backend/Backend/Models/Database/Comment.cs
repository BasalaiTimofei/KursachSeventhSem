using System;
using Backend.Interfaces;

namespace Backend.Models.Database
{
    public class Comment : IEntity
    {
        public string Id { get; set; }

        public string Text { get; set; }
        public DateTime DateTimeCreate { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}