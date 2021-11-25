using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SOL.Core.Commands;
using SOL.Core.Interfaces;
using SOL.Identity.SOL.Identity.Application.Interfaces;
using SOL.Identity.SOL.Identity.Domain.Commands.User;
using SOL.Identity.SOL.Identity.Domain.Interfaces.Repositories;

namespace SOL.Identity.SOL.Identity.Application.User
{
    public class LoginUserCommandHandler : CommandHandlerBase, ICommandHandlerAsync<LoginUserCommand>, IRequestHandler<LoginUserCommand, Result>
    {
        private readonly IValidator<LoginUserCommand> _loginUserCommandValidator;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<Domain.Entities.User> _userManager;
        private readonly SignInManager<Domain.Entities.User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginUserCommandHandler(IValidator<LoginUserCommand> validator, IUserRepository userRepository, UserManager<Domain.Entities.User> userManager,
                                   SignInManager<Domain.Entities.User> signInManager, IJwtGenerator jwtGenerator)
        {
            _loginUserCommandValidator = validator;
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<Result> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var validationResult = Validate(command, _loginUserCommandValidator);

            if (validationResult.IsValid)
            {
                if(await _userRepository.ExistsAsync(command.Email)) {
                    var user = await _userManager.FindByEmailAsync(command.Email);
                    var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
                    // SignIn success.
                    if (result.Succeeded)
                    {
                        var token = _jwtGenerator.CreateToken(user);
                        return Return(token);
                    }
                    // SignIn fail.
                    else
                    {
                        var pw = await _userManager.CheckPasswordAsync(user, command.Password);
                        List<string> errors = new();
                        if (!pw)
                        {
                            errors.Add("Invalid password for user.");
                            return new Result(false, errors);
                        }
                    }
                }
            }

            return Return();
        }
    }
}