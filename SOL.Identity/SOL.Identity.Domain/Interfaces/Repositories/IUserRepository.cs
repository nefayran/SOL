using System.Threading.Tasks;
using SOL.Core.Interfaces;
using SOL.Identity.SOL.Identity.Domain.Entities;

namespace SOL.Identity.SOL.Identity.Domain.Interfaces.Repositories
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
        /// <param name="name">User name</param>
        /// <returns>true if exsist</returns>
        Task<bool> ExistsAsync(string name);
    }
}
