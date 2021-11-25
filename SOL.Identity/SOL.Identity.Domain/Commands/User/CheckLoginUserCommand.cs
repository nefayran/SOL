namespace SOL.Identity.SOL.Identity.Domain.Commands.User
{
    public class CheckLoginUserCommand : UserCommandBase
    {
        public string Token { get; set; }
    }
}
