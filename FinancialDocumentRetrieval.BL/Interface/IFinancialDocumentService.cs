using FinancialDocumentRetrieval.Models.DTOs.ResponseDTOs;
using FinancialDocumentRetrieval.Models.DTOs.RequestDTOs;

namespace FinancialDocumentRetrieval.BL.Interface
{
    public interface IFinancialDocumentService
    {
        Task<FinancialDocumentResponseDto> RetrieveAsync(FinancialDocumentRequestDTO financialDocumentRequestDto);
    }
}