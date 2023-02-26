using Business.Tool;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.Configurations
{
    /// <summary>
    /// TokenAuthorizeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class TokenAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private MessageVO messageVO = new MessageVO();
        private ContentHTML contentHTML = new ContentHTML();

        private static bool GetToken(HttpActionContext actionContext, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;

            if (!actionContext.Request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
                return false;

            string element = authzHeaders.ElementAt(0);
            token = element.StartsWith("Bearer ") ? element.Substring(7) : element;
            return true;
        }

        /// <summary>
        /// Metodo para validar token
        /// </summary>
        /// <param name="actionContext">HttpActionContext</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string token;
            try
            {
                if (!GetToken(actionContext, out token))
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("notAuthorizedTitle"), contentHTML.GetInnerTextById("notAuthorized"));
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, messageVO);
                }
                else
                {
                    string tokenSecretPassword = Useful.GetAppSettings("JWTSecretPassword");
                    string tokenAudience = Useful.GetAppSettings("JWTAudience");
                    string tokenIssuer = Useful.GetAppSettings("JWTIssuer");

                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(tokenSecretPassword));
                    SecurityToken securityToken;
                    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = tokenAudience,
                        ValidateIssuer = true,
                        ValidIssuer = tokenIssuer,
                        ValidateLifetime = true,
                        LifetimeValidator = LifeTimeValidator,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = symmetricSecurityKey
                    };

                    Thread.CurrentPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                    HttpContext.Current.User = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                }
            }
            catch (SecurityTokenValidationException)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("notAuthorizedTitle"), contentHTML.GetInnerTextById("notAuthorized"));
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, messageVO);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, messageVO);
            }
            base.OnAuthorization(actionContext);
        }

        /// <summary>
        /// Metodo para validar tiempo de vida del token
        /// </summary>
        /// <param name="notBefore">NotBefore</param>
        /// <param name="expires">Expires</param>
        /// <param name="securityToken">SecurityToken</param>
        /// <param name="validationParameters">ValidationParameters</param>
        /// <returns>Retorna el objeto</returns>
        public bool LifeTimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires)
                    return true;
            }
            return false;
        }
    }
}