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
        public void ExampleControllerSelectMethodIsEmptyParameters()
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
        public void ExampleControllerSelectMethodIsInvalidParameters()
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
        public void ExampleControllerSelectMethodIsCorrectAndNotExist()
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
        public void ExampleControllerSelectMethodIsCorrect()
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
        public void ExampleControllerExistMethodIsEmptyParameters()
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
        public void ExampleControllerExistMethodIsInvalidParameters()
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
        public void ExampleControllerExistMethodIsCorrectAndNotExist()
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
        public void ExampleControllerExistMethodIsCorrect()
        {
            NegotiatedContentResult<bool> existMethod = exampleController.Exist("1-9") as NegotiatedContentResult<bool>;
            Assert.IsNotNull(existMethod);
            Assert.IsTrue(existMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, existMethod.StatusCode);
        }

        #endregion

        #region Insert

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = string.Empty, Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = "12332231", Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad existe y no se inserta
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsExist()
        {
            NegotiatedContentResult<MessageVO> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = "1-9", Name = "Pedro", LastName = "Gutierrez", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "1234qwer" }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/insert funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad no existe y se inserta
        /// </summary>
        [TestMethod]
        public void ExampleControllerInsertMethodIsCorrect()
        {
            NegotiatedContentResult<Example> insertMethod = exampleController.Insert(new ExampleInsertDTO() { Rut = "18-3", Name = "Emanuel", LastName = "Leiva", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "4321rewq" }) as NegotiatedContentResult<Example>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(Example));
            Assert.AreEqual(HttpStatusCode.OK, insertMethod.StatusCode);
        }

        #endregion

        #region Update

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(new ExampleUpdateDTO() { Rut = string.Empty, Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateMethod = exampleController.Update(new ExampleUpdateDTO() { Rut = "12332231", Name = string.Empty, LastName = string.Empty, BirthDate = DateTimeOffset.MinValue, Active = false, Password = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad no existe y no se actualiza
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsNotExist()
        {
            NegotiatedContentResult<bool> updateMethod = exampleController.Update(new ExampleUpdateDTO() { Rut = "21-3", Name = "Leonel", LastName = "Gonzalez", BirthDate = DateTimeOffset.UtcNow.Date, Active = true, Password = "vcxz9876" }) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod);
            Assert.IsFalse(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/update funciona segun lo necesitado al enviar el objeto con parametros correctos en donde la entidad existe y se actualiza
        /// </summary>
        [TestMethod]
        public void ExampleControllerUpdateMethodIsCorrect()
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
        public void ExampleControllerDeleteMethodIsEmptyParameters()
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
        public void ExampleControllerDeleteMethodIsInvalidParameters()
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
        public void ExampleControllerDeleteMethodIsCorrectAndNotExist()
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
        public void ExampleControllerDeleteMethodIsCorrect()
        {
            NegotiatedContentResult<bool> deleteMethod = exampleController.Delete("1-9") as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsTrue(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
        }

        #endregion

        #region List

        /// <summary>
        /// Verificar que el metodo api/example/list funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ExampleControllerListMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = exampleController.List(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/list funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ExampleControllerListMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> listMethod = exampleController.List(new ExampleListDTO() { PageIndex = 0, PageSize = 0 }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
        }

        /// <summary>
        /// Verificar que el metodo api/example/list funciona segun lo necesitado al enviar el objeto con parametros correctos
        /// </summary>
        [TestMethod]
        public void ExampleControllerListMethodIsCorrect()
        {
            NegotiatedContentResult<List<Example>> listMethod = exampleController.List(new ExampleListDTO() { PageIndex = 1, PageSize = 10 }) as NegotiatedContentResult<List<Example>>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(List<Example>));
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
        }

        #endregion

        #region Count

        [TestMethod]
        public void ExampleControllerCountMethodIsCorrect()
        {
            NegotiatedContentResult<long> countMethod = exampleController.Count() as NegotiatedContentResult<long>;
            Assert.IsNotNull(countMethod);
            Assert.IsInstanceOfType(countMethod.Content, typeof(long));
            Assert.AreEqual(HttpStatusCode.OK, countMethod.StatusCode);
        }
        #endregion
    }
}
