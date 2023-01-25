using Business.Tool;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    ///  Controlador api/check
    /// </summary>    
    [RoutePrefix("api/check")]
    public class CheckController : ApiController
    {
        MessageVO messageVO = new MessageVO();
        ContentHTML contentHTML = new ContentHTML();
        

        /// <summary>
        /// Metodo para verificar conectividad al servicio
        /// </summary>
        /// <remarks>
        /// api/check/Check
        /// </remarks>
        /// <returns>MessageVO</returns>    
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Se ha verificado exitosamente", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Check")]
        [AllowAnonymous]
        public IHttpActionResult Check()
        {
            try
            {
                if (contentHTML.IsLoadDocumentHTML())
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("checkTitle"), contentHTML.GetInnerTextById("correctCheckMessage"));
                    return Content(HttpStatusCode.OK, messageVO);
                }
                else
                {
                    messageVO.SetMessage(0, "Verificacion de API", "Servicio no responde correctamente, funcionalidad no se ha ejecutado segun lo esperado");
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }                
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para verificar funcionalidad de autenticacion
        /// </summary>
        /// <remarks>
        /// api/check/CheckAuth
        /// </remarks>
        /// <returns>MessageVO</returns>  
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Se ha verificado exitosamente", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("CheckAuth")]
        [APIKeyAuth]
        public IHttpActionResult CheckAuth()
        {
            try
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("checkTitle"), contentHTML.GetInnerTextById("correctCheckMessage"));
                return Content(HttpStatusCode.OK, messageVO);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }
    }
}
