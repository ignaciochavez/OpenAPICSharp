using Business.Tool;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace WebAPI.Configurations
{
    /// <summary>
    /// Clase TokenConfig
    /// </summary>
    public static class TokenConfig
    {
        /// <summary>
        /// Metodo para generar token
        /// </summary>
        /// <param name="rut">Rut</param>
        /// <returns>Retorna el objeto</returns>
        public static string GenerateToken(string rut)
        {
            string jWTSecretPassword = Useful.GetAppSettings("JWTSecretPassword");
            string jWTAudience = Useful.GetAppSettings("JWTAudience");
            string jWTIssuer = Useful.GetAppSettings("JWTIssuer");
            string jWTTimeMinutesExpires = Useful.GetAppSettings("JWTTimeMinutesExpires");

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jWTSecretPassword));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim("Rut", rut.Trim().ToUpper()) });

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: jWTAudience,
                issuer: jWTIssuer,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(jWTTimeMinutesExpires)),
                signingCredentials: signingCredentials);

            string jwtToken = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtToken;
        }
    }
}