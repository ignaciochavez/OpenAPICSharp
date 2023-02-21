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
    ///  Controlador api/rol
    /// </summary>    
    [RoutePrefix("api/rol")]
    public class RolController : ApiController
    {
        private MessageVO messageVO = new MessageVO();
        private ContentHTML contentHTML = new ContentHTML();

        /// <summary>
        /// Metodo para seleccionar Rol
        /// </summary>
        /// <remarks>
        /// api/rol/Select?id=1
        /// </remarks>
        /// <param name="id">Id Role</param>
        /// <returns>Retorna el objeto</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(Rol))]
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

                var entity = RolImpl.Select(id);
                return Content(HttpStatusCode.OK, entity);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para insertar Rol
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Nombre": "Administrador"
        ///     }
        ///
        /// </remarks>
        /// <param name="Nombre">Nombre Rol</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado y retornado", typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Insert")]
        public IHttpActionResult Insert([FromBody] string Nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Nombre))
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Nombre"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (Nombre.Trim().Length > 50)
                {
                    messageVO.SetMessage(1, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Nombre").Replace("{1}", "50"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                
                bool existsEntity = RolImpl.Exist(Nombre);
                if (existsEntity)
                {
                    messageVO.SetMessage(2, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("entityExistByParameter").Replace("{0}", "Rol").Replace("{1}", "Nombre"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var insert = RolImpl.Insert(Nombre);
                return Content(HttpStatusCode.OK, insert);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para actualizar Rol
        /// </summary>
        /// <remarks>
        /// Request PUT:
        ///
        ///     {
        ///        "Id": 1,
        ///        "Nombre": "Administrador"
        ///     }
        ///
        /// </remarks>
        /// <param name="rol">Modelo Rol</param>
        /// <returns>Retorna el objeto</returns>    
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido creado o no", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] Rol rol)
        {
            try
            {
                if (rol == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "Rol"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (rol.Id <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "Id"));

                if (string.IsNullOrWhiteSpace(rol.Nombre))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("emptyParameters").Replace("{0}", "Nombre"));
                else if (rol.Nombre.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLengthCharacter").Replace("{0}", "Nombre").Replace("{1}", "50"));
                                
                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(1, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                bool existAndNotSameEntity = RolImpl.ExistAndNotSameEntity(new Rol(rol.Id, rol.Nombre));
                if (existAndNotSameEntity)
                {
                    messageVO.SetMessage(2, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("entityExistsByParameterAndIsNotTheSameEntity").Replace("{0}", "Rol").Replace("{1}", "Nombre").Replace("{2}", "Rol"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var update = RolImpl.Update(rol);
                return Content(HttpStatusCode.OK, update);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para eliminar entidad Rol
        /// </summary>
        /// <remarks>
        /// api/rol/Delete?id=1
        /// </remarks>
        /// <param name="id">Id Rol</param>
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

                var delete = RolImpl.Delete(id);
                return Content(HttpStatusCode.OK, delete);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para listar Rol
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
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<Rol>))]
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

                var entitys = RolImpl.ListPaginated(listPaginatedDTO);
                return Content(HttpStatusCode.OK, entitys);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para contar registros de entidad Rol
        /// </summary>
        /// <remarks>
        /// api/rol/TotalRecords
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
                var totalRecords = RolImpl.TotalRecords();
                return Content(HttpStatusCode.OK, totalRecords);
            }
            catch (Exception ex)
            {
                messageVO.SetMessage(0, contentHTML.GetInnerTextById("exceptionTitle"), ex.GetOriginalException().Message);
                return Content(HttpStatusCode.InternalServerError, messageVO);
            }
        }

        /// <summary>
        /// Metodo para buscar Rol
        /// </summary>
        /// <remarks>
        /// Request POST:
        ///
        ///     {
        ///        "Id": 0,
        ///        "Nombre": "Administador",
        ///        "ListPaginatedDTO": {
        ///          "PageIndex": 1,
        ///          "PageSize": 10
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <param name="rolSearchDTO">Objeto RolSearchDTO</param>
        /// <returns>Retorna el objeto</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "No Autorizado", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.OK, "El objeto ha sido retornado", typeof(List<Rol>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Parametros invalidos", typeof(MessageVO))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", typeof(MessageVO))]
        [Route("Search")]
        public IHttpActionResult Search([FromBody] RolSearchDTO rolSearchDTO)
        {
            try
            {
                if (rolSearchDTO == null)
                {
                    messageVO.SetMessage(0, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "RolSearchDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }
                else if (rolSearchDTO.ListPaginatedDTO == null)
                {
                    messageVO.SetMessage(1, contentHTML.GetInnerTextById("requeridTitle"), contentHTML.GetInnerTextById("nullObject").Replace("{0}", "ListPaginatedDTO"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                if (rolSearchDTO.Id < 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterLessThan").Replace("{0}", "Id").Replace("{1}", "0"));

                if (!string.IsNullOrWhiteSpace(rolSearchDTO.Nombre) && rolSearchDTO.Nombre.Trim().Length > 50)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parameterGreaterThan").Replace("{0}", "Nombre").Replace("{1}", "50"));

                if (rolSearchDTO.Id == 0 && string.IsNullOrWhiteSpace(rolSearchDTO.Nombre))
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersNotInitialized").Replace("{0}", "Id y Nombre,").Replace("{1}", "n").Replace("{2}", "s"));

                if (rolSearchDTO.ListPaginatedDTO.PageIndex <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));

                if (rolSearchDTO.ListPaginatedDTO.PageSize <= 0)
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("parametersAtZero").Replace("{0}", "PageIndex"));
                else if (rolSearchDTO.ListPaginatedDTO.PageSize > Useful.GetPageSizeMaximun())
                    messageVO.Messages.Add(contentHTML.GetInnerTextById("maximunParameterLength").Replace("{0}", "PageSize").Replace("{1}", Useful.GetPageSizeMaximun().ToString()));

                if (messageVO.Messages.Count() > 0)
                {
                    messageVO.SetIdTitle(2, contentHTML.GetInnerTextById("requeridTitle"));
                    return Content(HttpStatusCode.BadRequest, messageVO);
                }

                var entitys = RolImpl.Search(rolSearchDTO);
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
                var excel = RolImpl.Excel();
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
                var pdf = RolImpl.PDF();
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
