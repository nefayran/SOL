namespace SOL.Identity.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(Domain.Entities.User user);
    }
}
