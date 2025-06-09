using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Entities;
using alquimia.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace alquimia.Tests.TestServices
{
    public class JWTServiceTests
    {
        [Fact]
        public void GenerateToken_ShouldIncludeCorrectClaims_WhenCalledWithUserAndRoles()
        {
            // Arrange
            var configMock = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Jwt:Key", "ThisIsASecretKeyForTestingOnly123!" },
                    { "Jwt:Issuer", "AlquimiaTestIssuer" },
                    { "Jwt:Audience", "AlquimiaTestAudience" }
                }).Build();

            var service = new JwtService(configMock);
            var user = new User { Id = 42, Email = "user@test.com", Name = "Test User" };
            var roles = new List<string> { "Creador" };

            // Act
            var token = service.GenerateToken(user, roles);

            // Assert
            Assert.NotNull(token);
            Assert.Contains("ey", token); 
        }
    }
}

