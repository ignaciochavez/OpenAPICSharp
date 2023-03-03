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
    /// APIKeyAuthorizeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class APIKeyAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private MessageVO messageVO = new MessageVO();
        private ContentHTML contentHTML = new ContentHTML();

        private static bool GetAPIKeyHeader(System.Web.Http.Controllers.HttpActionContext actionContext, out string apiKeyHeader)
        {
            apiKeyHeader = null;
            IEnumerable<string> headers;
            if (!actionContext.Request.Headers.TryGetValues("Authorization", out headers) || headers.Count() > 1)
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
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("notAuthorizedTitle"), contentHTML.GetInnerTextById("notAuthorized"));
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, messageVO);
                }
                else
                {
                    string apiKey = Useful.GetAppSettings("APIKey");
                    if (apiKey != apiKeyHeader)
                    {
                        messageVO.SetMessage(0, contentHTML.GetInnerTextById("notAuthorizedTitle"), contentHTML.GetInnerTextById("notAuthorized"));
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, messageVO);
                    }
                }
                base.OnAuthorization(actionContext);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, messageVO);
                base.OnAuthorization(actionContext);
            }
        }
    }
}