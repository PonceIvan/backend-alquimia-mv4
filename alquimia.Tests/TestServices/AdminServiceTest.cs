using alquimia.Data.Entities;
using alquimia.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace alquimia.Tests.TestServices
{
    public class AdminServiceTest
    {
        [Fact]
        public async Task GetPendingAndApprovedProvidersAsync_ShouldReturnCorrectProviders()
        {
            // Arrange
            var users = new List<User>
    {
        new User { Id = 1, Name = "Proveedor 1", Email = "p1@example.com", EsProveedor = true },
        new User { Id = 2, Name = "Proveedor 2", Email = "p2@example.com", EsProveedor = true },
        new User { Id = 3, Name = "No proveedor", Email = "n@example.com", EsProveedor = false },
    }.AsQueryable();

            var mockUserStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null
            );

            var userDbSet = new Mock<DbSet<User>>();
            userDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            userDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            userDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            userDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            userManager.Setup(u => u.Users).Returns(userDbSet.Object);

            userManager.Setup(u => u.GetRolesAsync(It.IsAny<User>())).ReturnsAsync((User user) =>
            {
                return user.Id switch
                {
                    1 => new List<string> { "Proveedor" },
                    2 => new List<string> { "Creador" },
                    _ => new List<string>()
                };
            });

            var contextMock = new Mock<AlquimiaDbContext>();
            var service = new AdminService(contextMock.Object, userManager.Object);

            // Act
            var result = await service.GetPendingAndApprovedProvidersAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Nombre == "Proveedor 1" && r.EsAprobado);
            Assert.Contains(result, r => r.Nombre == "Proveedor 2" && !r.EsAprobado);
        }

    }
}
