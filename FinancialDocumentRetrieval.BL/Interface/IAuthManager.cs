using FinancialDocumentRetrieval.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace FinancialDocumentRetrieval.BL.Interface
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterAsync(ApiUserDto userDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}
