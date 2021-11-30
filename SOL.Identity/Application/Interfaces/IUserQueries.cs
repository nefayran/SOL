using System.Threading.Tasks;

namespace SOL.Identity.Application.Interfaces
{
    public interface IUserQueries
    {
        Task<string> GetTokenByEmailAsync(string email);
    }
}