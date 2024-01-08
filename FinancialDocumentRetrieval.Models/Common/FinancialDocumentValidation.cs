using FinancialDocumentRetrieval.Models.DTOs.RequestDTOs;

namespace FinancialDocumentRetrieval.Models.Common
{
    public class FinancialDocumentValidation : FinancialDocumentRequestDTO
    {
        public Guid ClientId { get; set; }
    }
}