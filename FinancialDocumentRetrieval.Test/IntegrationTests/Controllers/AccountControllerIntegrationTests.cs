using FinancialDocumentRetrieval.Models.Users;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FinancialDocumentRetrieval.Test.IntegrationTests.Controllers;

public class AccountControllerIntegrationTests : IDisposable
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public AccountControllerIntegrationTests()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    public void Dispose()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Fact]
    public async Task RegisterAsync_ValidUser_ReturnsOk()
    {
        // Arrange
        var apiUserDto = new ApiUserDto
        {
            FirstName = "Marko",
            LastName = "Trmcic",
            Email = "marko@example.com",
            Password = "Password123." // Meets password requirements
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(apiUserDto), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/account/register", jsonContent);

        // Assert
        response.Should().NotBeNull();
        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }

    [Fact]
    public async Task RegisterAsync_InvalidUser_ReturnsBadRequest()
    {
        // Arrange
        var apiUserDto = new ApiUserDto
        {
            FirstName = "Marko",
            LastName = "Trmcic",
            Email = "marko@example.com",
            Password = "pass" // Does not meet password requirements
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(apiUserDto), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/account/register", jsonContent);

        // Assert
        response.Should().NotBeNull();
        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
    }
}