using backendAlquimia.alquimia.Data;
//using backendAlquimia.alquimia.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Seed
{
    public static class ProductoSeeder
    {

        public static async Task SeedTiposProductoAsync(IServiceProvider services)
        {
        //    using var scope = services.CreateScope();
        //    var context = scope.ServiceProvider.GetRequiredService<AlquimiaDbContext>();

        //    if (!await context.TiposProducto.AnyAsync())
        //    {
        //        await context.TiposProducto.AddRangeAsync(new List<TipoProducto>
        //{
        //    new TipoProducto { Description = "Esencias puras" }, 
        //    new TipoProducto { Description = "Alcohol" },
        //    new TipoProducto { Description = "Frascos vidrio" }
        //});

        //        await context.SaveChangesAsync();
            //}
        }
    }
}