namespace Backend.Models.View
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ProviderId { get; set; }

        public string[] UrlImages { get; set; }
        public ProductInformationViewModel ProductInformation { get; set; }
    }
}