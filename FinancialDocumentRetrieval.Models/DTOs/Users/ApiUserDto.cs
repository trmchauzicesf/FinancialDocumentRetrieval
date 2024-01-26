using System.ComponentModel.DataAnnotations;

namespace FinancialDocumentRetrieval.Models.Users
{
    public record ApiUserDto : LoginDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
