using FakeItEasy;
using FinancialDocumentRetrieval.Api.Controllers;
using FinancialDocumentRetrieval.BL.Implementation;
using FinancialDocumentRetrieval.BL.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Program = FinancialDocumentRetrieval.Api.Program;

namespace FinancialDocumentRetrieval.Test.IntegrationTests.Controllers
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private IAuthManager _authManager { get; }
        private ILogger<AccountController> _logger { get; }

        public CustomWebApplicationFactory()
        {
            _authManager = A.Fake<AuthManager>();
            _logger = A.Fake<ILogger<AccountController>>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(_authManager);
                services.AddSingleton(_logger);
            });
        }
    }
}