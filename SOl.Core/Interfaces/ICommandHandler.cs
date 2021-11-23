using DigestCreator.DigestCreator.Core.Commands;

namespace DigestCreator.DigestCreator.Core.Interfaces
{
    public interface ICommandHandler<in T>  where T : CommandBase
    {
        Result Handle(T command);
    }
}