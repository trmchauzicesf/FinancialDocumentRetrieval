
using Microsoft.AspNetCore.Identity;

namespace FinancialDocumentRetrieval.DAL.Identity
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
