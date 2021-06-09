using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.Models;
namespace WavoProjects.Api.Helpers
{
    public class AuthenticationHelper
    {
        private JWTConfig m_jwtConfig;
        private WavoContext m_db;
        private ILogger<AuthenticationHelper> m_logger;

        public AuthenticationHelper(IOptionsMonitor<JWTConfig> jwtConfig, WavoContext db, ILogger<AuthenticationHelper> logger)
        {
            m_jwtConfig = jwtConfig.CurrentValue;
            m_db = db;
            m_logger = logger;

            jwtConfig.OnChange((newValue, x) =>
            {
                m_jwtConfig = newValue;
            });
        }

        public async Task<bool> ValidateCredentials(User user, string password)
        {
            bool enhancedValidated = BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password);

            if(!enhancedValidated)
            {
                bool legacyValidated = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (!legacyValidated)
                {
                    m_logger.LogError($"Login failed for {user.FullName}. Incorrect password");
                    return false;
                }

                m_logger.LogWarning($"Password for {user.FullName} uses the non-enhanced hashing. Updating the password to use enhanced hashing");
                user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
                await m_db.SaveChangesAsync();
            }

            return true;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public async Task<string> MintToken(int userId)
        {
            m_logger.LogInformation($"Generating Token for userId: {userId}");

            User user = await m_db.Users.SingleOrDefaultAsync(i => i.Id == userId);

            if (user == null)
            {
                m_logger.LogError($"User with Id {userId} not found");
                return null;
            }

            return MintToken(user);
        }

        public string MintToken(User user)
        {
            m_logger.LogInformation($"Generating Token for {user.FullName}");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(m_jwtConfig.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", user.Id.ToString()) }),
                Expires = DateTime.Now.AddSeconds(m_jwtConfig.Exp),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            m_logger.LogInformation($"Generating Token for {user.FullName} complete");
            return tokenHandler.WriteToken(token);
        }
        
        public async Task<User> DecodeToken(string token)
        {
            m_logger.LogDebug($"Decoding token: {token}");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(m_jwtConfig.Key);

            int? userId = null;
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
                userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);

                return await m_db.Users.SingleAsync(i => i.Id == userId);
            }
            catch (InvalidOperationException e)
            {
                m_logger.LogError($"Token is valid, but could not find user with Id: {userId}");
                return null;
            }
            catch (Exception e)
            {
                m_logger.LogError(e, $"Token is invalid");
                return null;
            }
            
        }
    }
}
