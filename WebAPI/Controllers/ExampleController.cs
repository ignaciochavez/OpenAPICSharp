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
        ContentHTML contentHTML = new ContentHTML();

        /// <summary>
        /// Metodo para seleccionar Example
        /// </summary>
        /// <remarks>
        /// api/example/Select?id=1
        /// </remarks>
        /// <param name="id">Id Example</param>
        /// <returns>Retorna el objeto</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(Example))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Select")]
        public IHttpActionResult Select([FromUri] int id)
        {
            try
            {
                if (id <= 0)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "id"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ExampleImpl.Select(id);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
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
        /// <param name="exampleInsertDTO">Modelo ExampleInsertDTO</param>
        /// <returns>Retorna el objeto</returns>    
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
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ExampleInsertDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(exampleInsertDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (!Useful.ValidateRut(exampleInsertDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));
               
                if (string.IsNullOrWhiteSpace(exampleInsertDTO.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                if (string.IsNullOrWhiteSpace(exampleInsertDTO.LastName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "LastName"));
                if (!Useful.ValidateDateTimeOffset(exampleInsertDTO.BirthDate))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "BirthDate"));

                if (string.IsNullOrWhiteSpace(exampleInsertDTO.Password))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Password"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existsEntity = ExampleImpl.ExistsByRut(exampleInsertDTO.Rut);
                if (existsEntity)
                {
                    messageVO.SetMessage(2, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("entityExistByParameter").Replace("{0}", "Example").Replace("{1}", "Rut"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ExampleImpl.Insert(exampleInsertDTO);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
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
        ///        "Id": 1,
        ///        "Rut": "18-3",
        ///        "Name": "Emanuel",
        ///        "LastName": "Leiva",
        ///        "BirthDate": "1995-12-25T00:00:00.0000007-04:00",
        ///        "Active": true,
        ///        "Password": "vcxz7894"
        ///     }
        ///
        /// </remarks>
        /// <param name="example">Modelo Example</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] Example example)
        {
            try
            {
                if (example == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "Example"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (example.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (string.IsNullOrWhiteSpace(example.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (!Useful.ValidateRut(example.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));

                if (string.IsNullOrWhiteSpace(example.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                if (string.IsNullOrWhiteSpace(example.LastName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "LastName"));
                if (!Useful.ValidateDateTimeOffset(example.BirthDate))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "BirthDate"));

                if (string.IsNullOrWhiteSpace(example.Password))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Password"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existByRutAndNotSameEntity = ExampleImpl.ExistByRutAndNotSameEntity(new ExampleExistByRutAndNotSameEntityDTO() { Id = example.Id, Rut = example.Rut });
                if (existByRutAndNotSameEntity)
                {
                    messageVO.SetMessage(2, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("entityExistsByParameterAndIsNotTheSameEntity").Replace("{0}", "Example").Replace("{1}", "Rut").Replace("{2}", "Example"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var update = ExampleImpl.Update(example);
                return Content(HttpStatusCode.OK, update);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para eliminar entidad Example
        /// </summary>
        /// <remarks>
        /// api/example/Delete?id=1
        /// </remarks>
        /// <param name="id">Id Example</param>
        /// <returns>Retorna el objeto</returns>
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido eliminado", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Delete")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                if (id <= 0)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "id"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var delete = ExampleImpl.Delete(id);
                return Content(HttpStatusCode.OK, delete);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
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
        /// <returns>Retorna el objeto</returns>
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
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ExampleListDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (exampleListDTO.PageIndex <= 0) 
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));

                if (exampleListDTO.PageSize <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                else if (exampleListDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("minimumParameterLength").Replace("{0}", "PageSize").Replace("{1}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entitys = ExampleImpl.List(exampleListDTO);
                return Content(HttpStatusCode.OK, entitys);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar registros de entidad Example 
        /// </summary>
        /// <remarks>
        /// api/example/TotalRecords
        /// </remarks>
        /// <returns>Retorna el objeto</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(long))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("TotalRecords")]
        public IHttpActionResult TotalRecords()
        {
            try
            {
                var totalRecords = ExampleImpl.TotalRecords();
                return Content(HttpStatusCode.OK, totalRecords);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para verificar si existe entidad Example, que no sea igual a la entidad actual por Id y Rut
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Rut": "1-9"
        ///     }
        ///
        /// </remarks>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("ExistByRutAndNotSameEntity")]
        public IHttpActionResult ExistByRutAndNotSameEntity([FromBody] ExampleExistByRutAndNotSameEntityDTO exampleExistByRutAndNotSameEntityDTO)
        {
            try
            {
                if (exampleExistByRutAndNotSameEntityDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ExampleExistByRutAndNotSameEntityDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (exampleExistByRutAndNotSameEntityDTO.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (string.IsNullOrWhiteSpace(exampleExistByRutAndNotSameEntityDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (!Useful.ValidateRut(exampleExistByRutAndNotSameEntityDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool exists = ExampleImpl.ExistByRutAndNotSameEntity(exampleExistByRutAndNotSameEntityDTO);
                return Content(HttpStatusCode.OK, exists);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar retornar Excel
        /// </summary>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(ExampleExcelDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Excel")]
        public IHttpActionResult Excel()
        {
            try
            {
                var excel = ExampleImpl.Excel();
                return Content(HttpStatusCode.OK, excel);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar retornar PDF
        /// </summary>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(ExamplePDFDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("PDF")]
        public IHttpActionResult PDF()
        {
            try
            {
                var pdf = ExampleImpl.PDF();
                return Content(HttpStatusCode.OK, pdf);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }
    }
}
