namespace FinancialDocumentRetrieval.Models.DTOs.ResponseDTOs
{
    public record CompanyDTO
    {
        public string RegistrationNumber { get; set; }
        public string CompanyType { get; set; }
    }
}