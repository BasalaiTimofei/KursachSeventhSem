namespace Backend.Models.View
{
    public class ProductFullViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string UrlImage { get; set; }
        public ProductInformationViewModel ProductInformation { get; set; }
        public string ProviderName { get; set; }
        public string Assessment { get; set; }
    }
}