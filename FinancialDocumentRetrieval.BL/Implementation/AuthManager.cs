using AutoMapper;
using FinancialDocumentRetrieval.BL.Interface;
using FinancialDocumentRetrieval.DAL.Identity;
using FinancialDocumentRetrieval.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancialDocumentRetrieval.BL.Implementation
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private ApiUser _user;


        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration,
            ILogger<AuthManager> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            AuthResponseDto authResponseDto = new AuthResponseDto();
            _logger.LogInformation($"Looking for user with email {loginDto.Email}");
            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (_user == null || isValidUser == false)
            {
                _logger.LogWarning($"User with email {loginDto.Email} was not found");
                // not to return null
                return authResponseDto;
            }

            var token = await GenerateToken();
            _logger.LogInformation($"Token generated for user with email {loginDto.Email} | Token: {token}");

            authResponseDto.Token = token;
            authResponseDto.UserId = _user.Id;
            return authResponseDto;
        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            _user = _mapper.Map<ApiUser>(userDto);
            _user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(_user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;
        }

        private async Task<string> GenerateToken()
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:SecretKey"]));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtConfiguration:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
