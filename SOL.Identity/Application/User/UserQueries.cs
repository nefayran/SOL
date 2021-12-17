using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SOL.Core.Commands;
using SOL.Core.Interfaces;
using SOL.Identity.Application.Interfaces;
using SOL.Identity.Domain.Commands.User;
using SOL.Identity.Domain.Interfaces.Repositories;

namespace SOL.Identity.Application.User
{
    public class UserQueries : IUserQueries
    {
        private readonly IUserRepository _userRepository;

        public UserQueries(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // TODO: Transfer to TokenQueries.
        public async Task<string> GetTokenByEmailAsync(string email)
        {
            return await _userRepository.GetTokenByEmailAsync(email);
        }
        
        public async Task<Domain.Entities.User> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }
    }
}