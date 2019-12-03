namespace Backend.Models.Database
{
    public class Assessment
    {
        public string Id { get; set; }

        public byte Value { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}