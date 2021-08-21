using System.Collections.Generic;
using System.Linq;
using Marble.Core.Bus;
using Marble.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Marble.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;
        
        public BaseController(IMediatorHandler mediator, 
            INotificationHandler<DomainNotification> notifications)
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected string UserId => GetAuthenticatedUserId();

        protected bool IsOperationValid()
        {
            return !_notifications.HasNotifications();
        }
        
        protected new IActionResult Response(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(string.Empty, errorMessage);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }
        
        private string GetAuthenticatedUserId()
        {
            if (!User.Identity.IsAuthenticated) return string.Empty;
            return User.Claims.First(x => x.Type == "UserId").Value;
        }
    }
}