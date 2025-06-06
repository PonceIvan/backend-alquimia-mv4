using alquimia.Services.Models;

namespace alquimia.Services.Interfaces
{
    public interface IDesignLabelService
    {
        Task<int> SaveDesignAsync(DesignDTO dto);

    }
}
