using Marble.Domain.Models;

namespace Marble.Domain.Specifications
{
    public class ProductFilterSpecification : BaseSpecification<Product>
    {
        public ProductFilterSpecification(string keyword, int? minStockQuantity, int? maxStockQuantity)
            : base(product => product.Title.Contains(keyword) || 
                              product.Description.Contains(keyword) ||
                              product.Category.Name.Contains(keyword) ||
                              product.Stock >= minStockQuantity ||
                              product.Stock <= maxStockQuantity)
        {
            AddInclude(x => x.Category);
        }
    }
}