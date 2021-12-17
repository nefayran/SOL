using System.Threading.Tasks;

namespace SOL.Identity.Application.Interfaces
{
    public interface IUserQueries
    {
        // TODO: Transfer.
        Task<string> GetTokenByEmailAsync(string email);
        Task<Domain.Entities.User> GetByEmailAsync(string email);
    }
}