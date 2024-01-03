namespace FinancialDocumentRetrieval.Models.Common.Response
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public bool Successful { get; set; } = true;
        public List<string> SuccessMsgs { get; set; } = new();
        public List<string> ErrorList { get; set; } = new();
        public List<string> WarningList { get; set; } = new ();
    }
}
