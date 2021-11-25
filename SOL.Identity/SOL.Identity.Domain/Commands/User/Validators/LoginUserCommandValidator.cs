using FluentValidation;
using SOL.Identity.SOL.Identity.Domain.Interfaces.Repositories;

namespace SOL.Identity.SOL.Identity.Domain.Commands.User.Validators
{
    public class LoginUserCommandValidator : UserCommandValidatorBase<LoginUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandValidator(IUserRepository userRepository)
            : base((userRepository))
        {
            _userRepository = userRepository;
            ValidateEmail();
            ValidatePassword();
        }
        /// <summary>
        /// Password fluent validation.
        /// </summary>
        private void ValidatePassword()
        {
            RuleFor(userCommand => userCommand.Password).NotEmpty().WithMessage("Please enter the password");
        }
        /// <summary>
        /// Email fluent validation.
        /// </summary>
        private void ValidateEmail()
        {
            RuleFor(userCommandBase => userCommandBase.Email)
                .MustAsync(async (name, cancellationToken) => (await _userRepository.ExistsAsync(name)))
                .WithSeverity(Severity.Error)
                .WithMessage("User with this email not found.");
        }
    }
}