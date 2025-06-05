using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Entities;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly AlquimiaDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(UserManager<User> userManager, AlquimiaDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserProfileDto?> BringMyData()
        {

            var user = await GetCurrentUserAsync();
            if (user == null) return null;

            return new UserProfileDto
            {
                Name = user.Name,
                Email = user.Email,
                EsProveedor = user.EsProveedor,
                //CUIL = user.CUIL,
                //Ubicacion = user.Ubicacion,
                //CodigoPostal = user.CodigoPostal
            };
        }

        private async Task<User?> GetCurrentUserAsync()
        {
            var userIdString = _userManager.GetUserId(_httpContextAccessor.HttpContext?.User);
            if (int.TryParse(userIdString, out var userId))
            {
                return await _context.Users
                    .Include(u => u.Products)
                    .Include(u => u.UserProducts).ThenInclude(up => up.Producto)
                    .Include(u => u.IdFormulasNavigation)
                    .FirstOrDefaultAsync(u => u.Id == userId);
            }

            return null;
        }

        public async Task<List<Formula>> BringMyFormulas()
        {
            var user = await GetCurrentUserAsync();
            if (user == null || user.IdFormulasNavigation == null)
                return new List<Formula>();

            return new List<Formula> { user.IdFormulasNavigation };
        }

        public async Task<List<Product>> BringMyProducts()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return new List<Product>();

            // Productos comprados: podrían estar en Orders o en alguna relación más específica
            return user.Products.ToList();
        }

        public async Task<List<Product>> BringMyWishlist()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return new List<Product>();

            return user.UserProducts.Select(up => up.Producto).ToList();
        }

        public async Task<UserProfileDto?> UpdateMyData(UserProfileDto dto)
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return null;

            user.Name = dto.Name;
            //user.CUIL = dto.CUIL;
            //user.Ubicacion = dto.Ubicacion;
            //user.CodigoPostal = dto.CodigoPostal;

            await _context.SaveChangesAsync();

            return dto;
        }
    }
}
