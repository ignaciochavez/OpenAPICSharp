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
    public class ContactControllerTests
    {
        ContactController contactController = new ContactController();

        #region Select

        /// <summary>
        /// Verificar que el metodo api/contact/select funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void ContactControllerSelectMethodIsInvalidParameter()
        {
            NegotiatedContentResult<MessageVO> selectMethod = contactController.Select(0) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void ContactControllerSelectMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<Contact> selectMethod = contactController.Select(100) as NegotiatedContentResult<Contact>;
            Assert.IsNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void ContactControllerSelectMethodIsCorrect()
        {
            NegotiatedContentResult<Contact> selectMethod = contactController.Select(1) as NegotiatedContentResult<Contact>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            contactController.Dispose();
        }

        #endregion

        #region Insert

        /// <summary>
        /// Verificar que el metodo api/contact/insert funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ContactControllerInsertMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = contactController.Insert(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/insert funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ContactControllerInsertMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = contactController.Insert(new ContactInsertDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/insert funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ContactControllerInsertMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = contactController.Insert(new ContactInsertDTO("ignacio.chavez4646", "123456")) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/insert funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad se inserta
        /// </summary>
        [TestMethod]
        public void ContactControllerInsertMethodIsCorrect()
        {
            NegotiatedContentResult<int> insertMethod = contactController.Insert(new ContactInsertDTO("ignacio.chavez4646@gmail.com", "+56932151752")) as NegotiatedContentResult<int>;
            Assert.AreNotEqual(0, insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, insertMethod.StatusCode);
            contactController.Dispose();
        }

        #endregion

        #region Update

        /// <summary>
        /// Verificar que el metodo api/contact/update funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ContactControllerUpdateMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = contactController.Update(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/update funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ContactControllerUpdateMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = contactController.Update(new Contact()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/update funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ContactControllerUpdateMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = contactController.Update(new Contact(-1, "Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test ")) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad no existe y no se actualiza
        /// </summary>
        [TestMethod]
        public void ContactControllerUpdateMethodIsNotExist()
        {
            NegotiatedContentResult<bool> updateMethod = contactController.Update(new Contact(3, "elena.garcia@gmail.com", "+56912345678")) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsFalse(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad existe y se actualiza
        /// </summary>
        [TestMethod]
        public void ContactControllerUpdateMethodIsCorrect()
        {
            NegotiatedContentResult<bool> updateMethod = contactController.Update(new Contact(2, "elena.garcia@gmail.com", "+56912345678")) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsTrue(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            contactController.Dispose();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Verificar que el metodo api/contact/delete funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void ContactControllerDeleteMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> deleteMethod = contactController.Delete(-1) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void ContactControllerDeleteMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<bool> deleteMethod = contactController.Delete(100) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsFalse(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void ContactControllerDeleteMethodIsCorrect()
        {
            NegotiatedContentResult<bool> deleteMethod = contactController.Delete(2) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsTrue(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            contactController.Dispose();
        }

        #endregion

        #region ListPaginated

        /// <summary>
        /// Verificar que el metodo api/contact/listpaginated funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ContactControllerListMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = contactController.ListPaginated(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/listpaginated funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ContactControllerListMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = contactController.ListPaginated(new ListPaginatedDTO(0, 0)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/listpaginated funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void ContactControllerListMethodIsCorrect()
        {
            NegotiatedContentResult<List<Contact>> listMethod = contactController.ListPaginated(new ListPaginatedDTO(1, 10)) as NegotiatedContentResult<List<Contact>>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
            contactController.Dispose();
        }

        #endregion

        #region TotalRecords

        /// <summary>
        /// Verificar que el metodo api/contact/totalrecords funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void ContactControllerTotalRecordsMethodIsCorrect()
        {
            NegotiatedContentResult<long> totalRecordsMethod = contactController.TotalRecords() as NegotiatedContentResult<long>;
            Assert.IsNotNull(totalRecordsMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, totalRecordsMethod.StatusCode);
            contactController.Dispose();
        }

        #endregion  

        #region Search

        /// <summary>
        /// Verificar que el metodo api/contact/search funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ContactControllerSearchMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = contactController.Search(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/search funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ContactControllerSearchMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = contactController.Search(new ContactSearchDTO(-1, "Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test ", new ListPaginatedDTO(0, 0))) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/search funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void ContactControllerSearchMethodIsCorrect()
        {
            NegotiatedContentResult<List<Contact>> searchMethod = contactController.Search(new ContactSearchDTO(0, "ignacio.chavez4646@gmail.com", "+56932151752", new ListPaginatedDTO(1, 10))) as NegotiatedContentResult<List<Contact>>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, searchMethod.StatusCode);
            contactController.Dispose();
        }

        #endregion

        #region Excel

        /// <summary>
        /// Verificar que el metodo api/contact/excel funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ContactControllerExcelMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = contactController.Excel(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/excel funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ContactControllerExcelMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = contactController.Excel("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/excel funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void ContactControllerExcelMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> excelMethod = contactController.Excel(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, excelMethod.StatusCode);
            contactController.Dispose();
        }

        #endregion

        #region PDF

        /// <summary>
        /// Verificar que el metodo api/contact/pdf funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ContactControllerPDFMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = contactController.PDF(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/pdf funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ContactControllerPDFMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = contactController.PDF("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            contactController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/contact/pdf funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void ContactControllerPDFMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> pdfMethod = contactController.PDF(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, pdfMethod.StatusCode);
            contactController.Dispose();
        }

        #endregion
    }
}
