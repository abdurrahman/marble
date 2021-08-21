using System;
using MediatR;

namespace Marble.Core.Events
{
    public abstract class Message : IRequest<bool>
    {
        public Guid AggregateId { get; set; }
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}