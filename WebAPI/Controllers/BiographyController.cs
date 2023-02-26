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
using WebAPI.Configurations;

namespace WebAPI.Controllers
{
    /// <summary>
    ///  Controlador api/biography
    /// </summary>    
    [TokenAuthorize]
    [RoutePrefix("api/biography")]
    public class BiographyController : ApiController
    {
        private MessageVO messageVO = new MessageVO();
        private ContentHTML contentHTML = new ContentHTML();

        /// <summary>
        /// Metodo para seleccionar Biography
        /// </summary>
        /// <remarks>
        /// api/biography/Select?id=1
        /// </remarks>
        /// <param name="id">Id Biography</param>
        /// <returns>Retorna el objeto</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(Biography))]
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

                var entity = BiographyImpl.Select(id);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para insertar Biography
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "FullName": "Ignacio Chavez",
        ///        "Gender": "M",
        ///        "Appearance": 2023-02-18T00:00:00.0000000-03:00,
        ///        "Alias": "Nacho",
        ///        "Publisher": "Marvel Comics"
        ///     }
        ///
        /// </remarks>
        /// <param name="biographyInsertDTO">Objeto BiographyInsertDTO</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado y retornado", typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Insert")]
        public IHttpActionResult Insert([FromBody] BiographyInsertDTO biographyInsertDTO)
        {
            try
            {
                if (biographyInsertDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "BiographyInsertDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (!string.IsNullOrWhiteSpace(biographyInsertDTO.FullName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "FullName"));
                else if (biographyInsertDTO.FullName.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "FullName").Replace("{1}", "50"));

                if (!string.IsNullOrWhiteSpace(biographyInsertDTO.Gender))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Gender"));
                else if (biographyInsertDTO.Gender.Trim().Length > 1)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Gender").Replace("{1}", "1"));

                if (biographyInsertDTO.Appearance == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "Appearance"));
                if (!Useful.ValidateDateTimeOffset(biographyInsertDTO.Appearance))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "Appearance"));
                else if (biographyInsertDTO.Appearance > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "Appearance"));

                if (!string.IsNullOrWhiteSpace(biographyInsertDTO.Alias))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Alias"));
                else if (biographyInsertDTO.Alias.Trim().Length > 200)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Alias").Replace("{1}", "200"));

                if (!string.IsNullOrWhiteSpace(biographyInsertDTO.Publisher))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Publisher"));
                else if (biographyInsertDTO.Publisher.Trim().Length > 25)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Publisher").Replace("{1}", "25"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var insert = BiographyImpl.Insert(biographyInsertDTO);
                return Content(HttpStatusCode.OK, insert);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para actualizar Biography
        /// </summary>
        /// <remarks>
        /// Request PUT:
        ///
        ///     {
        ///        "Id": 1,
        ///        "FullName": "Ignacio Chavez",
        ///        "Gender": "M",
        ///        "Appearance": 2023-02-18T00:00:00.0000000-03:00,
        ///        "Alias": "Nacho",
        ///        "Publisher": "Marvel Comics"
        ///     }
        ///
        /// </remarks>
        /// <param name="biography">Modelo Biography</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] Biography biography)
        {
            try
            {
                if (biography == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "Biography"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (biography.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (!string.IsNullOrWhiteSpace(biography.FullName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "FullName"));
                else if (biography.FullName.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "FullName").Replace("{1}", "50"));

                if (!string.IsNullOrWhiteSpace(biography.Gender))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Gender"));
                else if (biography.Gender.Trim().Length > 1)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Gender").Replace("{1}", "1"));

                if (biography.Appearance == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "Appearance"));
                if (!Useful.ValidateDateTimeOffset(biography.Appearance))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "Appearance"));
                else if (biography.Appearance > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "Appearance"));

                if (!string.IsNullOrWhiteSpace(biography.Alias))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Alias"));
                else if (biography.Alias.Trim().Length > 200)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Alias").Replace("{1}", "200"));

                if (!string.IsNullOrWhiteSpace(biography.Publisher))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Publisher"));
                else if (biography.Publisher.Trim().Length > 25)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Publisher").Replace("{1}", "25"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var update = BiographyImpl.Update(biography);
                return Content(HttpStatusCode.OK, update);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para eliminar entidad Biography
        /// </summary>
        /// <remarks>
        /// api/biography/Delete?id=1
        /// </remarks>
        /// <param name="id">Id Biography</param>
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

                var delete = BiographyImpl.Delete(id);
                return Content(HttpStatusCode.OK, delete);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para listar Biography
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "PageIndex": 1,
        ///        "PageSize": 10
        ///     }
        ///
        /// </remarks>
        /// <param name="listPaginatedDTO">Objeto ListPaginatedDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<User>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("ListPaginated")]
        public IHttpActionResult ListPaginated([FromBody] ListPaginatedDTO listPaginatedDTO)
        {
            try
            {
                if (listPaginatedDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ListPaginatedDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (listPaginatedDTO.PageIndex <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));

                if (listPaginatedDTO.PageSize <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                else if (listPaginatedDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLength").Replace("{0}", "PageSize").Replace("{1}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entitys = BiographyImpl.ListPaginated(listPaginatedDTO);
                return Content(HttpStatusCode.OK, entitys);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar registros de entidad Biography 
        /// </summary>
        /// <remarks>
        /// api/biography/TotalRecords
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
                var totalRecords = BiographyImpl.TotalRecords();
                return Content(HttpStatusCode.OK, totalRecords);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para buscar Biography
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Id": 1,
        ///        "FullName": "Ignacio Chavez",
        ///        "Gender": "M",
        ///        "Appearance": 2023-02-18T00:00:00.0000000-03:00,
        ///        "Alias": "Nacho",
        ///        "Publisher": "Marvel Comics",
        ///        "ListPaginatedDTO": {
        ///          "PageIndex": 1,
        ///          "PageSize": 10
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="biographySearchDTO">Objeto BiographySearchDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<Biography>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Search")]
        public IHttpActionResult Search([FromBody] BiographySearchDTO biographySearchDTO)
        {
            try
            {
                if (biographySearchDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "BiographySearchDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (biographySearchDTO.ListPaginatedDTO == null)
                {
                    messageVO.SetMessage(1, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ListPaginatedDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (biographySearchDTO.Id < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Id").Replace("{1}", "0"));

                if (!string.IsNullOrWhiteSpace(biographySearchDTO.FullName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "FullName"));
                else if (biographySearchDTO.FullName.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "FullName").Replace("{1}", "50"));

                if (!string.IsNullOrWhiteSpace(biographySearchDTO.Gender))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Gender"));
                else if (biographySearchDTO.Gender.Trim().Length > 1)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Gender").Replace("{1}", "1"));

                if (biographySearchDTO.Appearance == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "Appearance"));
                if (!Useful.ValidateDateTimeOffset(biographySearchDTO.Appearance))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "Appearance"));
                else if (biographySearchDTO.Appearance > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "Appearance"));

                if (!string.IsNullOrWhiteSpace(biographySearchDTO.Alias))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Alias"));
                else if (biographySearchDTO.Alias.Trim().Length > 200)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Alias").Replace("{1}", "200"));

                if (!string.IsNullOrWhiteSpace(biographySearchDTO.Publisher))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Publisher"));
                else if (biographySearchDTO.Publisher.Trim().Length > 25)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Publisher").Replace("{1}", "25"));
                
                if (biographySearchDTO.Id == 0 && string.IsNullOrWhiteSpace(biographySearchDTO.FullName) && string.IsNullOrWhiteSpace(biographySearchDTO.Gender) && biographySearchDTO.Appearance == null
                    && string.IsNullOrWhiteSpace(biographySearchDTO.Alias) && string.IsNullOrWhiteSpace(biographySearchDTO.Publisher))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNotInitialized").Replace("{0}", "Id, FullName, Gender, Appearance, Alias y Publisher,").Replace("{1}", "n").Replace("{2}", "s"));

                if (biographySearchDTO.ListPaginatedDTO.PageIndex <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));

                if (biographySearchDTO.ListPaginatedDTO.PageSize <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                else if (biographySearchDTO.ListPaginatedDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLength").Replace("{0}", "PageSize").Replace("{1}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entitys = BiographyImpl.Search(biographySearchDTO);
                return Content(HttpStatusCode.OK, entitys);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para retornar Excel
        /// </summary>
        /// <param name="TimeZoneInfoName">TimeZoneInfoName</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(FileDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Excel")]
        public IHttpActionResult Excel([FromBody] string TimeZoneInfoName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TimeZoneInfoName))
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "TimeZoneInfoName"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (TimeZoneInfoName.Length > 50)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "TimeZoneInfoName").Replace("{1}", "50"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (!Useful.ValidateTimeZoneInfo(TimeZoneInfoName))
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "TimeZoneInfoName"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var excel = BiographyImpl.Excel(TimeZoneInfoName);
                return Content(HttpStatusCode.OK, excel);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para retornar PDF
        /// </summary>
        /// <param name="TimeZoneInfoName">TimeZoneInfoName</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(FileDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("PDF")]
        public IHttpActionResult PDF([FromBody] string TimeZoneInfoName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TimeZoneInfoName))
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "TimeZoneInfoName"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (TimeZoneInfoName.Length > 50)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "TimeZoneInfoName").Replace("{1}", "50"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (!Useful.ValidateTimeZoneInfo(TimeZoneInfoName))
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "TimeZoneInfoName"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var pdf = BiographyImpl.PDF(TimeZoneInfoName);
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
