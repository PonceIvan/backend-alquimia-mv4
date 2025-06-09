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
        private AlquimiaDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase(databaseName: $"AlquimiaTestDb_{System.Guid.NewGuid()}")
                .Options;

            return new AlquimiaDbContext(options);
        }

        private UserManager<User> GetMockUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
        }
        [Fact]
        public async Task GetPendingAndApprovedProvidersAsync_ReturnsValidProviders()
        {
            var context = CreateDbContext();
            var users = new List<User>
            {
                new User { Id = 1, Name = "Ana", Email = "ana@test.com", EsProveedor = true },
                new User { Id = 2, Name = "Bob", Email = "bob@test.com", EsProveedor = true },
                new User { Id = 3, Name = "Carla", Email = "carla@test.com", EsProveedor = false },
            };
            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            var userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null
            );

            userManagerMock.Setup(m => m.Users).Returns(context.Users);
            userManagerMock.Setup(m => m.GetRolesAsync(It.Is<User>(u => u.Id == 1)))
                .ReturnsAsync(new List<string> { "Proveedor" });
            userManagerMock.Setup(m => m.GetRolesAsync(It.Is<User>(u => u.Id == 2)))
                .ReturnsAsync(new List<string> { "Creador" });

            var service = new AdminService(context, userManagerMock.Object);
            var result = await service.GetPendingAndApprovedProvidersAsync();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Nombre == "Ana" && r.EsAprobado);
            Assert.Contains(result, r => r.Nombre == "Bob" && !r.EsAprobado);
        }

        [Fact]
        public async Task GetPendingOrApprovedProviderByIdAsync_ReturnsProviderIfExists()
        {
            var context = CreateDbContext();
            var user = new User { Id = 5, Name = "Dani", Email = "dani@test.com", EsProveedor = true };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Creador" });

            var service = new AdminService(context, userManagerMock.Object);
            var result = await service.GetPendingOrApprovedProviderByIdAsync(5);

            Assert.NotNull(result);
            Assert.Equal("Dani", result.Nombre);
        }

        [Fact]
        public async Task GetPendingOrApprovedProviderByIdAsync_ReturnsNullIfNotProvider()
        {
            var context = CreateDbContext();
            var user = new User { Id = 6,Name = "Axel", EsProveedor = false };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userManager = GetMockUserManager();
            var service = new AdminService(context, userManager);
            var result = await service.GetPendingOrApprovedProviderByIdAsync(6);

            Assert.Null(result);
        }

        [Fact]
        public async Task ApprovePendingProviderAsync_ReturnsTrueIfSuccess()
        {
            var context = CreateDbContext();
            var user = new User { Id = 7, Name="Lenny", EsProveedor = true };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Creador" });
            userManagerMock.Setup(m => m.RemoveFromRoleAsync(user, "Creador")).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(m => m.AddToRoleAsync(user, "Proveedor")).ReturnsAsync(IdentityResult.Success);

            var service = new AdminService(context, userManagerMock.Object);
            var result = await service.ApprovePendingProviderAsync(7);

            Assert.True(result);
        }

        [Fact]
        public async Task ApprovePendingProviderAsync_ReturnsFalseIfUserNotFound()
        {
            var context = CreateDbContext();
            var userManager = GetMockUserManager();
            var service = new AdminService(context, userManager);

            var result = await service.ApprovePendingProviderAsync(99);
            Assert.False(result);
        }

        [Fact]
        public async Task DeactivateProviderAsync_ReturnsTrueIfSuccess()
        {
            var context = CreateDbContext();
            var user = new User { Id = 8,Name="Michel", EsProveedor = true };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Proveedor" });
            userManagerMock.Setup(m => m.RemoveFromRoleAsync(user, "Proveedor")).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(m => m.AddToRoleAsync(user, "Creador")).ReturnsAsync(IdentityResult.Success);

            var service = new AdminService(context, userManagerMock.Object);
            var result = await service.DeactivateProviderAsync(8);

            Assert.True(result);
        }

        [Fact]
        public async Task DeactivateProviderAsync_ReturnsFalseIfUserInvalid()
        {
            var context = CreateDbContext();
            var userManager = GetMockUserManager();
            var service = new AdminService(context, userManager);

            var result = await service.DeactivateProviderAsync(100);
            Assert.False(result);
        }

        [Fact]
        public async Task ApprovePendingProviderAsync_HandlesMultipleRoles()
        {
            var context = CreateDbContext();
            var user = new User { Id = 10,Name="Dana", EsProveedor = true };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string> { "Creador", "Proveedor" });

            var service = new AdminService(context, userManagerMock.Object);
            var result = await service.ApprovePendingProviderAsync(10);

            Assert.True(result);
        }

        [Fact]
        public async Task DeactivateProviderAsync_FailsRoleAssignment()
        {
            var context = CreateDbContext();
            var user = new User { Id = 11, Name="Lisa", EsProveedor = true };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(new List<string> { });

            var service = new AdminService(context, userManagerMock.Object);
            var result = await service.DeactivateProviderAsync(11);

            Assert.True(result); 
        }


        private Mock<DbSet<T>> MockDbSet<T>(List<T> list) where T : class
        {
            var query = list.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(query.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(query.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(query.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => query.GetEnumerator());
            return mockSet;
        }
        

    }
}
