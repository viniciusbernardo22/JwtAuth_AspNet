using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JwtAspNet.Services
{
    public class AuthService
    {
        //Method that will generate the Token
        public string CreateToken()
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

            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}
