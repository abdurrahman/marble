using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marble.Application.ViewModels;
using Marble.Core.Bus;
using Marble.Core.ObjectMapper;
using Marble.Domain.Commands;
using Marble.Domain.Repositories;
using Marble.Domain.Specifications;

namespace Marble.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IObjectMapper _mapper;
        private readonly IMediatorHandler _bus;

        public ProductService(IProductRepository productRepository, IObjectMapper mapper, IMediatorHandler bus)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _bus = bus;
        }

        public IEnumerable<ProductViewModel> GetPublishedProducts()
        {
            var data = _productRepository.GetPublishedProducts();
            return _mapper.MapTo<IList<ProductViewModel>>(data);
        }
        
        public IEnumerable<ProductViewModel> GetFilteredProducts(string keyword, int? minStockQuantity,
            int? maxStockQuantity)
        {
            var data = _productRepository.GetAll(new ProductFilterSpecification(keyword,
                minStockQuantity, maxStockQuantity));
            return _mapper.MapTo<IList<ProductViewModel>>(data);
        }

        public async Task<ProductViewModel> GetByIdAsync(Guid id)
        {
            return _mapper.MapTo<ProductViewModel>(await _productRepository.GetByIdAsync(id));
        }

        public void Create(ProductCreateModel model)
        {
            var createCommand = _mapper.MapTo<CreateNewProductCommand>(model);
            _bus.SendCommand(createCommand);
        }

        public void Update(ProductViewModel model)
        {
            var updateCommand = _mapper.MapTo<UpdateProductCommand>(model);
            _bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveProductCommand(id);
            _bus.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}