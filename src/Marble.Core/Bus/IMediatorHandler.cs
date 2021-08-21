using System.Threading.Tasks;
using Marble.Core.Commands;
using Marble.Core.Events;
using MediatR;

namespace Marble.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;

        Task<TResult> RunCommand<TResult>(IRequest<TResult> command);

        Task RaiseEvent<T>(T @event) where T : Event;
    }
}