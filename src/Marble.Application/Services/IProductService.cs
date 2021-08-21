using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marble.Application.ViewModels;

namespace Marble.Application.Services
{
    public interface IProductService : IDisposable
    {
        IEnumerable<ProductViewModel> GetPublishedProducts();

        IEnumerable<ProductViewModel> GetFilteredProducts(string keyword, int? minStockQuantity,
            int? maxStockQuantity);

        Task<ProductViewModel> GetByIdAsync(Guid id);
        
        void Create(ProductCreateModel model);
        
        void Update(ProductViewModel model);

        void Remove(Guid id);
    }
}