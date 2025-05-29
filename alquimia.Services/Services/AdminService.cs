using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Data.Entities;
using alquimia.Services.Services.Interfaces;
using alquimia.Services.Services.Models;
using Microsoft.AspNetCore.Identity;

namespace alquimia.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly AlquimiaDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminService(AlquimiaDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<ProviderDTO>> GetAllProvidersAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("Proveedor");

            return users.Select(u => new ProviderDTO
            {
                Id = u.Id,
                Nombre = u.Name,
                Email = u.Email,
                EsAprobado = u.EsProveedor
            }).ToList();
        }

        public async Task<ProviderDTO?> GetProviderByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Proveedor")) return null;

            return new ProviderDTO
            {
                Id = user.Id,
                Nombre = user.Name,
                Email = user.Email,
                EsAprobado = user.EsProveedor
            };
        }

        public async Task<bool> ApproveProviderAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Proveedor")) return false;

            user.EsProveedor = true;
            await _context.SaveChangesAsync();

            if (await _userManager.IsInRoleAsync(user, "Creador"))
                await _userManager.RemoveFromRoleAsync(user, "Creador");

            await _userManager.AddToRoleAsync(user, "Proveedor");

            return true;
        }


        public async Task<bool> DeactivateProviderAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Proveedor")) return false;

            user.EsProveedor = false;
            await _context.SaveChangesAsync();
            await _userManager.RemoveFromRoleAsync(user, "Proveedor");
            await _userManager.AddToRoleAsync(user, "Creador");
            return true;
        }
    }
}
