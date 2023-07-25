using DTOS;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPIObligatorio
{
    public class ManejadorJWT
    {
        public static string GenerarToken(UsuarioDTO usu)
        {
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler(); 


            byte[] clave = Encoding.ASCII.GetBytes("ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE="); 


            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {                                       
                    new Claim(ClaimTypes.Email, usu.Email),
                    new Claim("Id", usu.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMonths(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(clave),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
