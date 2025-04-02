using BAITZ_BLOG_API.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BAITZ_BLOG_API.Auth
{
    
    public class TokenAuth
    {
        public static object GenerateToken(Client client)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("ClientId", client.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                Issuer = "BAITZ_BLOG_API",
                Audience = "BAITZ_BLOG_API_Domain",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);
            return new { token = tokenString };
        }
    }
}
