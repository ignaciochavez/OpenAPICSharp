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
    public class PowerStatsControllerTests
    {
        PowerStatsController powerStatsController = new PowerStatsController();

        #region Select

        /// <summary>
        /// Verificar que el metodo api/powerstats/select funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerSelectMethodIsInvalidParameter()
        {
            NegotiatedContentResult<MessageVO> selectMethod = powerStatsController.Select(0) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerSelectMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<PowerStats> selectMethod = powerStatsController.Select(100) as NegotiatedContentResult<PowerStats>;
            Assert.IsNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerSelectMethodIsCorrect()
        {
            NegotiatedContentResult<PowerStats> selectMethod = powerStatsController.Select(1) as NegotiatedContentResult<PowerStats>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            powerStatsController.Dispose();
        }

        #endregion

        #region Insert

        /// <summary>
        /// Verificar que el metodo api/powerstats/insert funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerInsertMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = powerStatsController.Insert(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/insert funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerInsertMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = powerStatsController.Insert(new PowerStatsInsertDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/insert funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerInsertMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = powerStatsController.Insert(new PowerStatsInsertDTO(-1, -1, -1, -1, -1, -1)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/insert funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad se inserta
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerInsertMethodIsCorrect()
        {
            NegotiatedContentResult<int> insertMethod = powerStatsController.Insert(new PowerStatsInsertDTO(100, 100, 100, 100, 100, 100)) as NegotiatedContentResult<int>;
            Assert.AreNotEqual(0, insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, insertMethod.StatusCode);
            powerStatsController.Dispose();
        }

        #endregion

        #region Update

        /// <summary>
        /// Verificar que el metodo api/powerstats/update funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerUpdateMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = powerStatsController.Update(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/update funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerUpdateMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = powerStatsController.Update(new PowerStats()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/update funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerUpdateMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = powerStatsController.Update(new PowerStats(-1, -1, -1, -1, -1, -1, -1)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad no existe y no se actualiza
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerUpdateMethodIsNotExist()
        {
            NegotiatedContentResult<bool> updateMethod = powerStatsController.Update(new PowerStats(30, 99, 99, 99, 99, 99, 99)) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsFalse(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad existe y se actualiza
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerUpdateMethodIsCorrect()
        {
            NegotiatedContentResult<bool> updateMethod = powerStatsController.Update(new PowerStats(29, 99, 99, 99, 99, 99, 99)) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsTrue(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            powerStatsController.Dispose();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Verificar que el metodo api/powerstats/delete funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerDeleteMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> deleteMethod = powerStatsController.Delete(-1) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerDeleteMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<bool> deleteMethod = powerStatsController.Delete(100) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsFalse(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerDeleteMethodIsCorrect()
        {
            NegotiatedContentResult<bool> deleteMethod = powerStatsController.Delete(29) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsTrue(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            powerStatsController.Dispose();
        }

        #endregion

        #region ListPaginated

        /// <summary>
        /// Verificar que el metodo api/powerstats/listpaginated funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerListMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = powerStatsController.ListPaginated(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/listpaginated funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerListMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = powerStatsController.ListPaginated(new ListPaginatedDTO(0, 0)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/listpaginated funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerListMethodIsCorrect()
        {
            NegotiatedContentResult<List<PowerStats>> listMethod = powerStatsController.ListPaginated(new ListPaginatedDTO(1, 10)) as NegotiatedContentResult<List<PowerStats>>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
            powerStatsController.Dispose();
        }

        #endregion

        #region TotalRecords

        /// <summary>
        /// Verificar que el metodo api/powerstats/totalrecords funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerTotalRecordsMethodIsCorrect()
        {
            NegotiatedContentResult<long> totalRecordsMethod = powerStatsController.TotalRecords() as NegotiatedContentResult<long>;
            Assert.IsNotNull(totalRecordsMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, totalRecordsMethod.StatusCode);
            powerStatsController.Dispose();
        }

        #endregion

        #region Search

        /// <summary>
        /// Verificar que el metodo api/powerstats/search funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerSearchMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = powerStatsController.Search(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/search funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerSearchMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = powerStatsController.Search(new PowerStatsSearchDTO(-1, -1, -1, -1, -1, -1, -1, new ListPaginatedDTO(0, 0))) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/search funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerSearchMethodIsCorrect()
        {
            NegotiatedContentResult<List<PowerStats>> searchMethod = powerStatsController.Search(new PowerStatsSearchDTO(0, 0, 0, 0, 0, 100, 0,  new ListPaginatedDTO(1, 10))) as NegotiatedContentResult<List<PowerStats>>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, searchMethod.StatusCode);
            powerStatsController.Dispose();
        }

        #endregion

        #region Excel

        /// <summary>
        /// Verificar que el metodo api/powerstats/excel funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerExcelMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = powerStatsController.Excel(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/excel funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerExcelMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = powerStatsController.Excel("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/excel funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerExcelMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> excelMethod = powerStatsController.Excel(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, excelMethod.StatusCode);
            powerStatsController.Dispose();
        }

        #endregion

        #region PDF

        /// <summary>
        /// Verificar que el metodo api/powerstats/pdf funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerPDFMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = powerStatsController.PDF(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/pdf funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerPDFMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = powerStatsController.PDF("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            powerStatsController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/powerstats/pdf funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void PowerStatsControllerPDFMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> pdfMethod = powerStatsController.PDF(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, pdfMethod.StatusCode);
            powerStatsController.Dispose();
        }

        #endregion
    }
}
