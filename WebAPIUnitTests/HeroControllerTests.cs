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
    public class HeroControllerTests
    {
        HeroController heroController = new HeroController();

        #region Select

        /// <summary>
        /// Verificar que el metodo api/hero/select funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void HeroControllerSelectMethodIsInvalidParameter()
        {
            NegotiatedContentResult<MessageVO> selectMethod = heroController.Select(0) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void HeroControllerSelectMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<Hero> selectMethod = heroController.Select(100) as NegotiatedContentResult<Hero>;
            Assert.IsNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void HeroControllerSelectMethodIsCorrect()
        {
            NegotiatedContentResult<Hero> selectMethod = heroController.Select(1) as NegotiatedContentResult<Hero>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            heroController.Dispose();
        }

        #endregion

        #region Insert

        /// <summary>
        /// Verificar que el metodo api/hero/insert funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void HeroControllerInsertMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = heroController.Insert(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/insert funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void HeroControllerInsertMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = heroController.Insert(new HeroInsertDTO(string.Empty, string.Empty, string.Empty, 0, 0)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/insert funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void HeroControllerInsertMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = heroController.Insert(new HeroInsertDTO("Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test ", -1, -1)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/insert funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad se inserta
        /// </summary>
        [TestMethod]
        public void HeroControllerInsertMethodIsCorrect()
        {
            NegotiatedContentResult<int> insertMethod = heroController.Insert(new HeroInsertDTO("Ignacio Chavez", "El super heroe programador de chile", Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}Contents\\api-200.png"), 1, 1)) as NegotiatedContentResult<int>;
            Assert.AreNotEqual(0, insertMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, insertMethod.StatusCode);
            heroController.Dispose();
        }

        #endregion

        #region Update

        /// <summary>
        /// Verificar que el metodo api/hero/update funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void HeroControllerUpdateMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = heroController.Update(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/update funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void HeroControllerUpdateMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = heroController.Update(new HeroUpdateDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/update funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void HeroControllerUpdateMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = heroController.Update(new HeroUpdateDTO(-1, "Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test ", -1, -1)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad no existe y no se actualiza
        /// </summary>
        [TestMethod]
        public void HeroControllerUpdateMethodIsNotExist()
        {
            NegotiatedContentResult<bool> updateMethod = heroController.Update(new HeroUpdateDTO(100, "Ignacio Chavez Update", "El super heroe programador de chile", Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}Contents\\api-200.png"), 1, 1)) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsFalse(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad existe y se actualiza
        /// </summary>
        [TestMethod]
        public void HeroControllerUpdateMethodIsCorrect()
        {
            NegotiatedContentResult<bool> updateMethod = heroController.Update(new HeroUpdateDTO(29, "Ignacio Chavez Update", "El super heroe programador de chile", Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}Contents\\api-200.png"), 1, 1)) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod.Content);
            Assert.IsTrue(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            heroController.Dispose();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Verificar que el metodo api/hero/delete funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void HeroControllerDeleteMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> deleteMethod = heroController.Delete(-1) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void HeroControllerDeleteMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<bool> deleteMethod = heroController.Delete(100) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsFalse(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void HeroControllerDeleteMethodIsCorrect()
        {
            NegotiatedContentResult<bool> deleteMethod = heroController.Delete(29) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod.Content);
            Assert.IsTrue(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            heroController.Dispose();
        }

        #endregion

        #region ListPaginated

        /// <summary>
        /// Verificar que el metodo api/hero/listpaginated funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void HeroControllerListMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = heroController.ListPaginated(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/listpaginated funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void HeroControllerListMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = heroController.ListPaginated(new ListPaginatedDTO(0, 0)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/listpaginated funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void HeroControllerListMethodIsCorrect()
        {
            NegotiatedContentResult<List<Hero>> listMethod = heroController.ListPaginated(new ListPaginatedDTO(1, 10)) as NegotiatedContentResult<List<Hero>>;
            Assert.IsNotNull(listMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
            heroController.Dispose();
        }

        #endregion

        #region TotalRecords

        /// <summary>
        /// Verificar que el metodo api/hero/totalrecords funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void HeroControllerTotalRecordsMethodIsCorrect()
        {
            NegotiatedContentResult<long> totalRecordsMethod = heroController.TotalRecords() as NegotiatedContentResult<long>;
            Assert.IsNotNull(totalRecordsMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, totalRecordsMethod.StatusCode);
            heroController.Dispose();
        }

        #endregion

        #region Search

        /// <summary>
        /// Verificar que el metodo api/hero/search funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void HeroControllerSearchMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = heroController.Search(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/search funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void HeroControllerSearchMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> searchMethod = heroController.Search(new HeroSearchDTO(-1, "Test Test Test Test Test Test Test Test Test Test Test ", string.Empty, -1, -1, new ListPaginatedDTO(0, 0))) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, searchMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/search funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void HeroControllerSearchMethodIsCorrect()
        {
            NegotiatedContentResult<List<Hero>> searchMethod = heroController.Search(new HeroSearchDTO(0, "Batman", string.Empty, 0, 0, new ListPaginatedDTO(1, 10))) as NegotiatedContentResult<List<Hero>>;
            Assert.IsNotNull(searchMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, searchMethod.StatusCode);
            heroController.Dispose();
        }

        #endregion

        #region Excel

        /// <summary>
        /// Verificar que el metodo api/hero/excel funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void HeroControllerExcelMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = heroController.Excel(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/excel funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void HeroControllerExcelMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> excelMethod = heroController.Excel("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, excelMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/excel funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void HeroControllerExcelMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> excelMethod = heroController.Excel(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(excelMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, excelMethod.StatusCode);
            heroController.Dispose();
        }

        #endregion

        #region PDF

        /// <summary>
        /// Verificar que el metodo api/hero/pdf funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void HeroControllerPDFMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = heroController.PDF(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/pdf funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void HeroControllerPDFMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> pdfMethod = heroController.PDF("asdsad") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, pdfMethod.StatusCode);
            heroController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/hero/pdf funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void HeroControllerPDFMethodIsCorrect()
        {
            NegotiatedContentResult<FileDTO> pdfMethod = heroController.PDF(Useful.GetAppSettings("TimeZoneInfoName")) as NegotiatedContentResult<FileDTO>;
            Assert.IsNotNull(pdfMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, pdfMethod.StatusCode);
            heroController.Dispose();
        }

        #endregion
    }
}
