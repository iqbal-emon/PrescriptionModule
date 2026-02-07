using Entities.EntityClass;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SharedService.JWTTokenService
{
    public class TokenService : ITokenService
    {
        private static readonly byte[] key = Encoding.UTF8.GetBytes("Agfd11384HSOTITYH@84584DHFDgsdg3746$$FGDSF7hgdh");
        private static readonly byte[] iv = Encoding.UTF8.GetBytes("my initialization vector");
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateToken(User user)
        {
            string expireTime = _configuration.GetSection("AppSettings").GetSection("TokenExpireTimeInSecound").Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.PrimarySid, user.UserID.ToString()),
                     new Claim(ClaimTypes.Email, user.Email)
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                //Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public string DecryptToken(string token, string apiKey)
        {

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Encrypted token cannot be null or empty.");
            }
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(apiKey);
                using (var generator = RandomNumberGenerator.Create())
                {
                    generator.GetBytes(key);
                }

                byte[] cipherText = Convert.FromBase64String(token);
                using Aes aes = Aes.Create();
                aes.Key = key;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                using MemoryStream msDecrypt = new MemoryStream(cipherText);
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);

                return srDecrypt.ReadToEnd();
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("The encrypted token is not in a valid format.", ex);
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while decrypting the token.", ex);
            }

        }

        public async Task<Guid> GetUserIdFromJwtToken(string token)
        {
            string apiKey = _configuration.GetSection("AppSettings").GetSection("Token").Value;
            var tokenValidationResult = Guid.Empty;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(apiKey);

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(apiKey))
            {
                tokenValidationResult = Guid.Empty;
            }
            else
            {
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var expires = jwtToken.ValidTo;
                    if (expires > DateTime.UtcNow)
                    {
                        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "primarysid");
                        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                        {
                            tokenValidationResult = userId;
                        }
                    }
                    else
                    {
                        tokenValidationResult = Guid.Empty;
                    }
                }
                catch (SecurityTokenExpiredException)
                {
                    tokenValidationResult = Guid.Empty;
                }
                catch
                {
                    // Other exceptions are handled here
                    tokenValidationResult = Guid.Empty;
                }

            }
            return tokenValidationResult;
        }

        public List<Claim> JwtTokenDecode(string token)
        {
            string decodeToken = token;
            // Decode the JWT token
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadJwtToken(token);
            // Access claims
            var claims = tokenS.Claims;

            return claims.ToList();
        }

        public string TokenEncryptor(string token, string apiKey)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token cannot be null or empty.");
            }

            byte[] key = Encoding.UTF8.GetBytes(apiKey);
            byte[] iv = new byte[16]; // The IV is always 16 bytes for AES

            try
            {
                using (var generator = RandomNumberGenerator.Create())
                {
                    generator.GetBytes(key);
                }

                using Aes aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using StreamWriter swEncrypt = new StreamWriter(csEncrypt);
                swEncrypt.Write(token);
                csEncrypt.FlushFinalBlock();
                byte[] encrypted = msEncrypt.ToArray();
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while encrypting the token.", ex);
            }
        }


        //public async Task<TokenValidationResultViewModel> ValidateToken(string token)
        //{
        //    string apiKey = _configuration.GetSection("AppSettings").GetSection("Token").Value;
        //    var tokenValidationResult = new TokenValidationResultViewModel();
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(apiKey);

        //    if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(apiKey))
        //    {
        //        tokenValidationResult.token = null;
        //        tokenValidationResult.user_id = null;
        //        tokenValidationResult.user_logged_in = false;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            tokenHandler.ValidateToken(token, new TokenValidationParameters
        //            {
        //                ValidateIssuerSigningKey = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(key),
        //                ValidateIssuer = false,
        //                ValidateAudience = false,
        //                ClockSkew = TimeSpan.Zero
        //            }, out SecurityToken validatedToken);

        //            var jwtToken = (JwtSecurityToken)validatedToken;
        //            var expires = jwtToken.ValidTo;
        //            if (expires < DateTime.UtcNow)
        //            {
        //                // The token has expired, so refresh it
        //                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "primarysid");
        //                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
        //                {
        //                    tokenValidationResult.user_id = userId;
        //                }

        //                var userInfo = await _userInfoService.GetById(tokenValidationResult.user_id.ToString());

        //                if (userInfo != null)
        //                {
        //                    var newJwtToken = CreateToken(userInfo.Result);
        //                    tokenValidationResult.user_logged_in = true;
        //                    tokenValidationResult.token = newJwtToken.Result;
        //                }
        //                else
        //                {
        //                    tokenValidationResult.user_logged_in = false;
        //                    tokenValidationResult.token = null;
        //                }
        //            }
        //            else
        //            {
        //                // The token is still valid, so extract user ID from token
        //                //var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "primarysid");
        //                //if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
        //                //{
        //                //	tokenValidationResult.user_id = userId;
        //                //}

        //                tokenValidationResult.user_logged_in = true;
        //                tokenValidationResult.token = token;
        //            }
        //        }
        //        catch (SecurityTokenExpiredException)
        //        {
        //            //// The token has expired, so refresh it
        //            //var tokenS = tokenHandler.ReadJwtToken(token);
        //            //var userIdClaim = tokenS.Claims.FirstOrDefault(c => c.Type == "primarysid");
        //            //if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
        //            //{
        //            //	tokenValidationResult.user_id = userId;
        //            //}
        //            //var userInfo = await _userInfoService.GetById(tokenValidationResult.user_id.ToString());

        //            //if (userInfo != null)
        //            //{
        //            //	var newJwtToken = CreateToken(userInfo.Result);
        //            //	tokenValidationResult.user_logged_in = true;
        //            //	tokenValidationResult.token = newJwtToken;
        //            //}
        //            //else
        //            //{
        //            tokenValidationResult.user_logged_in = false;
        //            tokenValidationResult.token = null;
        //            //}
        //        }
        //        catch
        //        {
        //            // Other exceptions are handled here
        //            tokenValidationResult.user_logged_in = false;
        //            tokenValidationResult.token = null;
        //        }

        //    }
        //    return tokenValidationResult;
        //}

        public async Task<bool> TokenExpireOrNot(string token)
        {
            string apiKey = _configuration.GetSection("AppSettings").GetSection("Token").Value;
            bool tokenValidationResult = false;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(apiKey);

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(apiKey))
            {
                tokenValidationResult = false;
            }
            else
            {
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var expires = jwtToken.ValidTo;
                    if (expires < DateTime.UtcNow)
                    {
                        tokenValidationResult = false;
                    }
                    else
                    {
                        tokenValidationResult = true;

                    }
                }
                catch (SecurityTokenExpiredException)
                {
                    tokenValidationResult = false;
                }
            }
            return tokenValidationResult;
        }

        //public string TokenEncryptor(string token, string apiKey)
        //{
        //	if (string.IsNullOrEmpty(token))
        //	{
        //		throw new ArgumentException("Token cannot be null or empty.");
        //	}
        //	try
        //	{
        //		using Aes aes = Aes.Create();
        //		aes.Key = key;
        //		aes.IV = iv; 
        //		ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV); 
        //		using MemoryStream msEncrypt = new MemoryStream();
        //		using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        //		using StreamWriter swEncrypt = new StreamWriter(csEncrypt); swEncrypt.Write(token); csEncrypt.FlushFinalBlock(); 
        //		byte[] encrypted = msEncrypt.ToArray(); 
        //		return Convert.ToBase64String(encrypted);
        //	}
        //	catch (Exception ex)
        //	{
        //		throw new Exception("An error occurred while encrypting the token.", ex);
        //	}
        //}
    }
}
