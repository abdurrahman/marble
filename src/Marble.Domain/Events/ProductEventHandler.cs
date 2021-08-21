using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Marble.Domain.Events
{
    public class ProductEventHandler :
        INotificationHandler<ProductCreatedEvent>,
        INotificationHandler<ProductRemovedEvent>,
        INotificationHandler<ProductUpdatedEvent>
    {
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProductRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}