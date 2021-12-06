using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SOL.Identity.Infrastructure.Security
{
    public class JwtValidator
    {
        private readonly SymmetricSecurityKey _key;

        public JwtValidator(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public bool ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var name = jwtToken.Claims.First(x => x.Type == "nameid").Value;
                if (name.Length > 0)
                {
                    return true;
                }
            }
            catch
            {
                // return null if validation fails
                return false;
            }

            return false;
        }
    }
}
