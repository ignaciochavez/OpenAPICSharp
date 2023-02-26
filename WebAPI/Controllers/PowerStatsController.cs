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
    ///  Controlador api/powerstats
    /// </summary>    
    [TokenAuthorize]
    [RoutePrefix("api/powerstats")]
    public class PowerStatsController : ApiController
    {
        private MessageVO messageVO = new MessageVO();
        private ContentHTML contentHTML = new ContentHTML();

        /// <summary>
        /// Metodo para seleccionar PowerStats
        /// </summary>
        /// <remarks>
        /// api/powerstats/Select?id=1
        /// </remarks>
        /// <param name="id">Id PowerStats</param>
        /// <returns>Retorna el objeto</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(PowerStats))]
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

                var entity = PowerStatsImpl.Select(id);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para insertar PowerStats
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Intelligence": 100,
        ///        "Strength": 100,
        ///        "Speed": 100,
        ///        "Durability": 100,
        ///        "Power": 100,
        ///        "Combat": 100
        ///     }
        ///
        /// </remarks>
        /// <param name="powerStatsInsertDTO">Objeto PowerStatsInsertDTO</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado y retornado", typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Insert")]
        public IHttpActionResult Insert([FromBody] PowerStatsInsertDTO powerStatsInsertDTO)
        {
            try
            {
                if (powerStatsInsertDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "PowerStatsInsertDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (powerStatsInsertDTO.Intelligence <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Intelligence"));

                if (powerStatsInsertDTO.Strength <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Strength"));

                if (powerStatsInsertDTO.Speed <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Speed"));

                if (powerStatsInsertDTO.Durability <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Durability"));

                if (powerStatsInsertDTO.Power <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Power"));

                if (powerStatsInsertDTO.Combat <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Combat"));
                
                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                
                var insert = PowerStatsImpl.Insert(powerStatsInsertDTO);
                return Content(HttpStatusCode.OK, insert);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para actualizar PowerStats
        /// </summary>
        /// <remarks>
        /// Request PUT:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Intelligence": 100,
        ///        "Strength": 100,
        ///        "Speed": 100,
        ///        "Durability": 100,
        ///        "Power": 100,
        ///        "Combat": 100
        ///     }
        ///
        /// </remarks>
        /// <param name="powerStats">Modelo PowerStats</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] PowerStats powerStats)
        {
            try
            {
                if (powerStats == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "PowerStats"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (powerStats.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (powerStats.Intelligence <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Intelligence"));

                if (powerStats.Strength <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Strength"));

                if (powerStats.Speed <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Speed"));

                if (powerStats.Durability <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Durability"));

                if (powerStats.Power <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Power"));

                if (powerStats.Combat <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Combat"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var update = PowerStatsImpl.Update(powerStats);
                return Content(HttpStatusCode.OK, update);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para eliminar entidad PowerStats
        /// </summary>
        /// <remarks>
        /// api/powerstats/Delete?id=1
        /// </remarks>
        /// <param name="id">Id PowerStats</param>
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

                var delete = PowerStatsImpl.Delete(id);
                return Content(HttpStatusCode.OK, delete);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para listar PowerStats
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
        [Route("List")]
        public IHttpActionResult List([FromBody] ListPaginatedDTO listPaginatedDTO)
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

                var entitys = PowerStatsImpl.ListPaginated(listPaginatedDTO);
                return Content(HttpStatusCode.OK, entitys);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar registros de entidad PowerStats 
        /// </summary>
        /// <remarks>
        /// api/powerstats/TotalRecords
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
                var totalRecords = PowerStatsImpl.TotalRecords();
                return Content(HttpStatusCode.OK, totalRecords);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para buscar PowerStats
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Intelligence": 100,
        ///        "Strength": 100,
        ///        "Speed": 100,
        ///        "Durability": 100,
        ///        "Power": 100,
        ///        "Combat": 100,
        ///        "ListPaginatedDTO": {
        ///          "PageIndex": 1,
        ///          "PageSize": 10
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="powerStatsSearchDTO">Objeto PowerStatsSearchDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<User>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Search")]
        public IHttpActionResult Search([FromBody] PowerStatsSearchDTO powerStatsSearchDTO)
        {
            try
            {
                if (powerStatsSearchDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "PowerStatsSearchDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (powerStatsSearchDTO.ListPaginatedDTO == null)
                {
                    messageVO.SetMessage(1, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ListPaginatedDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (powerStatsSearchDTO.Id < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Id"));

                if (powerStatsSearchDTO.Intelligence < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Intelligence"));

                if (powerStatsSearchDTO.Strength < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Strength"));

                if (powerStatsSearchDTO.Speed < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Speed"));

                if (powerStatsSearchDTO.Durability < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Durability"));

                if (powerStatsSearchDTO.Power < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Power"));

                if (powerStatsSearchDTO.Combat < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Combat"));

                if (powerStatsSearchDTO.Id == 0 && powerStatsSearchDTO.Intelligence == 0 && powerStatsSearchDTO.Strength == 0 && powerStatsSearchDTO.Speed == 0 && powerStatsSearchDTO.Durability == 0
                    && powerStatsSearchDTO.Power == 0 && powerStatsSearchDTO.Combat == 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNotInitialized").Replace("{0}", "Id, Intelligence, Strength, Speed, Durability, Power y Combat,").Replace("{1}", "n").Replace("{2}", "s"));
                
                if (powerStatsSearchDTO.ListPaginatedDTO.PageIndex <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));

                if (powerStatsSearchDTO.ListPaginatedDTO.PageSize <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                else if (powerStatsSearchDTO.ListPaginatedDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLength").Replace("{0}", "PageSize").Replace("{1}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entitys = PowerStatsImpl.Search(powerStatsSearchDTO);
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

                var excel = PowerStatsImpl.Excel(TimeZoneInfoName);
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

                var pdf = PowerStatsImpl.PDF(TimeZoneInfoName);
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
