using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SOL.Identity.SOL.Identity.Domain.Entities;
using SOL.Identity.SOL.Identity.Domain.Interfaces.Repositories;

namespace SOL.Identity.SOL.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null;
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
            // _identityContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
