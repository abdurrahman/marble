using System;
using System.Threading;
using System.Threading.Tasks;
using Marble.Core.Bus;
using Marble.Core.Data;
using Marble.Core.Notifications;
using Marble.Domain.Commands;
using Marble.Domain.Events;
using Marble.Domain.Models;
using Marble.Domain.Repositories;
using MediatR;

namespace Marble.Domain.CommandHandlers
{
    public class ProductCommandHandler : CommandHandler,
        IRequestHandler<CreateNewProductCommand, bool>,
        IRequestHandler<UpdateProductCommand, bool>,
        IRequestHandler<RemoveProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediatorHandler _bus;
        
        public ProductCommandHandler(IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications, 
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository)
            : base(bus, unitOfWork, notifications)
        {
            _bus = bus;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public Task<bool> Handle(CreateNewProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var product = new Product(Guid.NewGuid(), request.Title, request.Description, request.Price,
                request.Stock, request.CategoryId);

            // Todo: [abdurrahman] move rules into another method
            // Product should have a minimum stock quantity in Category level and Products with stock quantity
            // under this limit cannot be live.
            var category = _categoryRepository.GetByIdAsync(request.CategoryId).Result;
            if (category != null && request.Stock >= category.MinimumQuantity)
            {
                product.IsPublished = true;
            }
            
            _productRepository.Insert(product);

            if (Commit())
            {
                _bus.RaiseEvent(new ProductCreatedEvent(product.Id, product.Title, product.Description, 
                    product.Price, product.Stock, product.CategoryId));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var product = new Product(request.Id, request.Title, request.Description, request.Price,
                request.Stock, request.CategoryId);
            var existingProduct = _productRepository.GetByIdAsNoTracking(request.Id);
            
            if (existingProduct != null && existingProduct.Id != product.Id)
            {
                return Task.FromResult(false);
            }

            _productRepository.Update(product);

            if (Commit())
            {
                _bus.RaiseEvent(new ProductUpdatedEvent(request.Id, request.Title, request.Description,
                    request.Price, request.Stock, request.CategoryId));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }
            
            _productRepository.Delete(request.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new ProductRemovedEvent(request.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}