using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Services.Models;

namespace alquimia.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<ProviderDTO>> GetPendingAndApprovedProvidersAsync();
        Task<ProviderDTO?> GetPendingOrApprovedProviderByIdAsync(int id);
        Task<bool> ApprovePendingProviderAsync(int id);
        Task<bool> DeactivateProviderAsync(int id);
    }
}
