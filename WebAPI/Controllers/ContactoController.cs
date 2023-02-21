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
    ///  Controlador api/contact
    /// </summary>    
    [RoutePrefix("api/contacto")]
    public class ContactoController : ApiController
    {
        private MessageVO messageVO = new MessageVO();
        private ContentHTML contentHTML = new ContentHTML();

        /// <summary>
        /// Metodo para seleccionar Contacto
        /// </summary>
        /// <remarks>
        /// api/contacto/Select?id=1
        /// </remarks>
        /// <param name="id">Id Contacto</param>
        /// <returns>Retorna el objeto</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(Contacto))]
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

                var entity = ContactoImpl.Select(id);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para insertar Contacto
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "CorreoElectronico": "correo@mail.com",
        ///        "Telefono": "+56912345678"
        ///     }
        ///
        /// </remarks>
        /// <param name="contactoInsertDTO">Objeto ContactoInsertDTO</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado y retornado", typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Insert")]
        public IHttpActionResult Insert([FromBody] ContactoInsertDTO contactoInsertDTO)
        {
            try
            {
                if (contactoInsertDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ContactoInsertDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (string.IsNullOrWhiteSpace(contactoInsertDTO.CorreoElectronico))                
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "CorreoElectronico"));
                else if (contactoInsertDTO.CorreoElectronico.Trim().Length > 15)                
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "CorreoElectronico").Replace("{1}", "15"));
                else if (!Useful.ValidateEmail(contactoInsertDTO.CorreoElectronico))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "CorreoElectronico"));

                if (string.IsNullOrWhiteSpace(contactoInsertDTO.Telefono))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Telefono"));
                else if (contactoInsertDTO.Telefono.Trim().Length > 15)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Telefono").Replace("{1}", "15"));
                else if (!Useful.ValidatePhone(contactoInsertDTO.Telefono))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Telefono"));
                
                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var insert = ContactoImpl.Insert(contactoInsertDTO);
                return Content(HttpStatusCode.OK, insert);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para actualizar Contacto
        /// </summary>
        /// <remarks>
        /// Request PUT:
        ///
        ///     {
        ///        "Id": 1,
        ///        "CorreoElectronico": "correo@mail.com",
        ///        "Telefono": "+56912345678"
        ///     }
        ///
        /// </remarks>
        /// <param name="contacto">Modelo Contacto</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] Contacto contacto)
        {
            try
            {
                if (contacto == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "Contacto"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (contacto.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (string.IsNullOrWhiteSpace(contacto.CorreoElectronico))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "CorreoElectronico"));
                else if (contacto.CorreoElectronico.Trim().Length > 15)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "CorreoElectronico").Replace("{1}", "15"));
                else if (!Useful.ValidateEmail(contacto.CorreoElectronico))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "CorreoElectronico"));

                if (string.IsNullOrWhiteSpace(contacto.Telefono))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Telefono"));
                else if (contacto.Telefono.Trim().Length > 15)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Telefono").Replace("{1}", "15"));
                else if (!Useful.ValidatePhone(contacto.Telefono))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("invalidFormatParameters").Replace("{0}", "Telefono"));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var update = ContactoImpl.Update(contacto);
                return Content(HttpStatusCode.OK, update);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para eliminar entidad Contacto
        /// </summary>
        /// <remarks>
        /// api/contacto/Delete?id=1
        /// </remarks>
        /// <param name="id">Id Contacto</param>
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

                var delete = ContactoImpl.Delete(id);
                return Content(HttpStatusCode.OK, delete);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para listar Contacto
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
        /// <param name="listPaginatedDTO">Objeto</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<Contacto>))]
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

                var entitys = ContactoImpl.ListPaginated(listPaginatedDTO);
                return Content(HttpStatusCode.OK, entitys);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar registros de entidad Contacto
        /// </summary>
        /// <remarks>
        /// api/contacto/TotalRecords
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
                var totalRecords = ContactoImpl.TotalRecords();
                return Content(HttpStatusCode.OK, totalRecords);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para buscar Contacto
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Id": 0,
        ///        "CorreoElectronico": "email@correo.com",
        ///        "Telefono": "+56912345678"
        ///        "ListPaginatedDTO": {
        ///          "PageIndex": 1,
        ///          "PageSize": 10
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="contactSearchDTO">Objeto ContactoSearchDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<Contacto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Search")]
        public IHttpActionResult Search([FromBody] ContactoSearchDTO contactSearchDTO)
        {
            try
            {
                if (contactSearchDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ContactSearchDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (contactSearchDTO.ListPaginatedDTO == null)
                {
                    messageVO.SetMessage(1, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ListPaginatedDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (contactSearchDTO.Id < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Id").Replace("{1}", "0"));

                if (!string.IsNullOrWhiteSpace(contactSearchDTO.CorreoElectronico) && contactSearchDTO.CorreoElectronico.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterGreaterThan").Replace("{0}", "CorreoElectronico").Replace("{1}", "50"));

                if (!string.IsNullOrWhiteSpace(contactSearchDTO.Telefono) && contactSearchDTO.Telefono.Trim().Length > 15)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterGreaterThan").Replace("{0}", "Telefono").Replace("{1}", "15"));

                if (contactSearchDTO.Id == 0 && string.IsNullOrWhiteSpace(contactSearchDTO.CorreoElectronico) && string.IsNullOrWhiteSpace(contactSearchDTO.Telefono))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNotInitialized").Replace("{0}", "Id, CorreoElectronico y Telefono,").Replace("{1}", "n").Replace("{2}", "s"));

                if (contactSearchDTO.ListPaginatedDTO.PageIndex <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));

                if (contactSearchDTO.ListPaginatedDTO.PageSize <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                else if (contactSearchDTO.ListPaginatedDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLength").Replace("{0}", "PageSize").Replace("{1}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entitys = ContactoImpl.Search(contactSearchDTO);
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
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(FileDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Excel")]
        public IHttpActionResult Excel()
        {
            try
            {
                var excel = ContactoImpl.Excel();
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
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(FileDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("PDF")]
        public IHttpActionResult PDF()
        {
            try
            {
                var pdf = ContactoImpl.PDF();
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
