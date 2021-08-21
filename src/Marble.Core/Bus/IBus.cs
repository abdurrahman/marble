using Marble.Core.Commands;
using Marble.Core.Events;

namespace Marble.Core.Bus
{
    public interface IBus : IEventStore, ICommandSender
    {
        
    }
}