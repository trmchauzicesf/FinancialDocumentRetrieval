using FakeItEasy;
using FinancialDocumentRetrieval.BL.Implementation;
using FinancialDocumentRetrieval.DAL.UnitOfWork;
using FinancialDocumentRetrieval.Models.Common.Exceptions;
using FinancialDocumentRetrieval.Models.Entity;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace FinancialDocumentRetrieval.Test.UnitTests.Services
{
    public class ClientServiceTests
    {
        [Fact]
        public async Task GetVatForId_ValidClientId_ReturnsVat()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var expectedVat = "AB321321321";

            var fakeUnitOfWork = A.Fake<IRepositoryInitUnitOfWork>();
            A.CallTo(() => fakeUnitOfWork.ClientRepository.GetVat(clientId)).Returns(expectedVat);

            var fakeLogger = A.Fake<ILogger<ClientService>>();

            var clientService = new ClientService(fakeUnitOfWork, fakeLogger);

            // Act
            var actualVat = await clientService.GetVatForId(clientId);

            // Assert
            actualVat.Should().NotBeNullOrWhiteSpace();
            actualVat.Should().BeOfType<string>();
            actualVat.Should().Be(expectedVat);
        }

        [Fact]
        public async Task GetVatForId_InvalidClientId_ReturnsNull()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            string expectedVat = null; // Simulate an invalid or non-existent client

            var fakeUnitOfWork = A.Fake<IRepositoryInitUnitOfWork>();
            A.CallTo(() => fakeUnitOfWork.ClientRepository.GetVat(clientId)).Returns(expectedVat);

            var fakeLogger = A.Fake<ILogger<ClientService>>();

            var clientService = new ClientService(fakeUnitOfWork, fakeLogger);

            // Act
            var actualVat = await clientService.GetVatForId(clientId);

            // Assert
            actualVat.Should().BeNull();
        }

        [Theory]
        [InlineData("AB321321321")]
        public async Task GetAdditionalClientInfoForVat_ReturnsClientWhenCompanyTypeIsNotSmall(
            string clientVat)
        {
            // Arrange
            var client = new Client { CompanyType = "medium" };
            var fakeUnitOfWork = A.Fake<IRepositoryInitUnitOfWork>();
            A.CallTo(() => fakeUnitOfWork.ClientRepository.GetAdditionalClientInfoForVat(clientVat)).Returns(client);
            var fakeLogger = A.Fake<ILogger<ClientService>>();
            var clientService = new ClientService(fakeUnitOfWork, fakeLogger);

            // Act
            var result = await clientService.GetAdditionalClientInfoForVat(clientVat);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Client>();
            result.CompanyType.Should().NotBe("small");
        }

        [Theory]
        [InlineData("AB321321321")]
        public async Task GetAdditionalClientInfoForVat_ThrowsExceptionWhenCompanyTypeIsSmall(
            string clientVat)
        {
            // Arrange
            var client = new Client { CompanyType = "small" };
            var fakeUnitOfWork = A.Fake<IRepositoryInitUnitOfWork>();
            A.CallTo(() => fakeUnitOfWork.ClientRepository.GetAdditionalClientInfoForVat(clientVat)).Returns(client);
            var fakeLogger = A.Fake<ILogger<ClientService>>();
            var clientService = new ClientService(fakeUnitOfWork, fakeLogger);

            // Act & Assert
            await Assert.ThrowsAsync<FinancialDocumentRetrievalException>(async () =>
                await clientService.GetAdditionalClientInfoForVat(clientVat));
        }
    }
}