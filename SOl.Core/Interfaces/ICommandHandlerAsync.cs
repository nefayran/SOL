using SOL.Core.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace SOL.Core.Interfaces
{
    public interface ICommandHandlerAsync<in T> where T : CommandBase
    {
        Task<Result> Handle(T command, CancellationToken cancellationToken);
    }
}