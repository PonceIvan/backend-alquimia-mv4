using backendAlquimia.Data.Entities;

namespace backendAlquimia.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Usuario user, IList<string> roles);
    }
}
