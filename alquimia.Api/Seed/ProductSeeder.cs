
using alquimia.Data.Entities;

namespace alquimia.Api.Seed
{
    public static class ProductSeeder
    {

        public static async Task SeedTiposProductoAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AlquimiaDbContext>();
            await context.SaveChangesAsync();
        }
    }
}

