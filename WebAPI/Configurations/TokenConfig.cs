using Business.Tool;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;

namespace WebAPI
{
    /// <summary>
    /// Clase TokenConfig
    /// </summary>
    public static class TokenConfig
    {
        /// <summary>
        /// Metodo para generar token
        /// </summary>
        /// <param name="rutUser">Rut User</param>
        /// <returns>Retorna el objeto</returns>
        public static string GenerateToken(string rutUser)
        {
            string jWTSecretPassword = Useful.GetAppSettings("JWTSecretPassword");
            string jWTAudience = Useful.GetAppSettings("JWTAudience");
            string jWTIssuer = Useful.GetAppSettings("JWTIssuer");
            string jWTTimeMinutesExpires = Useful.GetAppSettings("JWTTimeMinutesExpires");

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jWTSecretPassword));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim("RutUser", rutUser.Trim().ToUpper()) });

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

        /// <summary>
        /// Metodo para recuperar el Rut del User
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public static string GetRutUserToken(HttpActionContext actionContext)
        {
            string token = null;
            IEnumerable<string> authzHeaders = actionContext.Request.Headers.GetValues("Authorization");            
            string element = authzHeaders.ElementAt(0);
            token = element.StartsWith("Bearer ") ? element.Substring(7) : element;
            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenUser = jwtHandler.ReadJwtToken(token);
            string roleName = tokenUser.Claims.FirstOrDefault(o => o.Type == "RutUser").Value;
            return roleName;
        }
    }
}