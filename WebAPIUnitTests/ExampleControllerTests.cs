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
            Assert.IsInstanceOfType(selectMethod.Content, typeof(MessageVO));
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
            Assert.IsInstanceOfType(selectMethod.Content, typeof(Example));
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
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
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
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = string.Empty, Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
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
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = "12332231", Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
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
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = "1-9", Name = "Pedro", LastName = "Gutierrez", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "1234qwer" }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
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
            NegotiatedContentResult<Example> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = "18-3", Name = "Emanuel", LastName = "Leiva", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "4321rewq" }) as NegotiatedContentResult<Example>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(Example));
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
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
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
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(new Example() { Id = 0, Rut = string.Empty, Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
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
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(new Example() { Id = 0, Rut = "12332231", Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
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
            NegotiatedContentResult<bool> updateMethod = exampleController.Update(new Example() { Id = 100, Rut = "21-3", Name = "Leonel", LastName = "Gonzalez", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "vcxz9876" }) as NegotiatedContentResult<bool>;
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
            NegotiatedContentResult<bool> updateMethod = exampleController.Update(new Example() { Id = 2, Rut = "2-7", Name = "Pedro", LastName = "Gutierrez", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "1234qwer" }) as NegotiatedContentResult<bool>;
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
            Assert.IsInstanceOfType(deleteMethod.Content, typeof(MessageVO));
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
            Assert.IsInstanceOfType(listMethod.Content, typeof(MessageVO));
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
            NegotiatedContentResult<MessageVO> listMethod = exampleController.List(new ExampleListDTO() { PageIndex = 0, PageSize = 0 }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(MessageVO));
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
            NegotiatedContentResult<List<Example>> listMethod = exampleController.List(new ExampleListDTO() { PageIndex = 1, PageSize = 10 }) as NegotiatedContentResult<List<Example>>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(List<Example>));
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region TotalRecords

        [TestMethod]
        public void ExampleControllerTotalRecordsMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<long> countMethod = exampleController.TotalRecords() as NegotiatedContentResult<long>;
            Assert.IsNotNull(countMethod);
            Assert.IsInstanceOfType(countMethod.Content, typeof(long));
            Assert.AreEqual(HttpStatusCode.OK, countMethod.StatusCode);
            exampleController.Dispose();
        }
        #endregion

        #region ExistByNameAndNotSameEntity

        /// <summary>
        /// Verificar que el metodo api/example/existbyrutandnotsameentity funciona segun lo necesitado al enviar el objecto nulo
        /// </summary>
        [TestMethod]
        public void ExampleControllerExistByRutAndNotSameEntityMethodIsNullObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> existByRutAndNotSameEntityMethod = exampleController.ExistByRutAndNotSameEntity(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(existByRutAndNotSameEntityMethod);
            Assert.IsInstanceOfType(existByRutAndNotSameEntityMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, existByRutAndNotSameEntityMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/existbyrutandnotsameentity funciona segun lo necesitado al enviar el objecto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ExampleControllerExistByRutAndNotSameEntityMethodIsEmptyParametersOfObject()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<MessageVO> existByRutAndNotSameEntityMethod = exampleController.ExistByRutAndNotSameEntity(new ExampleExistByRutAndNotSameEntityDTO() { Id = 0, Rut = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(existByRutAndNotSameEntityMethod);
            Assert.IsInstanceOfType(existByRutAndNotSameEntityMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, existByRutAndNotSameEntityMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/existbyrutandnotsameentity funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void ExampleControllerExistByRutAndNotSameEntityMethodIsNotExist()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<bool> existByRutAndNotSameEntityMethod = exampleController.ExistByRutAndNotSameEntity(new ExampleExistByRutAndNotSameEntityDTO() { Id = 100, Rut = "29-9" }) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(existByRutAndNotSameEntityMethod);
            Assert.IsFalse(existByRutAndNotSameEntityMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, existByRutAndNotSameEntityMethod.StatusCode);
            exampleController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/example/existbyrutandnotsameentity funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void ExampleControllerExistByRutAndNotSameEntityMethodIsCorrect()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<bool> existByRutAndNotSameEntityMethod = exampleController.ExistByRutAndNotSameEntity(new ExampleExistByRutAndNotSameEntityDTO() { Id = 11, Rut = "12-4" }) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(existByRutAndNotSameEntityMethod);
            Assert.IsTrue(existByRutAndNotSameEntityMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, existByRutAndNotSameEntityMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region Excel

        /// <summary>
        /// Verificar que el metodo api/example/excel funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void Excel()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<ExampleExcelDTO> excelMethod = exampleController.Excel() as NegotiatedContentResult<ExampleExcelDTO>;
            Assert.IsNotNull(excelMethod);
            Assert.IsInstanceOfType(excelMethod.Content, typeof(ExampleExcelDTO));
            Assert.AreEqual(HttpStatusCode.OK, excelMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

        #region PDF

        /// <summary>
        /// Verificar que el metodo api/example/pdf funciona segun lo necesitado
        /// </summary>
        [TestMethod]
        public void PDF()
        {
            ExampleController exampleController = new ExampleController();
            NegotiatedContentResult<ExamplePDFDTO> pdfMethod = exampleController.PDF() as NegotiatedContentResult<ExamplePDFDTO>;
            Assert.IsNotNull(pdfMethod);
            Assert.IsInstanceOfType(pdfMethod.Content, typeof(ExamplePDFDTO));
            Assert.AreEqual(HttpStatusCode.OK, pdfMethod.StatusCode);
            exampleController.Dispose();
        }

        #endregion

    }
}
