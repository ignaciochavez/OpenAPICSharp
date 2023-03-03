using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System.Web.Http.Results;
using Business.Tool;
using System.Net;
using Business.Entity;
using Business.DTO;
using System.Collections.Generic;

namespace WebAPIUnitTests
{
    [TestClass]
    public class UserControllerTests
    {
        UserController userController = new UserController();

        #region Select

        /// <summary>
        /// Verificar que el metodo api/user/select funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void UserControllerSelectMethodIsInvalidParameter()
        {
            NegotiatedContentResult<MessageVO> selectMethod = userController.Select(new UserSelectDTO(-1, string.Empty)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void UserControllerSelectMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<User> selectMethod = userController.Select(new UserSelectDTO(100, Useful.GetAppSettings("TimeZoneInfoName"))) as NegotiatedContentResult<User>;
            Assert.IsNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void UserControllerSelectMethodIsCorrect()
        {
            NegotiatedContentResult<User> selectMethod = userController.Select(new UserSelectDTO(1, Useful.GetAppSettings("TimeZoneInfoName"))) as NegotiatedContentResult<User>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            userController.Dispose();
        }

        #endregion

        #region Insert

        /// <summary>
        /// Verificar que el metodo api/user/insert funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void UserControllerInsertMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = userController.Insert(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/insert funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void UserControllerInsertMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = userController.Insert(new UserInsertDTO(string.Empty, string.Empty, string.Empty, DateTime.MinValue, string.Empty, null, 0, 0)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/insert funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void UserControllerInsertMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = userController.Insert(new UserInsertDTO("Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", DateTime.MinValue, "Test Test Test Test ", null, -1, -1)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/insert funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad se inserta
        /// </summary>
        [TestMethod]
        public void UserControllerInsertMethodIsCorrect()
        {
            NegotiatedContentResult<int> insertMethod = userController.Insert(new UserInsertDTO("4-3", "Manuel", "Palma", new DateTime(2002, 9, 17), "123456", true, 1, 3)) as NegotiatedContentResult<int>;
            Assert.AreNotEqual(0, insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, insertMethod.StatusCode);
            userController.Dispose();
        }

        #endregion

        #region Update

        /// <summary>
        /// Verificar que el metodo api/user/update funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void UserControllerUpdateMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = userController.Update(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/update funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void UserControllerUpdateMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = userController.Update(new UserUpdateDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/update funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void UserControllerUpdateMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = userController.Update(new UserUpdateDTO(-1, "Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", DateTime.MinValue, "Test Test Test Test ", null, -1, -1)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad no existe y no se actualiza
        /// </summary>
        [TestMethod]
        public void UserControllerUpdateMethodIsNotExist()
        {
            NegotiatedContentResult<bool> updateMethod = userController.Update(new UserUpdateDTO(100, "5-1", "Manuel", "Palma", new DateTime(2002, 9, 17), "123456", true, 1, 3)) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsFalse(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad existe y se actualiza
        /// </summary>
        [TestMethod]
        public void UserControllerUpdateMethodIsCorrect()
        {
            NegotiatedContentResult<bool> updateMethod = userController.Update(new UserUpdateDTO(4, "5-1", "Manuel", "Palma", new DateTime(2002, 9, 17), "123456", true, 1, 3)) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsTrue(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            userController.Dispose();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Verificar que el metodo api/user/delete funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void UserControllerDeleteMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> deleteMethod = userController.Delete(-1) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void UserControllerDeleteMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<bool> deleteMethod = userController.Delete(100) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsFalse(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void UserControllerDeleteMethodIsCorrect()
        {
            NegotiatedContentResult<bool> deleteMethod = userController.Delete(4) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsTrue(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            userController.Dispose();
        }

        #endregion

        #region ListPaginated

        /// <summary>
        /// Verificar que el metodo api/user/listpaginated funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void UserControllerListMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = userController.ListPaginated(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/listpaginated funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void UserControllerListMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = userController.ListPaginated(new UserListPaginatedDTO("asd", new ListPaginatedDTO(0, 0))) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/listpaginated funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void UserControllerListMethodIsCorrect()
        {
            NegotiatedContentResult<List<User>> listMethod = userController.ListPaginated(new UserListPaginatedDTO(Useful.GetAppSettings("TimeZoneInfoName"), new ListPaginatedDTO(1, 10))) as NegotiatedContentResult<List<User>>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
            userController.Dispose();
        }

        #endregion

        #region TotalRecords

        /// <summary>
        /// Verificar que el metodo api/user/totalrecords funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void UserControllerTotalRecordsMethodIsCorrect()
        {
            NegotiatedContentResult<long> totalRecordsMethod = userController.TotalRecords() as NegotiatedContentResult<long>;
            Assert.IsNotNull(totalRecordsMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, totalRecordsMethod.StatusCode);
            userController.Dispose();
        }

        #endregion

        #region Search

        /// <summary>
        /// Verificar que el metodo api/user/search funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void UserControllerSearchMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = userController.Search(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/search funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void UserControllerSearchMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = userController.Search(new UserSearchDTO(-1, "Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", DateTime.MinValue, null, DateTimeOffset.MinValue, -1, -1, "asd", new ListPaginatedDTO(0, 0))) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/search funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void UserControllerSearchMethodIsCorrect()
        {
            NegotiatedContentResult<List<User>> searchMethod = userController.Search(new UserSearchDTO(0, string.Empty, "Ignacio", string.Empty, DateTime.MinValue, null, DateTimeOffset.MinValue, 0, 0, Useful.GetAppSettings("TimeZoneInfoName"), new ListPaginatedDTO(1, 10))) as NegotiatedContentResult<List<User>>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, searchMethod.StatusCode);
            userController.Dispose();
        }

        #endregion

        #region Excel

        /// <summary>
        /// Verificar que el metodo api/user/excel funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void UserControllerExcelMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = userController.Excel(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/excel funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void UserControllerExcelMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = userController.Excel("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/excel funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void UserControllerExcelMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> excelMethod = userController.Excel(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, excelMethod.StatusCode);
            userController.Dispose();
        }

        #endregion

        #region PDF

        /// <summary>
        /// Verificar que el metodo api/user/pdf funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void UserControllerPDFMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = userController.PDF(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/pdf funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void UserControllerPDFMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = userController.PDF("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            userController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/user/pdf funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void UserControllerPDFMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> pdfMethod = userController.PDF(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, pdfMethod.StatusCode);
            userController.Dispose();
        }

        #endregion
    }
}
