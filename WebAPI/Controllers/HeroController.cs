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
    ///  Controlador api/hero
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix("api/hero")]
    public class HeroController : ApiController
    {
        private MessageVO messageVO = new MessageVO();
        private ContentHTML contentHTML = new ContentHTML();

        /// <summary>
        /// Metodo para seleccionar Hero
        /// </summary>
        /// <remarks>
        /// api/hero/Select?id=1
        /// </remarks>
        /// <param name="id">Id Hero</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(Hero))]
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

                var entity = HeroImpl.Select(id);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para insertar Hero
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Name": "Ignacio Chavez",
        ///        "Description": "El heroe mas fuerte del planeta tierra",
        ///        "ImageBase64String": "data:image/png;base64,asdf1234",
        ///        "BiographyId": 1,
        ///        "PowerStatsId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="heroInsertDTO">Objeto HeroInsertDTO</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado y retornado", typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Insert")]
        public IHttpActionResult Insert([FromBody] HeroInsertDTO heroInsertDTO)
        {
            try
            {
                if (heroInsertDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "HeroInsertDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(heroInsertDTO.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                else if (heroInsertDTO.Name.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "50"));

                if (string.IsNullOrWhiteSpace(heroInsertDTO.Description))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Description"));
                else if (heroInsertDTO.Description.Trim().Length > 500)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Description").Replace("{1}", "500"));

                if (string.IsNullOrWhiteSpace(heroInsertDTO.ImageBase64String))
                {
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "ImageBase64String"));
                }
                else if (!Useful.ValidateBase64String(Useful.ReplaceConventionImageFromBase64String(heroInsertDTO.ImageBase64String)))
                {
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "ImageBase64String"));
                }
                else
                {

                    string[] arrayImgBase64String = heroInsertDTO.ImageBase64String.Split(',');
                    if (!Useful.ValidateIsImageBase64String(arrayImgBase64String[0]))
                        messageVO.Messages.Add(contentHTML.GetInnerTextById("formatMustBe").Replace("{0}", "ImageBase64String").Replace("{1}", "bmp, emf, exif, gif, icon, jpeg, jpg, png, tiff o wmf"));

                }
                
                if (heroInsertDTO.BiographyId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "BiographyId"));

                if (heroInsertDTO.PowerStatsId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PowerStatsId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existByName = HeroImpl.ExistByName(heroInsertDTO.Name);
                if (existByName)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityExistByParameter").Replace("{0}", "Hero").Replace("{1}", "Name"));

                bool existContact = BiographyImpl.ExistById(heroInsertDTO.BiographyId);
                if (!existContact)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Biography").Replace("{1}", "BiographyId"));

                bool existRole = PowerStatsImpl.ExistById(heroInsertDTO.PowerStatsId);
                if (!existRole)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "PowerStats").Replace("{1}", "PowerStatsId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var insert = HeroImpl.Insert(heroInsertDTO);
                return Content(HttpStatusCode.OK, insert);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para actualizar Hero
        /// </summary>
        /// <remarks>
        /// Request PUT:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Name": "Ignacio Chavez",
        ///        "Description": "El heroe mas fuerte del planeta tierra",
        ///        "ImageBase64String": "data:image/png;base64,asdf1234",
        ///        "BiographyId": 1,
        ///        "PowerStatsId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="heroUpdateDTO">Modelo HeroUpdateDTO</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] HeroUpdateDTO heroUpdateDTO)
        {
            try
            {
                if (heroUpdateDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "HeroUpdateDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (heroUpdateDTO.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (string.IsNullOrWhiteSpace(heroUpdateDTO.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                else if (heroUpdateDTO.Name.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "50"));

                if (string.IsNullOrWhiteSpace(heroUpdateDTO.Description))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Description"));
                else if (heroUpdateDTO.Description.Trim().Length > 500)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Description").Replace("{1}", "500"));

                if (string.IsNullOrWhiteSpace(heroUpdateDTO.ImageBase64String))
                {
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "ImageBase64String"));
                }
                else if (!Useful.ValidateBase64String(Useful.ReplaceConventionImageFromBase64String(heroUpdateDTO.ImageBase64String)))
                {
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "ImageBase64String"));
                }
                else
                {

                    string[] arrayImgBase64String = heroUpdateDTO.ImageBase64String.Split(',');
                    if (!Useful.ValidateIsImageBase64String(arrayImgBase64String[0]))
                        messageVO.Messages.Add(contentHTML.GetInnerTextById("formatMustBe").Replace("{0}", "ImageBase64String").Replace("{1}", "bmp, emf, exif, gif, icon, jpeg, jpg, png, tiff o wmf"));

                }

                if (heroUpdateDTO.BiographyId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "BiographyId"));

                if (heroUpdateDTO.PowerStatsId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PowerStatsId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existHero = HeroImpl.Exist(heroUpdateDTO.Id);
                if (!existHero)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Hero").Replace("{1}", "Id"));

                bool existByNameAndNotSameEntity = HeroImpl.ExistByNameAndNotSameEntity(new HeroExistByNameAndNotSameEntityDTO(heroUpdateDTO.Id, heroUpdateDTO.Name));
                if (existByNameAndNotSameEntity)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityExistByParameter").Replace("{0}", "Hero").Replace("{1}", "Name").Replace("{2}", "Hero"));

                bool existContact = BiographyImpl.ExistById(heroUpdateDTO.BiographyId);
                if (!existContact)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Biography").Replace("{1}", "BiographyId"));

                bool existRole = PowerStatsImpl.ExistById(heroUpdateDTO.PowerStatsId);
                if (!existRole)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "PowerStats").Replace("{1}", "PowerStatsId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var update = HeroImpl.Update(heroUpdateDTO);
                return Content(HttpStatusCode.OK, update);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para eliminar entidad Hero
        /// </summary>
        /// <remarks>
        /// api/hero/Delete?id=1
        /// </remarks>
        /// <param name="id">Id Hero</param>
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

                var delete = HeroImpl.Delete(id);
                return Content(HttpStatusCode.OK, delete);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para listar Hero
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
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<Hero>))]
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

                var entitys = HeroImpl.ListPaginated(listPaginatedDTO);
                return Content(HttpStatusCode.OK, entitys);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar registros de entidad Hero 
        /// </summary>
        /// <remarks>
        /// api/hero/TotalRecords
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
                var totalRecords = HeroImpl.TotalRecords();
                return Content(HttpStatusCode.OK, totalRecords);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para buscar Hero
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Name": "Ignacio Chavez",
        ///        "Description": "El heroe mas fuerte del planeta tierra",
        ///        "ImageBase64String": "data:image/png;base64,asdf1234",
        ///        "BiographyId": 1,
        ///        "PowerStatsId": 1,
        ///        "ListPaginatedDTO": {
        ///          "PageIndex": 1,
        ///          "PageSize": 10
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="heroSearchDTO">Objeto HeroSearchDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<User>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Search")]
        public IHttpActionResult Search([FromBody] HeroSearchDTO heroSearchDTO)
        {
            try
            {
                if (heroSearchDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "HeroSearchDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (heroSearchDTO.ListPaginatedDTO == null)
                {
                    messageVO.SetMessage(1, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ListPaginatedDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (heroSearchDTO.Id < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Id").Replace("{1}", "0"));

                if (!string.IsNullOrWhiteSpace(heroSearchDTO.Name) && heroSearchDTO.Name.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "50"));

                if (!string.IsNullOrWhiteSpace(heroSearchDTO.Description) && heroSearchDTO.Description.Trim().Length > 500)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Description").Replace("{1}", "500"));
                
                if (heroSearchDTO.BiographyId < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "BiographyId").Replace("{1}", "0"));

                if (heroSearchDTO.PowerStatsId < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "PowerStatsId").Replace("{1}", "0"));

                if (heroSearchDTO.Id == 0 && string.IsNullOrWhiteSpace(heroSearchDTO.Name) && string.IsNullOrWhiteSpace(heroSearchDTO.Description) && heroSearchDTO.BiographyId == 0
                    && heroSearchDTO.PowerStatsId == 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNotInitialized").Replace("{0}", "Id, Name, Description, BiographyId y PowerStatsId,").Replace("{1}", "n").Replace("{2}", "s"));
                
                if (heroSearchDTO.ListPaginatedDTO.PageIndex <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));

                if (heroSearchDTO.ListPaginatedDTO.PageSize <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                else if (heroSearchDTO.ListPaginatedDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLength").Replace("{0}", "PageSize").Replace("{1}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entitys = HeroImpl.Search(heroSearchDTO);
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

                var excel = HeroImpl.Excel(TimeZoneInfoName);
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

                var pdf = HeroImpl.PDF(TimeZoneInfoName);
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
