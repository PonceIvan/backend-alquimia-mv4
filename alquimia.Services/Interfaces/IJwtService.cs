using alquimia.Data.Entities;

namespace alquimia.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user, IList<string> roles);
    }
}
