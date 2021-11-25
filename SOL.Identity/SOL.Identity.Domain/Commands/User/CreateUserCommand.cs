namespace SOL.Identity.SOL.Identity.Domain.Commands.User
{
    public class CreateUserCommand : UserCommandBase
    {
        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}
