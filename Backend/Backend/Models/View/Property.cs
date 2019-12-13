namespace Backend.Models.View
{
    public class PropertyViewModel
    {
        public ProviderViewModel[] Providers { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
    }
}
