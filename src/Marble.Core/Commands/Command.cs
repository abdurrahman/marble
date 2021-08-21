using System;
using FluentValidation.Results;
using Marble.Core.Events;

namespace Marble.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; set; }
        
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }
        
        public abstract bool IsValid();
    }
}