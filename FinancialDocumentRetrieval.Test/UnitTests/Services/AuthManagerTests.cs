using FluentAssertions;
using FinancialDocumentRetrieval.BL.Implementation;
using FinancialDocumentRetrieval.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FakeItEasy;
using FinancialDocumentRetrieval.DAL.Identity;

namespace FinancialDocumentRetrieval.Tests
{
    public class AuthManagerTests
    {
        private readonly AuthManager _authManager;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private readonly UserManager<ApiUser> _userManagerFake;
        private readonly IMapper _mapperFake;
        private readonly IConfiguration _configurationFake;
        private readonly ILogger<AuthManager> _loggerFake;

        public AuthManagerTests()
        {
            _userManagerFake = A.Fake<UserManager<ApiUser>>();
            _mapperFake = A.Fake<IMapper>();
            _configurationFake = A.Fake<IConfiguration>();
            _loggerFake = A.Fake<ILogger<AuthManager>>();

            _userManager = _userManagerFake;
            _mapper = _mapperFake;
            _configuration = _configurationFake;
            _logger = _loggerFake;

            _authManager = new AuthManager(
                _mapper,
                _userManager,
                _configuration,
                _logger
            );
        }

        [Fact]
        public void RegisterAsync_ValidUserDto_ReturnsEmptyErrorsList()
        {
            // Arrange
            var userDto = new ApiUserDto
            {
                FirstName = "Marko",
                LastName = "Trmcic",
                Email = "marko@example.com",
                Password = "Password123" // Meets password requirements
            };

            A.CallTo(() => _userManagerFake.CreateAsync(A<ApiUser>._, userDto.Password))
                .Returns(IdentityResult.Success);

            // Act
            var errors = _authManager.RegisterAsync(userDto).GetAwaiter().GetResult();

            // Assert
            errors.Should().BeEmpty();
        }

        [Fact]
        public void RegisterAsync_InvalidUserDto_ReturnsErrorsList()
        {
            // Arrange
            var userDto = new ApiUserDto
            {
                FirstName = "Marko",
                LastName = "Trmcic",
                Email = "marko@example.com",
                Password = "pass" // Does not meet password requirements
            };

            var errorsList = new[]
            {
                new IdentityError { Code = "PasswordTooShort", Description = "Password is too short." },
                new IdentityError { Code = "RequireNonAlphanumeric", Description = "Password RequireNonAlphanumeric." },
                new IdentityError { Code = "RequireUppercase", Description = "Password RequireUppercase." }

                // TODO add rest of Identity validations
            };
            A.CallTo(() => _userManagerFake.CreateAsync(A<ApiUser>._, userDto.Password))
                .Returns(IdentityResult.Failed(errorsList));

            // Act
            var errors = _authManager.RegisterAsync(userDto).GetAwaiter().GetResult();

            // Assert
            errors.Should().Contain(errorsList)
                .And.HaveCount(errorsList.Length);
        }

        [Fact]
        public void RegisterAsync_BadEmailFormat_ReturnsErrorsList()
        {
            // Arrange
            var userDto = new ApiUserDto
            {
                FirstName = "Marko",
                LastName = "Trmcic",
                Email = "bad-email-format",
                Password = "Password123"
            };

            var errorsList = new[]
            {
                new IdentityError { Code = "InvalidEmail", Description = "Email is not in a valid format." }
            };

            A.CallTo(() => _userManagerFake.CreateAsync(A<ApiUser>._, userDto.Password))
                .Returns(IdentityResult.Failed(errorsList));

            // Act
            var errors = _authManager.RegisterAsync(userDto).GetAwaiter().GetResult();

            // Assert
            errors.Should().NotBeEmpty()
                .And.ContainSingle()
                .Which.Should().BeEquivalentTo(errorsList[0]);
        }
    }
}