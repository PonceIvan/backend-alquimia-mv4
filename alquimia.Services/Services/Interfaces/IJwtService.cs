using alquimia.Data.Data.Entities;

namespace backendAlquimia.alquimia.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user, IList<string> roles);
    }
}
