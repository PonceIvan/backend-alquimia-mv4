using alquimia.Data.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace backendAlquimia.Seed
{
    public static class UserSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var email = "admin@alquimia.com";
            var password = "Admin123!";

            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                var admin = new User
                {
                    UserName = email,
                    Email = email,
                    Name = "Administrador"
                };

                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }

        public static async Task SeedProveedoresAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();


            var roleCreador = await roleManager.FindByNameAsync("Creador");
            if (roleCreador == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Creador"));
            }

            var roleProveedor = await roleManager.FindByNameAsync("Proveedor");
            if (roleProveedor == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("Proveedor"));
            }


            var usuarios = new List<(string Name, string Email, bool EsProveedor)>
            {
                ("Proveedor 1", "proveedor1@alquimia.com", true),
                ("Proveedor 2", "proveedor2@alquimia.com", true),
                ("Proveedor 3", "proveedor3@alquimia.com", true),
                ("Usuario 1", "usuario1@alquimia.com", false),
                ("Usuario 2", "usuario2@alquimia.com", false),
                ("Usuario 3", "usuario3@alquimia.com", false)
            };

            foreach (var (Name, Email, EsProveedor) in usuarios)
            {
                var existingUser = await userManager.FindByEmailAsync(Email);
                if (existingUser == null)
                {
                    var user = new User
                    {
                        UserName = Email,
                        Email = Email,
                        Name = Name,
                        EsProveedor = EsProveedor
                    };

                    var password = "Password123!";
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {

                        if (EsProveedor)
                        {
                            await userManager.AddToRoleAsync(user, "Proveedor");
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(user, "Creador");
                        }
                    }
                }
            }
        }
    }
}