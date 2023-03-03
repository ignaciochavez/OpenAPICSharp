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
    public class BiographyControllerTests
    {
        BiographyController biographyController = new BiographyController();

        #region Select

        /// <summary>
        /// Verificar que el metodo api/biography/select funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void BiographyControllerSelectMethodIsInvalidParameter()
        {
            NegotiatedContentResult<MessageVO> selectMethod = biographyController.Select(0) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void BiographyControllerSelectMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<Biography> selectMethod = biographyController.Select(100) as NegotiatedContentResult<Biography>;
            Assert.IsNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void BiographyControllerSelectMethodIsCorrect()
        {
            NegotiatedContentResult<Biography> selectMethod = biographyController.Select(1) as NegotiatedContentResult<Biography>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            biographyController.Dispose();
        }

        #endregion

        #region Insert

        /// <summary>
        /// Verificar que el metodo api/biography/insert funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void BiographyControllerInsertMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = biographyController.Insert(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/insert funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void BiographyControllerInsertMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = biographyController.Insert(new BiographyInsertDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/insert funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void BiographyControllerInsertMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = biographyController.Insert(new BiographyInsertDTO("Test Test Test Test Test Test Test Test Test Test Test Test ", "Test", DateTime.Now.AddDays(1), "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test")) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/insert funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad se inserta
        /// </summary>
        [TestMethod]
        public void BiographyControllerInsertMethodIsCorrect()
        {
            NegotiatedContentResult<int> insertMethod = biographyController.Insert(new BiographyInsertDTO("Ignacio Chavez", "M", new DateTime(2000, 1, 1), "Nacho", "Marvel Comics")) as NegotiatedContentResult<int>;
            Assert.AreNotEqual(0, insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, insertMethod.StatusCode);
            biographyController.Dispose();
        }

        #endregion

        #region Update

        /// <summary>
        /// Verificar que el metodo api/biography/update funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void BiographyControllerUpdateMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = biographyController.Update(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/update funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void BiographyControllerUpdateMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = biographyController.Update(new Biography()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/update funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void BiographyControllerUpdateMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = biographyController.Update(new Biography(-1, "Test Test Test Test Test Test Test Test Test Test Test Test ", "Test", DateTime.Now.AddDays(1), "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test")) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad no existe y no se actualiza
        /// </summary>
        [TestMethod]
        public void BiographyControllerUpdateMethodIsNotExist()
        {
            NegotiatedContentResult<bool> updateMethod = biographyController.Update(new Biography(100, "Elena Garcia", "F", new DateTime(1980, 1, 1), "Elena", "DC Comics")) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsFalse(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad existe y se actualiza
        /// </summary>
        [TestMethod]
        public void BiographyControllerUpdateMethodIsCorrect()
        {
            NegotiatedContentResult<bool> updateMethod = biographyController.Update(new Biography(29, "Elena Garcia", "F", new DateTime(1980, 1, 1), "Elena", "DC Comics")) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsTrue(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            biographyController.Dispose();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Verificar que el metodo api/biography/delete funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void BiographyControllerDeleteMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> deleteMethod = biographyController.Delete(-1) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void BiographyControllerDeleteMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<bool> deleteMethod = biographyController.Delete(100) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsFalse(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void BiographyControllerDeleteMethodIsCorrect()
        {
            NegotiatedContentResult<bool> deleteMethod = biographyController.Delete(29) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsTrue(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            biographyController.Dispose();
        }

        #endregion        

        #region ListPaginated

        /// <summary>
        /// Verificar que el metodo api/biography/listpaginated funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void BiographyControllerListMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = biographyController.ListPaginated(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/listpaginated funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void BiographyControllerListMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = biographyController.ListPaginated(new ListPaginatedDTO(0, 0)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/listpaginated funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void BiographyControllerListMethodIsCorrect()
        {
            NegotiatedContentResult<List<Biography>> listMethod = biographyController.ListPaginated(new ListPaginatedDTO(1, 10)) as NegotiatedContentResult<List<Biography>>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
            biographyController.Dispose();
        }

        #endregion

        #region TotalRecords

        /// <summary>
        /// Verificar que el metodo api/biography/totalrecords funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void BiographyControllerTotalRecordsMethodIsCorrect()
        {
            NegotiatedContentResult<long> totalRecordsMethod = biographyController.TotalRecords() as NegotiatedContentResult<long>;
            Assert.IsNotNull(totalRecordsMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, totalRecordsMethod.StatusCode);
            biographyController.Dispose();
        }

        #endregion

        #region Search

        /// <summary>
        /// Verificar que el metodo api/biography/search funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void BiographyControllerSearchMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = biographyController.Search(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/search funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void BiographyControllerSearchMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = biographyController.Search(new BiographySearchDTO(-1, "Test Test Test Test Test Test Test Test Test Test Test ", "Test", DateTime.Now.AddDays(1), "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test ", new ListPaginatedDTO(0, 0))) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/search funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void BiographyControllerSearchMethodIsCorrect()
        {
            NegotiatedContentResult<List<Biography>> searchMethod = biographyController.Search(new BiographySearchDTO(0, string.Empty, "M", DateTime.MinValue, string.Empty, string.Empty, new ListPaginatedDTO(1, 10))) as NegotiatedContentResult<List<Biography>>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, searchMethod.StatusCode);
            biographyController.Dispose();
        }

        #endregion

        #region Excel

        /// <summary>
        /// Verificar que el metodo api/biography/excel funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void BiographyControllerExcelMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = biographyController.Excel(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/excel funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void BiographyControllerExcelMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = biographyController.Excel("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/excel funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void BiographyControllerExcelMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> excelMethod = biographyController.Excel(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, excelMethod.StatusCode);
            biographyController.Dispose();
        }

        #endregion

        #region PDF

        /// <summary>
        /// Verificar que el metodo api/biography/pdf funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void BiographyControllerPDFMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = biographyController.PDF(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/pdf funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void BiographyControllerPDFMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = biographyController.PDF("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/pdf funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void BiographyControllerPDFMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> pdfMethod = biographyController.PDF(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, pdfMethod.StatusCode);
            biographyController.Dispose();
        }

        #endregion
    }
}
