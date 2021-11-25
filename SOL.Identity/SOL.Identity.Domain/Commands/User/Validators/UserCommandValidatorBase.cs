using FluentValidation;
using SOL.Identity.SOL.Identity.Domain.Interfaces.Repositories;

namespace SOL.Identity.SOL.Identity.Domain.Commands.User.Validators
{
    public abstract class UserCommandValidatorBase<T>: AbstractValidator<T> where T : UserCommandBase
    {
        private readonly IUserRepository _userRepository;

        protected UserCommandValidatorBase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            ValidateEmail();
        }


        private void ValidateEmail()
        {
            RuleFor(userCommandBase => userCommandBase.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
        }
    }
}