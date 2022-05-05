using Business.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebAPI
{
    /// <summary>
    /// APIKeyAuthAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class APIKeyAuthAttribute : AuthorizationFilterAttribute
    {
        MessageVO messageVO = new MessageVO();
        MessageHTML messageHTML = new MessageHTML(Useful.GetMessagesDirectory());

        private static bool GetAPIKeyHeader(System.Web.Http.Controllers.HttpActionContext actionContext, out string apiKeyHeader)
        {
            apiKeyHeader = null;
            IEnumerable<string> headers;
            if (!actionContext.Request.Headers.TryGetValues("API-KEY", out headers) || headers.Count() > 1)
            {
                return false;
            }
            apiKeyHeader = headers.ElementAt(0);
            return true;
        }

        /// <summary>
        /// OnAuthorization
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string apiKeyHeader = string.Empty;

            try
            {
                if (!GetAPIKeyHeader(actionContext, out apiKeyHeader))
                {
                    messageVO.SetMessage(0, messageHTML.GetInnerTextById("notAuthorizedTitle"), messageHTML.GetInnerTextById("notAuthorized"));
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, messageVO);
                }
                else
                {
                    string[] authorization = apiKeyHeader.Split(' ');
                    if (authorization[0] != "Bearer")
                    {
                        messageVO.SetMessage(0, messageHTML.GetInnerTextById("notAuthorizedTitle"), messageHTML.GetInnerTextById("notAuthorized"));
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, messageVO);
                    }
                    else
                    {
                        string secretKey = Useful.GetAppSettings("SecretKey");
                        string encryptApiKey = Useful.ConvertSHA256(authorization[1]);
                        if (secretKey != encryptApiKey)
                        {
                            messageVO.SetMessage(0, messageHTML.GetInnerTextById("notAuthorizedTitle"), messageHTML.GetInnerTextById("notAuthorized"));
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, messageVO);
                        }
                    }
                }
                base.OnAuthorization(actionContext);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, messageHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, messageVO);
                base.OnAuthorization(actionContext);
            }
        }
    }
}