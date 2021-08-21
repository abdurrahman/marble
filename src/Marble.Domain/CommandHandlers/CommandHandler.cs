using Marble.Core.Bus;
using Marble.Core.Commands;
using Marble.Core.Data;
using Marble.Core.Notifications;
using MediatR;

namespace Marble.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IMediatorHandler _bus;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IMediatorHandler bus, 
            IUnitOfWork unitOfWork, 
            INotificationHandler<DomainNotification> notifications)
        {
            _bus = bus;
            _unitOfWork = unitOfWork;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if (_unitOfWork.Commit()) return true;

            _bus.RaiseEvent(new DomainNotification("UoW Commit", 
                "A problem occurred during saving changes"));
            return false;
        }
    }
}