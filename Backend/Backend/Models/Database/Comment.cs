using System;

namespace Backend.Models.Database
{
    public class CommentDatabaseModel
    {
        public string Id { get; set; }

        public string Text { get; set; }
        public DateTime DateTimeCreate { get; set; }

        public string UserId { get; set; }
        public UserDatabaseModel User { get; set; }

        public string ProductId { get; set; }
        public ProductDatabaseModel Product { get; set; }
    }
}