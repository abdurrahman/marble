using System.Threading.Tasks;

namespace Marble.Core.Commands
{
    public interface ICommandSender
    {
        Task Send<T>(T command) where T : Command;
    }
}