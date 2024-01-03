using System.Globalization;

namespace FinancialDocumentRetrieval.Models.Common.Exceptions
{
    public class FinancialDocumentRetrievalException : ApplicationException
    {
        public FinancialDocumentRetrievalException() : base() { }

        public FinancialDocumentRetrievalException(string message) : base(message) { }

        public FinancialDocumentRetrievalException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
