namespace SOL.Identity.Domain.Commands.User
{
    public class LoginUserCommand : UserCommandBase
    {
        public string Password { get; set; }
        public object Token { get; set; }
    }
}
