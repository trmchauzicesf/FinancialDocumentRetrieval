using System.ComponentModel.DataAnnotations;

namespace FinancialDocumentRetrieval.Models.Users
{
    public class ApiUserDto : LoginDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
