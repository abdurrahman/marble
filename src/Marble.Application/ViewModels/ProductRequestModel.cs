namespace Marble.Application.ViewModels
{
    public class ProductRequestModel
    {
        public string Keyword { get; set; }

        public int? MinStockQuantity { get; set; }

        public int? MaxStockQuantity { get; set; }
    }
}