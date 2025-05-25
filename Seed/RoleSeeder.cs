using alquimia.Data.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using alquimia.Data.Data.Entities;

namespace backendAlquimia.Seed
{
    public class RoleSeeder
    {
        private static readonly string[] Roles = { "Admin", "Creador", "Proveedor" };

        public static async Task SeedRolesAsync(IServiceProvider serviceprovider)
        {
            var rolemanager = serviceprovider.GetRequiredService<RoleManager<Role>>();

            foreach (var role in Roles)
            {
                if (!await rolemanager.RoleExistsAsync(role))
                {
                    await rolemanager.CreateAsync(new Role { Name = role });
                }
            }
        }
    }
}
