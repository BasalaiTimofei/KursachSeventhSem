namespace Backend.Models.View
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string[] ProductId { get; set; }
        public string DateCreate { get; set; }
        public string FullPrice { get; set; }
    }
}