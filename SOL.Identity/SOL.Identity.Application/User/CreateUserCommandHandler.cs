using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SOL.Core.Commands;
using SOL.Core.Interfaces;
using SOL.Identity.SOL.Identity.Domain.Commands.User;
using SOL.Identity.SOL.Identity.Domain.Interfaces.Repositories;

namespace SOL.Identity.SOL.Identity.Application.User
{
    public class CreateUserCommandHandler : CommandHandlerBase, ICommandHandlerAsync<CreateUserCommand>, IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IValidator<CreateUserCommand> _createUserCommandValidator;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IValidator<CreateUserCommand> validator, IUserRepository userRepository)
        {
            _createUserCommandValidator = validator;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var validationResult = Validate(command, _createUserCommandValidator);

            if (validationResult.IsValid)
            {
                Domain.Entities.User user = new() { Email = command.Email, UserName = command.Email, Password = command.Password };
                await _userRepository.AddAsync(user);
            }

            return Return();
        }
    }
}