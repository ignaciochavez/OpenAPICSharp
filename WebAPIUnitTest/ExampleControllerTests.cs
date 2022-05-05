using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System.Web.Http.Results;
using Business.Entity;
using System.Net;
using Business.Tool;
using Business.DTO;
using System.Collections.Generic;

namespace WebAPIUnitTest
{
    [TestClass]
    public class ExampleControllerTests
    {
        ExampleController exampleController = new ExampleController();

        #region Select

        /// <summary>
        /// Verificar que el metodo api/example/select funciona segun lo necesitado al enviar parametros vacios
        /// </summary>
        [TestMethod]
        public void SelectMethodIsEmptyParameters()
        {
            NegotiatedContentResult<MessageVO> selectMethod = exampleController.Select(string.Empty) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod);
            Assert.IsInstanceOfType(selectMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/select funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void SelectMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> selectMethod = exampleController.Select("12332231") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod);
            Assert.IsInstanceOfType(selectMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void SelectMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<Example> selectMethod = exampleController.Select("21-3") as NegotiatedContentResult<Example>;
            Assert.IsNotNull(selectMethod);
            Assert.IsNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void SelectMethodIsCorrect()
        {
            NegotiatedContentResult<Example> selectMethod = exampleController.Select("1-9") as NegotiatedContentResult<Example>;
            Assert.IsNotNull(selectMethod);
            Assert.IsInstanceOfType(selectMethod.Content, typeof(Example));
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
        }

        #endregion

        #region Exist

        /// <summary>
        /// Verificar que el metodo api/example/exist funciona segun lo necesitado al enviar parametros vacios
        /// </summary>
        [TestMethod]
        public void ExistMethodIsEmptyParameters()
        {
            NegotiatedContentResult<MessageVO> existMethod = exampleController.Exist(string.Empty) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(existMethod);
            Assert.IsInstanceOfType(existMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, existMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/exist funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void ExistMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> existMethod = exampleController.Exist("12332231") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(existMethod);
            Assert.IsInstanceOfType(existMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, existMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/exist funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void ExistMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<bool> existMethod = exampleController.Exist("21-3") as NegotiatedContentResult<bool>;
            Assert.IsNotNull(existMethod);
            Assert.IsFalse(existMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, existMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/exist funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void ExistMethodIsCorrect()
        {
            NegotiatedContentResult<bool> existMethod = exampleController.Exist("1-9") as NegotiatedContentResult<bool>;
            Assert.IsNotNull(existMethod);
            Assert.IsTrue(existMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, existMethod.StatusCode);
        }

        #endregion

        #region Insert

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objecto nulo
        /// </summary>
        [TestMethod]
        public void InsertMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objecto con parametros vacios
        /// </summary>
        [TestMethod]
        public void InsertMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = string.Empty, Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objecto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void InsertMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = "12332231", Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad existe y no se inserta
        /// </summary>
        [TestMethod]
        public void InsertMethodIsExist()
        {
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = "1-9", Name = "Pedro", LastName = "Gutierrez", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "1234qwer" }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad no existe y se inserta
        /// </summary>
        [TestMethod]
        public void InsertMethodIsCorrect()
        {
            NegotiatedContentResult<Example> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = "18-3", Name = "Emanuel", LastName = "Leiva", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "4321rewq" }) as NegotiatedContentResult<Example>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(Example));
            Assert.AreEqual(HttpStatusCode.OK, insertMethod.StatusCode);
        }

        #endregion

        #region Update

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objecto nulo
        /// </summary>
        [TestMethod]
        public void UpdateMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objecto con parametros vacios
        /// </summary>
        [TestMethod]
        public void UpdateMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(new ExampleUpdateDTO() { Rut = string.Empty, Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objecto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void UpdateMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(new ExampleUpdateDTO() { Rut = "12332231", Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad no existe y no se actualiza
        /// </summary>
        [TestMethod]
        public void UpdateMethodIsNotExist()
        {
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(new ExampleUpdateDTO() { Rut = "21-3", Name = "Leonel", LastName = "Gonzalez", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "vcxz9876" }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad existe y se actualiza
        /// </summary>
        [TestMethod]
        public void UpdateMethodIsCorrect()
        {
            NegotiatedContentResult<bool> updateMethod = exampleController.Update(new ExampleUpdateDTO() { Rut = "1-9", Name = "Pedro", LastName = "Gutierrez", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "1234qwer" }) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod);
            Assert.IsTrue(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Verificar que el metodo api/example/delete funciona segun lo necesitado al enviar parametros vacios
        /// </summary>
        [TestMethod]
        public void DeleteMethodIsEmptyParameters()
        {
            NegotiatedContentResult<MessageVO> deleteMethod = exampleController.Delete(string.Empty) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsInstanceOfType(deleteMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/delete funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void DeleteMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> deleteMethod = exampleController.Delete("12332231") as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsInstanceOfType(deleteMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void DeleteMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<bool> deleteMethod = exampleController.Delete("21-3") as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsFalse(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void DeleteMethodIsCorrect()
        {
            NegotiatedContentResult<bool> deleteMethod = exampleController.Delete("1-9") as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsTrue(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
        }

        #endregion

        #region List

        /// <summary>
        /// Verificar que el metodo api/example/list funciona segun lo necesitado al enviar el objecto nulo
        /// </summary>
        [TestMethod]
        public void ListMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = exampleController.List(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/list funciona segun lo necesitado al enviar el objecto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ListMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = exampleController.List(new ExampleListDTO() { PageIndex = -1, PageSize = 0 }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/list funciona segun lo necesitado al enviar el objecto con parametros correctos
        /// </summary>
        [TestMethod]
        public void ListMethodIsCorrect()
        {
            NegotiatedContentResult<List<Example>> listMethod = exampleController.List(new ExampleListDTO() { PageIndex = 0, PageSize = 10 }) as NegotiatedContentResult<List<Example>>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(List<Example>));
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
        }

        #endregion
    }
}
