using FluentValidation;
using SOL.Identity.SOL.Identity.Domain.Interfaces.Repositories;

namespace SOL.Identity.SOL.Identity.Domain.Commands.User.Validators
{
    public class CreateUserCommandValidator : UserCommandValidatorBase<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandValidator(IUserRepository userRepository)
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
            RuleFor(userCommand => userCommand.PasswordConfirmation).NotEmpty().WithMessage("Please enter the confirmation password");
            RuleFor(userCommand => userCommand.PasswordConfirmation).Equal(userCommand => userCommand.Password).WithMessage("Confiramtion password and password are equal");
            RuleFor(userCommand => userCommand.Password)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
            .Matches("[0-9]").WithMessage("Password must contain a number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");
        }
        /// <summary>
        /// Email fluent validation.
        /// </summary>
        private void ValidateEmail()
        {
            RuleFor(userCommand => userCommand.Email)
                .MustAsync(async (name, cancellationToken) => !(await _userRepository.ExistsAsync(name)))
                .WithSeverity(Severity.Error)
                .WithMessage("Email already taken");
        }
    }
}