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
    ///  Controlador api/user
    /// </summary>    
    [TokenAuthorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private MessageVO messageVO = new MessageVO();
        private ContentHTML contentHTML = new ContentHTML();

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
        /// <param name="userSelectDTO">Modelo UserSelectDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Select")]
        public IHttpActionResult Select([FromBody] UserSelectDTO userSelectDTO)
        {
            try
            {
                if (userSelectDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "UserSelectDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (userSelectDTO.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (string.IsNullOrWhiteSpace(userSelectDTO.TimeZoneInfoName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "TimeZoneInfoName"));
                else if (userSelectDTO.TimeZoneInfoName.Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "TimeZoneInfoName").Replace("{1}", "50"));
                else if (!Useful.ValidateTimeZoneInfo(userSelectDTO.TimeZoneInfoName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "TimeZoneInfoName"));
                
                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entity = UserImpl.Select(userSelectDTO);
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
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Rut": "1-9",
        ///        "Name": "Ignacio",
        ///        "LastName": "Chavez",
        ///        "BirthDate": "1990-01-01T00:00:00.0000000-00:00",
        ///        "Password": "123456",
        ///        "Active": true,
        ///        "ContactId": 1,
        ///        "RoleId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="userInsertDTO">Objeto UserInsertDTO</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado y retornado", typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Insert")]
        public IHttpActionResult Insert([FromBody] UserInsertDTO userInsertDTO)
        {
            try
            {
                if (userInsertDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ContactInsertDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(userInsertDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (userInsertDTO.Rut.Trim().Length > 10)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Rut").Replace("{1}", "10"));
                else if (!Useful.ValidateRut(userInsertDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));

                if (string.IsNullOrWhiteSpace(userInsertDTO.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                else if (userInsertDTO.Name.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "45"));

                if (string.IsNullOrWhiteSpace(userInsertDTO.LastName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "LastName"));
                else if (userInsertDTO.LastName.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "45"));

                if (string.IsNullOrWhiteSpace(userInsertDTO.Password))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Password"));
                else if (userInsertDTO.Password.Length > 16)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "16"));

                if (userInsertDTO.BirthDate == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "BirthDate"));
                if (!Useful.ValidateDateTimeOffset(userInsertDTO.BirthDate))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "BirthDate"));
                else if (userInsertDTO.BirthDate > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "BirthDate"));

                if (userInsertDTO.Active == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "Active"));

                if (userInsertDTO.ContactId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "ContactId"));

                if (userInsertDTO.RoleId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "RoleId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existUserByRut = UserImpl.ExistByRut(userInsertDTO.Rut);
                if (existUserByRut)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityExistByParameter").Replace("{0}", "User").Replace("{1}", "Rut"));

                bool existContact = ContactImpl.ExistById(userInsertDTO.ContactId);
                if (!existContact)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Contact").Replace("{1}", "ContactId"));

                bool existRole = RoleImpl.ExistById(userInsertDTO.RoleId);
                if (!existRole)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Role").Replace("{1}", "RoleId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var insert = UserImpl.Insert(userInsertDTO);
                return Content(HttpStatusCode.OK, insert);
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
        /// <remarks>
        /// Request PUT:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Rut": "1-9",
        ///        "Name": "Ignacio",
        ///        "LastName": "Chavez",
        ///        "BirthDate": "1990-01-01T00:00:00.0000000-00:00",
        ///        "Password": "123456",
        ///        "Active": true,
        ///        "ContactId": 1,
        ///        "RoleId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="userUpdateDTO">Modelo UserUpdateDTO</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] UserUpdateDTO userUpdateDTO)
        {
            try
            {
                if (userUpdateDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "UserUpdateDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (userUpdateDTO.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (string.IsNullOrWhiteSpace(userUpdateDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Rut"));
                else if (userUpdateDTO.Rut.Trim().Length > 10)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Rut").Replace("{1}", "10"));
                else if (!Useful.ValidateRut(userUpdateDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));

                if (string.IsNullOrWhiteSpace(userUpdateDTO.Name))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Name"));
                else if (userUpdateDTO.Name.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "45"));

                if (string.IsNullOrWhiteSpace(userUpdateDTO.LastName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "LastName"));
                else if (userUpdateDTO.LastName.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "45"));

                if (string.IsNullOrWhiteSpace(userUpdateDTO.Password))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Password"));
                else if (userUpdateDTO.Password.Length > 16)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "16"));

                if (userUpdateDTO.BirthDate == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "BirthDate"));
                if (!Useful.ValidateDateTimeOffset(userUpdateDTO.BirthDate))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "BirthDate"));
                else if (userUpdateDTO.BirthDate > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "BirthDate"));

                if (userUpdateDTO.Active == null)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNull").Replace("{0}", "Active"));

                if (userUpdateDTO.ContactId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "ContactId"));

                if (userUpdateDTO.RoleId <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "RoleId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existUserByRutAndNotSameEntity = UserImpl.ExistByRutAndNotSameEntity(new UserExistByRutAndNotSameEntityDTO(userUpdateDTO.Id, userUpdateDTO.Rut));
                if (existUserByRutAndNotSameEntity)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityExistsByParameterAndIsNotTheSameEntity").Replace("{0}", "User").Replace("{1}", "Rut").Replace("{2}", "User"));
                
                bool existContact = ContactImpl.ExistById(userUpdateDTO.ContactId);
                if (!existContact)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Contact").Replace("{1}", "ContactId"));

                bool existRole = RoleImpl.ExistById(userUpdateDTO.RoleId);
                if (!existRole)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("entityNotExistByParameter").Replace("{0}", "Role").Replace("{1}", "RoleId"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var update = UserImpl.Update(userUpdateDTO);
                return Content(HttpStatusCode.OK, update);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }
        
        /// <summary>
        /// Metodo para eliminar entidad User
        /// </summary>
        /// <remarks>
        /// api/user/Delete?id=1
        /// </remarks>
        /// <param name="id">Id User</param>
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

                var delete = UserImpl.Delete(id);
                return Content(HttpStatusCode.OK, delete);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para listar User
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "TimeZoneInfoName": "Pacific SA Standard Time",
        ///        "ListPaginatedDTO": {
        ///          "PageIndex": 1,
        ///          "PageSize": 10
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="userListPaginatedDTO">Objeto UserListPaginatedDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<User>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("ListPaginated")]
        public IHttpActionResult ListPaginated([FromBody] UserListPaginatedDTO userListPaginatedDTO)
        {
            try
            {
                if (userListPaginatedDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "UserListPaginatedDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (userListPaginatedDTO.ListPaginatedDTO == null)
                {
                    messageVO.SetMessage(1, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ListPaginatedDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(userListPaginatedDTO.TimeZoneInfoName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "TimeZoneInfoName"));
                else if (userListPaginatedDTO.TimeZoneInfoName.Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "TimeZoneInfoName").Replace("{1}", "50"));
                else if (!Useful.ValidateTimeZoneInfo(userListPaginatedDTO.TimeZoneInfoName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "TimeZoneInfoName"));

                if (userListPaginatedDTO.ListPaginatedDTO.PageIndex <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));

                if (userListPaginatedDTO.ListPaginatedDTO.PageSize <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                else if (userListPaginatedDTO.ListPaginatedDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLength").Replace("{0}", "PageSize").Replace("{1}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entitys = UserImpl.ListPaginated(userListPaginatedDTO);
                return Content(HttpStatusCode.OK, entitys);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar registros de entidad User 
        /// </summary>
        /// <remarks>
        /// api/user/TotalRecords
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
                var totalRecords = UserImpl.TotalRecords();
                return Content(HttpStatusCode.OK, totalRecords);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para buscar User
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Rut": "1-9",
        ///        "Name": "Ignacio",
        ///        "LastName": "Chavez",
        ///        "BirthDate": "1990-01-01T00:00:00.0000000-00:00",
        ///        "Active": true,
        ///        "Registered": "1990-01-01T00:00:00.0000000-00:00",
        ///        "ContactId": 1,
        ///        "RoleId": 1,
        ///        "TimeZoneInfoName": "Pacific SA Standard Time",
        ///        "ListPaginatedDTO": {
        ///          "PageIndex": 1,
        ///          "PageSize": 10
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="userSearchDTO">Objeto ContactSearchDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<User>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Search")]
        public IHttpActionResult Search([FromBody] UserSearchDTO userSearchDTO)
        {
            try
            {
                if (userSearchDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "UserSearchDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (userSearchDTO.ListPaginatedDTO == null)
                {
                    messageVO.SetMessage(1, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ListPaginatedDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (userSearchDTO.Id < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Id").Replace("{1}", "0"));
                
                if (!string.IsNullOrWhiteSpace(userSearchDTO.Rut) && userSearchDTO.Rut.Trim().Length > 10)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Rut").Replace("{1}", "10"));
                else if (!string.IsNullOrWhiteSpace(userSearchDTO.Rut) && !Useful.ValidateRut(userSearchDTO.Rut))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Rut"));

                if (!string.IsNullOrWhiteSpace(userSearchDTO.Name) && userSearchDTO.Name.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Name").Replace("{1}", "45"));

                if (!string.IsNullOrWhiteSpace(userSearchDTO.LastName) && userSearchDTO.LastName.Trim().Length > 45)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "LastName").Replace("{1}", "45"));
                
                if (userSearchDTO.BirthDate != null && !Useful.ValidateDateTimeOffset(userSearchDTO.BirthDate))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "BirthDate"));
                else if (userSearchDTO.BirthDate != null && userSearchDTO.BirthDate > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "BirthDate"));
                
                if (userSearchDTO.Registered != null && !Useful.ValidateDateTimeOffset(userSearchDTO.Registered))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParametersNoInitialized").Replace("{0}", "Registered"));
                else if (userSearchDTO.Registered != null && userSearchDTO.Registered > DateTimeOffset.Now)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("dateTimeParameterGreaterThanTheCurrentDate").Replace("{0}", "Registered"));

                if (userSearchDTO.ContactId < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "ContactId").Replace("{1}", "0"));

                if (userSearchDTO.RoleId < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "RoleId").Replace("{1}", "0"));

                if (userSearchDTO.Id == 0 && string.IsNullOrWhiteSpace(userSearchDTO.Rut) && string.IsNullOrWhiteSpace(userSearchDTO.Name) && string.IsNullOrWhiteSpace(userSearchDTO.LastName)
                    && userSearchDTO.BirthDate == null && userSearchDTO.Active == null && userSearchDTO.Registered == null && userSearchDTO.ContactId == 0 && userSearchDTO.RoleId == 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNotInitialized").Replace("{0}", "Id, Rut, Name, LastName, BirthDate, Active, Registered, ContactId y RoleId,").Replace("{1}", "n").Replace("{2}", "s"));

                if (string.IsNullOrWhiteSpace(userSearchDTO.TimeZoneInfoName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "TimeZoneInfoName"));
                else if (userSearchDTO.TimeZoneInfoName.Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "TimeZoneInfoName").Replace("{1}", "50"));
                else if (!Useful.ValidateTimeZoneInfo(userSearchDTO.TimeZoneInfoName))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "TimeZoneInfoName"));

                if (userSearchDTO.ListPaginatedDTO.PageIndex <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));

                if (userSearchDTO.ListPaginatedDTO.PageSize <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                else if (userSearchDTO.ListPaginatedDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLength").Replace("{0}", "PageSize").Replace("{1}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entitys = UserImpl.Search(userSearchDTO);
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

                var excel = UserImpl.Excel(TimeZoneInfoName);
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

                var pdf = UserImpl.PDF(TimeZoneInfoName);
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
