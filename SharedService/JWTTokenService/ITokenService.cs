using SharedService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedService.JWTTokenService
{
    public interface ITokenService
    {
        string TokenEncryptor(string token, string apiKey);
        string DecryptToken(string token, string apiKey);
        List<Claim> JwtTokenDecode(string token);
        Task<Guid> GetUserIdFromJwtToken(string token);
        //Task<TokenValidationResultViewModel> ValidateToken(string token);
        Task<string> CreateToken(User user);
        Task<bool> TokenExpireOrNot(string token);
    }
}
