using FinancialDocumentRetrieval.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace FinancialDocumentRetrieval.BL.Interface
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        Task<AuthResponseDto> Login(LoginDto loginDto);
    }
}
