using FinancialDocumentRetrieval.BL.Interface;
using FinancialDocumentRetrieval.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialDocumentRetrieval.Api.Controllers
{
    public class ClientController : BaseApiController
    {
        readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [Authorize]
        [HttpGet("get-user")]
        public async Task<ActionResult<List<Client>>> Get()
        {
            var clients = await _clientService.Get();
            return Ok(clients);
        }
    }
}
