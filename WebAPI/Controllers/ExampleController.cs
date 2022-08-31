using Business.DTO;
using Business.Entity;
using Business.Implementation;
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
    ///  Controlador api/example
    /// </summary> 
    [RoutePrefix("api/example")]
    [APIKeyAuth]
    public class ExampleController : ApiController
    {
        MessageVO messageVO = new MessageVO();
        MessageHTML messageHTML = new MessageHTML();

        /// <summary>
        /// Metodo para seleccionar Example por rut
        /// </summary>
        /// <remarks>
        /// api/example/Select?rut=1-9
        /// </remarks>
        /// <param name="rut">Parametro rut</param>
        /// <returns>Entidad Example</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(Example))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Select")]
        public IHttpActionResult Select([FromUri] string rut)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rut))
                {
                    messageVO.SetMessage(0, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "rut"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (!Useful.ValidateRut(rut))
                {
                    messageVO.SetMessage(1, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "rut"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ExampleImpl.Select(rut);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, messageHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para verificar existencia de entidad Example por rut
        /// </summary>
        /// <remarks>
        /// api/example/Exist?rut=1-9
        /// </remarks>
        /// <param name="rut">Parametro rut</param>
        /// <returns>Verdadero o Falso</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto existe o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Exist")]
        public IHttpActionResult Exist([FromUri] string rut)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rut))
                {
                    messageVO.SetMessage(0, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "rut"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (!Useful.ValidateRut(rut))
                {
                    messageVO.SetMessage(1, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "rut"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var exist = ExampleImpl.Exist(rut);
                return Content(HttpStatusCode.OK, exist);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, messageHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para insertar Example
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Rut": "18-3",
        ///        "Name": "Emanuel",
        ///        "LastName": "Leiva",
        ///        "BirthDate": "1995-12-25T00:00:00.0000007-04:00",
        ///        "Active": true,
        ///        "Password": "4321rewq"
        ///     }
        ///
        /// </remarks>
        /// <param name="exampleInsertDTO">Objeto</param>
        /// <returns>Entidad Example</returns>    
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado y retornado", typeof(Example))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Insert")]
        public IHttpActionResult Insert([FromBody] ExampleInsertDTO exampleInsertDTO)
        {
            try
            {
                if (exampleInsertDTO == null)
                {
                    messageVO.SetMessage(0, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("nullObject").Replace("{0}", "ExampleInsertDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(exampleInsertDTO.Rut))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (!Useful.ValidateRut(exampleInsertDTO.Rut))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));
               
                if (string.IsNullOrWhiteSpace(exampleInsertDTO.Name))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                if (string.IsNullOrWhiteSpace(exampleInsertDTO.LastName))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "LastName"));
                if (!Useful.ValidateDateTimeOffset(exampleInsertDTO.BirthDate))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "BirthDate"));

                if (string.IsNullOrWhiteSpace(exampleInsertDTO.Password))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Password"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, messageHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (ExampleImpl.Exist(exampleInsertDTO.Rut))
                {
                    messageVO.SetMessage(2, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("entityExistByParameter").Replace("{0}", "Example").Replace("{1}", "Rut"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ExampleImpl.Insert(exampleInsertDTO);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, messageHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para actualizar Example
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Rut": "18-3",
        ///        "Name": "Emanuel",
        ///        "LastName": "Leiva",
        ///        "BirthDate": "1995-12-25T00:00:00.0000007-04:00",
        ///        "Active": true,
        ///        "Password": "vcxz7894"
        ///     }
        ///
        /// </remarks>
        /// <param name="exampleUpdateDTO">Objeto</param>
        /// <returns>Verdadero o Falso</returns>    
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] ExampleUpdateDTO exampleUpdateDTO)
        {
            try
            {
                if (exampleUpdateDTO == null)
                {
                    messageVO.SetMessage(0, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("nullObject").Replace("{0}", "ExampleUpdateDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(exampleUpdateDTO.Rut))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (!Useful.ValidateRut(exampleUpdateDTO.Rut))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));

                if (string.IsNullOrWhiteSpace(exampleUpdateDTO.Name))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                if (string.IsNullOrWhiteSpace(exampleUpdateDTO.LastName))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "LastName"));
                if (!Useful.ValidateDateTimeOffset(exampleUpdateDTO.BirthDate))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "BirthDate"));

                if (string.IsNullOrWhiteSpace(exampleUpdateDTO.Password))
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Password"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, messageHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                
                var update = ExampleImpl.Update(exampleUpdateDTO);
                return Content(HttpStatusCode.OK, update);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, messageHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para eliminar entidad Example por rut
        /// </summary>
        /// <remarks>
        /// api/example/Delete?rut=1-9
        /// </remarks>
        /// <param name="rut">Parametro rut</param>
        /// <returns>Verdadero o Falso</returns>
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido eliminado o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Delete")]
        public IHttpActionResult Delete([FromUri] string rut)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rut))
                {
                    messageVO.SetMessage(0, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("emptyParameters").Replace("{0}", "rut"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (!Useful.ValidateRut(rut))
                {
                    messageVO.SetMessage(1, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "rut"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var delete = ExampleImpl.Delete(rut);
                return Content(HttpStatusCode.OK, delete);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, messageHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para listar Example
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "PageIndex": 0,
        ///        "PageSize": 10
        ///     }
        ///
        /// </remarks>
        /// <param name="exampleListDTO">Objeto</param>
        /// <returns>Lista Entidad Example</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<Example>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("List")]
        public IHttpActionResult List([FromBody] ExampleListDTO exampleListDTO)
        {
            try
            {
                if (exampleListDTO == null)
                {
                    messageVO.SetMessage(0, messageHTML.GetInnerTextById("requeridTitle"), messageHTML.GetInnerTextById("nullObject").Replace("{0}", "ExampleListDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (exampleListDTO.PageIndex <= 0) 
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                if (exampleListDTO.PageSize <= 0 || exampleListDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(messageHTML.GetInnerTextById("parameterGreaterThanAndLessThan").Replace("{0}", "PageIndex").Replace("{1}", "0").Replace("{2}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, messageHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var list = ExampleImpl.List(exampleListDTO);
                return Content(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, messageHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar registros de entidad Example 
        /// </summary>
        /// <remarks>
        /// api/example/Count
        /// </remarks>
        /// <returns>Conteo de registros</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(long))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Count")]
        public IHttpActionResult Count()
        {
            try
            {
                var count = ExampleImpl.Count();
                return Content(HttpStatusCode.OK, count);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, messageHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }
    }
}
