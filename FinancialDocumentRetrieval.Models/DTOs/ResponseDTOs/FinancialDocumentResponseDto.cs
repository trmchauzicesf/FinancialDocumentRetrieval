namespace FinancialDocumentRetrieval.Models.DTOs.ResponseDTOs
{
    public record FinancialDocumentResponseDto
    {
        public string Data { get; set; }

        public CompanyDTO Company { get; set; }
    }
}
