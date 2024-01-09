using FinancialDocumentRetrieval.BL.Interface;
using FinancialDocumentRetrieval.Models.DTOs.RequestDTOs;
using FinancialDocumentRetrieval.Models.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialDocumentRetrieval.Api.Controllers
{
    public class FinancialDocumentController : BaseApiController
    {
        private readonly IFinancialDocumentService _financialDocumentService;

        public FinancialDocumentController(IFinancialDocumentService financialDocumentService)
        {
            _financialDocumentService = financialDocumentService;
        }

        [Authorize]
        [HttpGet("get-financial-document")]
        public async Task<ActionResult<FinancialDocumentResponseDto>> Get(
            [FromBody] FinancialDocumentRequestDTO financialDocumentRequestDto)
        {
            var financialDocument = await _financialDocumentService.Get(financialDocumentRequestDto);
            return Ok(financialDocument);
        }
    }
}