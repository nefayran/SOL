using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SOL.Identity.Application.Interfaces;
using SOL.Identity.Domain.Entities;
using SOL.Identity.Domain.Interfaces.Repositories;

namespace SOL.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        
        public UserRepository(
            UserManager<User> userManager, 
            IJwtGenerator jwtGenerator, 
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _signInManager = signInManager;
        }

        public async Task<bool> ExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<string> GetTokenByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = _jwtGenerator.CreateToken(user);
            return token;
        }

        public async Task<List<string>> LoginUserAsync(User user)
        {
            List<string> errors = new();
            if (await ExistsAsync(user.Email)) {
                var localUser = await _userManager.FindByEmailAsync(user.Email);
                var result = await _signInManager.CheckPasswordSignInAsync(localUser, user.Password, false);
                // SignIn success.
                if (result.Succeeded)
                {
                    return null;
                }
                // SignIn fail.
                // Check password.
                var pw = await _userManager.CheckPasswordAsync(localUser, user.Password);
                if (!pw)
                {
                    errors.Add("Invalid password for user.");
                    return errors;
                }
            }
            errors.Add("User with this email address does not exist.");
            return errors;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> AddAsync(User user)
        {
            var result = await _userManager.CreateAsync(user, user.Password);
            return result.Succeeded;
        }

        public int SaveChanges()
        {
            // empty
            return 0;
        }

        public void Add(User entity)
        {
            // empty
        }

        public void Update(User entity)
        {
            // empty
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
