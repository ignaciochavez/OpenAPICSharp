using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System.Web.Http.Results;
using Business.Entity;
using System.Net;
using Business.Tool;
using Business.DTO;
using System.Collections.Generic;

namespace WebAPIUnitTests
{
    [TestClass]
    public class ExampleControllerTests
    {
        #region Select

        /// <summary>
        /// Verificar que el metodo api/example/select funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void ExampleControllerSelectMethodIsInvalidParameters()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> selectMethod = exampleController.Select(0) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void ExampleControllerSelectMethodIsCorrectAndNotExist()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<Example> selectMethod = exampleController.Select(100) as NegotiatedContentResult<Example>;
            Assert.IsNotNull(selectMethod);
            Assert.IsNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void ExampleControllerSelectMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<Example> selectMethod = exampleController.Select(2) as NegotiatedContentResult<Example>;
            Assert.IsNotNull(selectMethod);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region Insert

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsNullObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsEmptyParametersOfObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO(string.Empty, string.Empty, string.Empty, DateTimeOffset.MinValue, false, string.Empty)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsInvalidParametersOfObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO("12332231123", "Test Test Test Test Test Test Test Test Test Test", "Test Test Test Test Test Test Test Test Test Test", DateTimeOffset.Now.AddDays(1), false, "Test Test Test Test")) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad existe y no se inserta
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsExist()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO("1-9", "Pedro", "Gutierrez", DateTimeOffset.UtcNow.Date, true, "1234qwer")) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad no existe y se inserta
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<Example> insertMethod = exampleController.Insert(new ExampleInsertDTO("76-0", "Emanuel", "Leiva", DateTimeOffset.UtcNow.Date, true, "4321rewq")) as NegotiatedContentResult<Example>;
            Assert.IsNotNull(insertMethod);
            Assert.AreEqual(HttpStatusCode.OK, insertMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region Update

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsNullObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsEmptyParametersOfObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(new Example(0, string.Empty, string.Empty, string.Empty, DateTimeOffset.MinValue, false, string.Empty)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsInvalidParametersOfObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(new Example(0, "12332231123123", "Test Test Test Test Test Test Test Test Test Test", "Test Test Test Test Test Test Test Test Test Test", DateTimeOffset.Now.AddDays(1), false, "Test Test Test Test")) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad no existe y no se actualiza
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsNotExist()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<bool> updateMethod = exampleController.Update(new Example(100, "77-9", "Leonel", "Gonzalez", DateTimeOffset.UtcNow.Date, true, "vcxz9876")) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod);
            Assert.IsFalse(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad existe y se actualiza
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<bool> updateMethod = exampleController.Update(new Example(2, "2-7", "Pedro", "Gutierrez", DateTimeOffset.UtcNow.Date, true, "1234qwer")) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod);
            Assert.IsTrue(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Verificar que el metodo api/example/delete funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void ExampleControllerDeleteMethodIsInvalidParameters()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> deleteMethod = exampleController.Delete(0) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void ExampleControllerDeleteMethodIsCorrectAndNotExist()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<bool> deleteMethod = exampleController.Delete(100) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsFalse(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void ExampleControllerDeleteMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<bool> deleteMethod = exampleController.Delete(1) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsTrue(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region List

        /// <summary>
        /// Verificar que el metodo api/example/list funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ExampleControllerListMethodIsNullObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> listMethod = exampleController.List(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/list funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ExampleControllerListMethodIsInvalidParametersOfObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> listMethod = exampleController.List(new ExampleListDTO(0, 0)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod);
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/list funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void ExampleControllerListMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<List<Example>> listMethod = exampleController.List(new ExampleListDTO(1, 10)) as NegotiatedContentResult<List<Example>>;
            Assert.IsNotNull(listMethod);
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region TotalRecords

        /// <summary>
        /// Verificar que el metodo api/example/totalrecords funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void ExampleControllerTotalRecordsMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<long> countMethod = exampleController.TotalRecords() as NegotiatedContentResult<long>;
            Assert.IsNotNull(countMethod);
            Assert.AreEqual(HttpStatusCode.OK, countMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region Excel

        /// <summary>
        /// Verificar que el metodo api/example/excel funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void ExampleControllerExcelMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<ExampleExcelDTO> excelMethod = exampleController.Excel() as NegotiatedContentResult<ExampleExcelDTO>;
            Assert.IsNotNull(excelMethod);
            Assert.AreEqual(HttpStatusCode.OK, excelMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region PDF

        /// <summary>
        /// Verificar que el metodo api/example/pdf funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void ExampleControllerPDFMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<ExamplePDFDTO> pdfMethod = exampleController.PDF() as NegotiatedContentResult<ExamplePDFDTO>;
            Assert.IsNotNull(pdfMethod);
            Assert.AreEqual(HttpStatusCode.OK, pdfMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

    }
}
