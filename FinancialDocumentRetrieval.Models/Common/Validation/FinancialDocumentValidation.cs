namespace FinancialDocumentRetrieval.Models.Common.Validation
{
    public class FinancialDocumentValidation
    {
        public Guid ClientId { get; set; }

        public string ProductCode { get; set; }

        public Guid TenantId { get; set; }

        public Guid DocumentId { get; set; }
    }
}