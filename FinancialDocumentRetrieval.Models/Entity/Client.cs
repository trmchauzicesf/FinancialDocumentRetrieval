namespace FinancialDocumentRetrieval.Models.Entity
{
    public class Client: BaseEntity
    {
        public string Name { get; set; }
        public string Vat { get; set; }
        public string RegistrationNumber { get; set; }
        public string CompanyType { get; set; }
    }
}
