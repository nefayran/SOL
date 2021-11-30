using System.Collections.Generic;
using System.Threading.Tasks;
using SOL.Core.Interfaces;
using SOL.Identity.Domain.Entities;

namespace SOL.Identity.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Add new user.
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>success/error</returns>
        Task<bool> AddAsync(User user);
        /// <summary>
        /// Check exist user or no.
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>true if exist</returns>
        Task<bool> ExistsAsync(string email);
        /// <summary>
        /// Generate and get token for user.
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>true if exist</returns>
        Task<string> GetTokenByEmailAsync(string email);
        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>true if successful</returns>
        Task<List<string>> LoginUserAsync(User user);
    }
}
