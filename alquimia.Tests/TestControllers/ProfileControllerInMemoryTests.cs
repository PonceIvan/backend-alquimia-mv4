using alquimia.Api.Controllers;
using alquimia.Data.Entities;
using alquimia.Services;
using alquimia.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using Xunit;

namespace alquimia.Tests.TestControllers
{
    public class ProfileControllerInMemoryTests
    {
        private AlquimiaDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase(databaseName: "AlquimiaControllerTestDb")
                .Options;

            return new AlquimiaDbContext(options);
        }

        private UserManager<User> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<User>>();
            return new UserManager<User>(store.Object, null, null, null, null, null, null, null, null);
        }

        private IHttpContextAccessor GetHttpContextAccessor(int userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            }, "mock"));

            var httpContext = new DefaultHttpContext() { User = user };
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(httpContext);

            return mockHttpContextAccessor.Object;
        }

        [Fact]
        public async Task GetMyData_UserFound_ReturnsOkWithUserProfile()
        {
            // Arrange
            var context = GetDbContext();
            var user = new User
            {
                Id = 1,
                UserName = "user1",
                Email = "user1@example.com",
                Name = "User One",
                EsProveedor = true
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userManager = GetUserManagerMock();
            var httpContextAccessor = GetHttpContextAccessor(user.Id);

            var service = new ProfileService(userManager, context, httpContextAccessor);
            var controller = new ProfileController(service);

            // Act
            var result = await controller.GetMyData();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var profile = Assert.IsType<UserProfileUpdateDto>(okResult.Value);
            Assert.Equal("User One", profile.Name);
        }

        [Fact]
        public async Task GetMyData_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var context = GetDbContext();

            var userManager = GetUserManagerMock();
            var httpContextAccessor = GetHttpContextAccessor(999);

            var service = new ProfileService(userManager, context, httpContextAccessor);
            var controller = new ProfileController(service);

            // Act
            var result = await controller.GetMyData();
            Console.WriteLine(result);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Usuario no encontrado", notFoundResult.Value);
        }
    }
}
