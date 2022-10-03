using PYP_MediatorTask.DTOs.Token;
using PYP_MediatorTask.Interfaces;
using PYP_MediatorTask.Model;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PYP_MediatorTask.Services.Token
{
    public class TokenHadler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHadler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto CreateAccessToken(int expiration, AppUser user, IList<string> roles)
        {
            TokenDto token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
              new Claim (ClaimTypes.Name,user.UserName),
              new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            token.Expiration = DateTime.UtcNow.AddDays(expiration);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                claims: claims,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }

}
