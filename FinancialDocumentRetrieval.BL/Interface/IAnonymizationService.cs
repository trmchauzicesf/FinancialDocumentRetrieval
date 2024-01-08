namespace FinancialDocumentRetrieval.BL.Interface
{
    public interface IAnonymizationService
    {
        string AnonymizeFinancialDocument(string json, string productCode);
    }
}