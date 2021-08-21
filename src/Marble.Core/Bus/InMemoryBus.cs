using System.Threading.Tasks;
using Marble.Core.Commands;
using Marble.Core.Events;
using Marble.Core.Notifications;
using MediatR;

namespace Marble.Core.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task<TResult> RunCommand<TResult>(IRequest<TResult> command)
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals(nameof(DomainNotification)))
            {
                _eventStore?.Save(@event);
            }
            
            return _mediator.Publish(@event);
        }
    }
}