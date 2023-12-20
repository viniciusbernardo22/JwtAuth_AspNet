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
            ClaimsIdentity ci = new ();

            ci.AddClaim(new Claim("id", user.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            ci.AddClaim(new Claim("image", user.Image));

            foreach (var role in user.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            
            return ci;
        }
    }
}
