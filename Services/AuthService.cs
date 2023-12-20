using JwtAspNet.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAspNet.Services
{
    public class AuthService
    {
        //Method that will generate the Token
        public string CreateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            //Getting my privatekey and converting to Byte[]
            var key = Encoding.ASCII.GetBytes(Configuration.privateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2),
                Subject = GenerateClaims(user)

            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
   
        private static ClaimsIdentity GenerateClaims(User user)
        {
            ClaimsIdentity CI = new ();

            CI.AddClaim(new Claim("Id", user.Id.ToString()));
            CI.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            CI.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            CI.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            CI.AddClaim(new Claim("Image", user.Image));

            foreach (var role in user.Roles)
                CI.AddClaim(new Claim(ClaimTypes.Role, role));
            
            return CI;
        }
    }
}
