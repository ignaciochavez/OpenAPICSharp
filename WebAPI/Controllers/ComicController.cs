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
    ///  Controlador api/comic
    /// </summary>    
    [RoutePrefix("api/comic")]
    public class ComicController : ApiController
    {
        private MessageVO messageVO = new MessageVO();
        private ContentHTML contentHTML = new ContentHTML();

        /// <summary>
        /// Metodo para eliminar Hero
        /// </summary>
        /// <remarks>
        /// api/comic/DeleteHero?id=1
        /// </remarks>
        /// <param name="id">Id Hero</param>
        /// <returns>Retorna el objeto</returns>
        [HttpGet]
        [TokenAuthorize]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("DeleteHero")]
        public IHttpActionResult DeleteHero([FromUri] int id)
        {
            try
            {
                if (id <= 0)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "id"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ComicImpl.DeleteHero(id);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para eliminar User
        /// </summary>
        /// <remarks>
        /// api/comic/DeleteUser?id=1
        /// </remarks>
        /// <param name="id">Id User</param>
        /// <returns>Retorna el objeto</returns>
        [HttpGet]
        [TokenAuthorize]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("DeleteUser")]
        public IHttpActionResult DeleteUser([FromUri] int id)
        {
            try
            {
                if (id <= 0)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "id"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ComicImpl.DeleteUser(id);
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
        ///  <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Name": "Star-Lord",
        ///        "Description": "Poderes y habilidades. Star-Lord es un maestro estratega y solucionador de problemas que es un experto en combate cuerpo a cuerpo, varias armas de fuego humanas y alienígenas y técnicas de batalla . Tiene un amplio conocimiento de varias costumbres, sociedades y culturas alienígenas, y un conocimiento considerable sobre abstracciones cósmicas, como Oblivion.",
        ///        "ImageBase64String": "data:image/bmp;base64,1234asdf",
        ///        "FullName": "Peter Jason Quill",
        ///        "Gender": "M",
        ///        "Appearance": 1976-01-01T00:00:00.0000000-03:00,
        ///        "Alias": "Starlord",
        ///        "Publisher": "Marvel Comics",
        ///        "Intelligence": 69,
        ///        "Strength": 20,
        ///        "Speed": 33,
        ///        "Durability": 50,
        ///        "Power": 25,
        ///        "Combat": 70
        ///     }
        ///
        /// </remarks>
        /// <param name="comicInsertHeroDTO">Objeto ComicInsertHeroDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [TokenAuthorize]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("InsertHero")]
        public IHttpActionResult InsertHero([FromBody] ComicInsertHeroDTO comicInsertHeroDTO)
        {
            try
            {
                if (comicInsertHeroDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ComicInsertHeroDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(comicInsertHeroDTO.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                else if (comicInsertHeroDTO.Name.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "50"));

                if (string.IsNullOrWhiteSpace(comicInsertHeroDTO.Description))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Description"));
                else if (comicInsertHeroDTO.Description.Trim().Length > 500)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Description").Replace("{1}", "500"));

                if (string.IsNullOrWhiteSpace(comicInsertHeroDTO.ImageBase64String))
                {
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "ImageBase64String"));
                }
                else if (!Useful.ValidateBase64String(Useful.ReplaceConventionImageFromBase64String(comicInsertHeroDTO.ImageBase64String)))
                {
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "ImageBase64String"));
                }
                else
                {

                    string[] arrayImgBase64String = comicInsertHeroDTO.ImageBase64String.Split(',');
                    if (!Useful.ValidateIsImageBase64String(arrayImgBase64String[0]))
                        messageVO.Messages.Add(contentHTML.GetInnerTextById("formatMustBe").Replace("{0}", "ImageBase64String").Replace("{1}", "bmp, emf, exif, gif, icon, jpeg, jpg, png, tiff o wmf"));

                }

                if (!string.IsNullOrWhiteSpace(comicInsertHeroDTO.FullName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "FullName"));
                else if (comicInsertHeroDTO.FullName.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "FullName").Replace("{1}", "50"));

                if (!string.IsNullOrWhiteSpace(comicInsertHeroDTO.Gender))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Gender"));
                else if (comicInsertHeroDTO.Gender.Trim().Length > 1)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Gender").Replace("{1}", "1"));
                else if (comicInsertHeroDTO.Gender.ToLower() != "m" && comicInsertHeroDTO.Gender.ToLower() != "f")
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("formatMustBe").Replace("{0}", "Gender").Replace("{1}", "M o F"));

                if (comicInsertHeroDTO.Appearance == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "Appearance"));
                if (!Useful.ValidateDateTimeOffset(comicInsertHeroDTO.Appearance))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "Appearance"));
                else if (comicInsertHeroDTO.Appearance > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "Appearance"));

                if (!string.IsNullOrWhiteSpace(comicInsertHeroDTO.Alias))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Alias"));
                else if (comicInsertHeroDTO.Alias.Trim().Length > 500)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Alias").Replace("{1}", "500"));

                if (!string.IsNullOrWhiteSpace(comicInsertHeroDTO.Publisher))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Publisher"));
                else if (comicInsertHeroDTO.Publisher.Trim().Length > 25)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Publisher").Replace("{1}", "25"));
                
                if (comicInsertHeroDTO.Intelligence <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Intelligence"));

                if (comicInsertHeroDTO.Strength <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Strength"));

                if (comicInsertHeroDTO.Speed <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Speed"));

                if (comicInsertHeroDTO.Durability <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Durability"));

                if (comicInsertHeroDTO.Power <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Power"));

                if (comicInsertHeroDTO.Combat <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Combat"));
                
                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existByName = HeroImpl.ExistByName(comicInsertHeroDTO.Name);
                if (existByName)
                {
                    messageVO.SetMessage(2, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("entityExistByParameter").Replace("{0}", "User").Replace("{1}", "Rut"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ComicImpl.InsertHero(comicInsertHeroDTO);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para insertar User
        /// </summary>
        ///  <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Rut": "1-9",
        ///        "Name": "Joaquin",
        ///        "LastName": "Phoenix",
        ///        "BirthDate": 1974-10-28T00:00:00.0000000-03:00,
        ///        "Password": "123456",
        ///        "Active": true,
        ///        "Email": "joaquin.phoenix@gmail.com",
        ///        "Phone": "+11234567890",
        ///        "RoleId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="comicInsertUserDTO">Objeto ComicInsertUserDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [TokenAuthorize]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("InsertUser")]
        public IHttpActionResult InsertUser([FromBody] ComicInsertUserDTO comicInsertUserDTO)
        {
            try
            {
                if (comicInsertUserDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ComicInsertUserDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(comicInsertUserDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (comicInsertUserDTO.Rut.Trim().Length > 10)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Rut").Replace("{1}", "10"));
                else if (!Useful.ValidateRut(comicInsertUserDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));

                if (string.IsNullOrWhiteSpace(comicInsertUserDTO.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                else if (comicInsertUserDTO.Name.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "45"));

                if (string.IsNullOrWhiteSpace(comicInsertUserDTO.LastName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "LastName"));
                else if (comicInsertUserDTO.LastName.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "45"));

                if (string.IsNullOrWhiteSpace(comicInsertUserDTO.Password))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Password"));
                else if (comicInsertUserDTO.Password.Length > 16)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "16"));

                if (comicInsertUserDTO.BirthDate == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "BirthDate"));
                if (!Useful.ValidateDateTimeOffset(comicInsertUserDTO.BirthDate))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "BirthDate"));
                else if (comicInsertUserDTO.BirthDate > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "BirthDate"));

                if (comicInsertUserDTO.Active == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "Active"));

                if (string.IsNullOrWhiteSpace(comicInsertUserDTO.Email))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Email"));
                else if (comicInsertUserDTO.Email.Trim().Length > 15)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Email").Replace("{1}", "15"));
                else if (!Useful.ValidateEmail(comicInsertUserDTO.Email))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Email"));

                if (string.IsNullOrWhiteSpace(comicInsertUserDTO.Phone))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Phone"));
                else if (comicInsertUserDTO.Phone.Trim().Length > 15)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Phone").Replace("{1}", "15"));
                else if (!Useful.ValidatePhone(comicInsertUserDTO.Phone))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Phone"));

                if (comicInsertUserDTO.RoleId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "RoleId"));
                
                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existUserByRut = UserImpl.ExistByRut(comicInsertUserDTO.Rut);
                if (existUserByRut)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityExistByParameter").Replace("{0}", "User").Replace("{1}", "Rut"));
                
                bool existRole = RoleImpl.ExistById(comicInsertUserDTO.RoleId);
                if (!existRole)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Role").Replace("{1}", "RoleId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ComicImpl.InsertUser(comicInsertUserDTO);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para Login
        /// </summary>
        ///  <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Rut": "1-9",
        ///        "Password": "123456"
        ///     }
        ///
        /// </remarks>
        /// <param name="comicLoginDTO">Objeto ComicLoginDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [APIKeyAuthorize]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] ComicLoginDTO comicLoginDTO)
        {
            try
            {
                if (comicLoginDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ComicLoginDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(comicLoginDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (comicLoginDTO.Rut.Trim().Length > 10)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Rut").Replace("{1}", "10"));
                else if (!Useful.ValidateRut(comicLoginDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));

                if (string.IsNullOrWhiteSpace(comicLoginDTO.Password))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Password"));
                else if (comicLoginDTO.Password.Length > 16)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "16"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var login = ComicImpl.Login(comicLoginDTO);
                if (login.Id != 3)
                {
                    messageVO = login;
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                string token = TokenConfig.GenerateToken(comicLoginDTO.Rut);
                return Content(HttpStatusCode.OK, token);

            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para seleccionar Hero
        /// </summary>
        /// <remarks>
        /// api/comic/SelectHero?id=1
        /// </remarks>
        /// <param name="id">Id Hero</param>
        /// <returns>Retorna el objeto</returns>
        [HttpGet]
        [TokenAuthorize]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(SelectHero))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("SelectHero")]
        public IHttpActionResult SelectHero([FromUri] int id)
        {
            try
            {
                if (id <= 0)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "id"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ComicImpl.SelectHero(id);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para seleccionar User
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Id": 1,
        ///        "TimeZoneInfoName": "Pacific SA Standard Time"
        ///     }
        ///
        /// </remarks>
        /// <param name="comicSelectUserDTO">Objeto ComicSelectUserDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [TokenAuthorize]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(SelectUser))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("SelectUser")]
        public IHttpActionResult SelectUser([FromBody] ComicSelectUserDTO comicSelectUserDTO)
        {
            try
            {
                if (comicSelectUserDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ComicSelectUserDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (comicSelectUserDTO.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (string.IsNullOrWhiteSpace(comicSelectUserDTO.TimeZoneInfoName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "TimeZoneInfoName"));
                else if (comicSelectUserDTO.TimeZoneInfoName.Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "TimeZoneInfoName").Replace("{1}", "50"));
                else if (!Useful.ValidateTimeZoneInfo(comicSelectUserDTO.TimeZoneInfoName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "TimeZoneInfoName"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ComicImpl.SelectUser(comicSelectUserDTO);
                return Content(HttpStatusCode.OK, entity);
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
        ///  <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "HeroId": 1,
        ///        "Name": "Star-Lord",
        ///        "Description": "Poderes y habilidades. Star-Lord es un maestro estratega y solucionador de problemas que es un experto en combate cuerpo a cuerpo, varias armas de fuego humanas y alienígenas y técnicas de batalla . Tiene un amplio conocimiento de varias costumbres, sociedades y culturas alienígenas, y un conocimiento considerable sobre abstracciones cósmicas, como Oblivion.",
        ///        "ImageBase64String": "data:image/bmp;base64,1234asdf",
        ///        "BiographyId": 1,
        ///        "FullName": "Peter Jason Quill",
        ///        "Gender": "M",
        ///        "Appearance": 1976-01-01T00:00:00.0000000-03:00,
        ///        "Alias": "Starlord",
        ///        "Publisher": "Marvel Comics",
        ///        "PowerStatsId": 1,
        ///        "Intelligence": 69,
        ///        "Strength": 20,
        ///        "Speed": 33,
        ///        "Durability": 50,
        ///        "Power": 25,
        ///        "Combat": 70
        ///     }
        ///
        /// </remarks>
        /// <param name="comicUpdateHeroDTO">Objeto ComicUpdateHeroDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [TokenAuthorize]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("UpdateHero")]
        public IHttpActionResult UpdateHero([FromBody] ComicUpdateHeroDTO comicUpdateHeroDTO)
        {
            try
            {
                if (comicUpdateHeroDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ComicInsertHeroDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (comicUpdateHeroDTO.HeroId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "HeroId"));

                if (string.IsNullOrWhiteSpace(comicUpdateHeroDTO.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                else if (comicUpdateHeroDTO.Name.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "50"));

                if (string.IsNullOrWhiteSpace(comicUpdateHeroDTO.Description))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Description"));
                else if (comicUpdateHeroDTO.Description.Trim().Length > 500)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Description").Replace("{1}", "500"));

                if (string.IsNullOrWhiteSpace(comicUpdateHeroDTO.ImageBase64String))
                {
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "ImageBase64String"));
                }
                else if (!Useful.ValidateBase64String(Useful.ReplaceConventionImageFromBase64String(comicUpdateHeroDTO.ImageBase64String)))
                {
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "ImageBase64String"));
                }
                else
                {

                    string[] arrayImgBase64String = comicUpdateHeroDTO.ImageBase64String.Split(',');
                    if (!Useful.ValidateIsImageBase64String(arrayImgBase64String[0]))
                        messageVO.Messages.Add(contentHTML.GetInnerTextById("formatMustBe").Replace("{0}", "ImageBase64String").Replace("{1}", "bmp, emf, exif, gif, icon, jpeg, jpg, png, tiff o wmf"));

                }

                if (comicUpdateHeroDTO.BiographyId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "BiographyId"));
                
                if (!string.IsNullOrWhiteSpace(comicUpdateHeroDTO.FullName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "FullName"));
                else if (comicUpdateHeroDTO.FullName.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "FullName").Replace("{1}", "50"));

                if (!string.IsNullOrWhiteSpace(comicUpdateHeroDTO.Gender))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Gender"));
                else if (comicUpdateHeroDTO.Gender.Trim().Length > 1)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Gender").Replace("{1}", "1"));
                else if (comicUpdateHeroDTO.Gender.ToLower() != "m" && comicUpdateHeroDTO.Gender.ToLower() != "f")
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("formatMustBe").Replace("{0}", "Gender").Replace("{1}", "M o F"));

                if (comicUpdateHeroDTO.Appearance == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "Appearance"));
                if (!Useful.ValidateDateTimeOffset(comicUpdateHeroDTO.Appearance))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "Appearance"));
                else if (comicUpdateHeroDTO.Appearance > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "Appearance"));

                if (!string.IsNullOrWhiteSpace(comicUpdateHeroDTO.Alias))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Alias"));
                else if (comicUpdateHeroDTO.Alias.Trim().Length > 500)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Alias").Replace("{1}", "500"));

                if (!string.IsNullOrWhiteSpace(comicUpdateHeroDTO.Publisher))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Publisher"));
                else if (comicUpdateHeroDTO.Publisher.Trim().Length > 25)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Publisher").Replace("{1}", "25"));

                if (comicUpdateHeroDTO.PowerStatsId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PowerStatsId"));
                
                if (comicUpdateHeroDTO.Intelligence <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Intelligence"));

                if (comicUpdateHeroDTO.Strength <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Strength"));

                if (comicUpdateHeroDTO.Speed <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Speed"));

                if (comicUpdateHeroDTO.Durability <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Durability"));

                if (comicUpdateHeroDTO.Power <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Power"));

                if (comicUpdateHeroDTO.Combat <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Combat"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existHero = HeroImpl.Exist(comicUpdateHeroDTO.HeroId);
                if (!existHero)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Hero").Replace("{1}", "HeroId"));

                bool existByNameAndNotSameEntity = HeroImpl.ExistByNameAndNotSameEntity(new HeroExistByNameAndNotSameEntityDTO(comicUpdateHeroDTO.HeroId, comicUpdateHeroDTO.Name));
                if (existByNameAndNotSameEntity)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityExistByParameter").Replace("{0}", "Hero").Replace("{1}", "Name").Replace("{2}", "Hero"));

                bool existContact = BiographyImpl.ExistById(comicUpdateHeroDTO.BiographyId);
                if (!existContact)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Biography").Replace("{1}", "BiographyId"));

                bool existRole = PowerStatsImpl.ExistById(comicUpdateHeroDTO.PowerStatsId);
                if (!existRole)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "PowerStats").Replace("{1}", "PowerStatsId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ComicImpl.UpdateHero(comicUpdateHeroDTO);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para actualizar User
        /// </summary>
        ///  <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "UserId": 1,
        ///        "Rut": "1-9",
        ///        "Name": "Joaquin",
        ///        "LastName": "Phoenix",
        ///        "BirthDate": 1974-10-28T00:00:00.0000000-03:00,
        ///        "Password": "123456",
        ///        "Active": true,
        ///        "ContactId": 1,
        ///        "Email": "joaquin.phoenix@gmail.com",
        ///        "Phone": "+11234567890",
        ///        "RoleId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="comicUpdateUserDTO">Objeto ComicUpdateUserDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [TokenAuthorize]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("UpdateUser")]
        public IHttpActionResult UpdateUser([FromBody] ComicUpdateUserDTO comicUpdateUserDTO)
        {
            try
            {
                if (comicUpdateUserDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ComicUpdateUserDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (comicUpdateUserDTO.UserId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "UserId"));
                
                if (string.IsNullOrWhiteSpace(comicUpdateUserDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (comicUpdateUserDTO.Rut.Trim().Length > 10)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Rut").Replace("{1}", "10"));
                else if (!Useful.ValidateRut(comicUpdateUserDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));

                if (string.IsNullOrWhiteSpace(comicUpdateUserDTO.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                else if (comicUpdateUserDTO.Name.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "45"));

                if (string.IsNullOrWhiteSpace(comicUpdateUserDTO.LastName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "LastName"));
                else if (comicUpdateUserDTO.LastName.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "45"));

                if (string.IsNullOrWhiteSpace(comicUpdateUserDTO.Password))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Password"));
                else if (comicUpdateUserDTO.Password.Length > 16)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "16"));

                if (comicUpdateUserDTO.BirthDate == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "BirthDate"));
                if (!Useful.ValidateDateTimeOffset(comicUpdateUserDTO.BirthDate))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "BirthDate"));
                else if (comicUpdateUserDTO.BirthDate > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "BirthDate"));

                if (comicUpdateUserDTO.Active == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "Active"));

                if (comicUpdateUserDTO.ContactId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "ContactId"));

                if (string.IsNullOrWhiteSpace(comicUpdateUserDTO.Email))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Email"));
                else if (comicUpdateUserDTO.Email.Trim().Length > 15)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Email").Replace("{1}", "15"));
                else if (!Useful.ValidateEmail(comicUpdateUserDTO.Email))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Email"));

                if (string.IsNullOrWhiteSpace(comicUpdateUserDTO.Phone))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Phone"));
                else if (comicUpdateUserDTO.Phone.Trim().Length > 15)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Phone").Replace("{1}", "15"));
                else if (!Useful.ValidatePhone(comicUpdateUserDTO.Phone))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Phone"));

                if (comicUpdateUserDTO.RoleId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "RoleId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existUser = UserImpl.Exist(comicUpdateUserDTO.UserId);
                if (!existUser)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "User").Replace("{1}", "UserId"));
                
                bool existUserByRutAndNotSameEntity = UserImpl.ExistByRutAndNotSameEntity(new UserExistByRutAndNotSameEntityDTO(comicUpdateUserDTO.UserId, comicUpdateUserDTO.Rut));
                if (existUserByRutAndNotSameEntity)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityExistsByParameterAndIsNotTheSameEntity").Replace("{0}", "User").Replace("{1}", "Rut").Replace("{2}", "User"));

                bool existContact = ContactImpl.ExistById(comicUpdateUserDTO.ContactId);
                if (!existContact)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Contact").Replace("{1}", "ContactId"));

                bool existRole = RoleImpl.ExistById(comicUpdateUserDTO.RoleId);
                if (!existRole)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Role").Replace("{1}", "RoleId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = ComicImpl.UpdateUser(comicUpdateUserDTO);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }
    }
}
