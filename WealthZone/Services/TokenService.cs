using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WealthZone.Data.Interface;
using WealthZone.Models;

namespace WealthZone.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey key;
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]));

        }
        public string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credential,
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDecriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
