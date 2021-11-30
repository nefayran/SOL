using SOL.Core.Commands;

namespace SOL.Identity.Domain.Commands.User
{
    public class UserCommandBase : CommandBase
    {
        public string Email { get; set; }
    }
}
