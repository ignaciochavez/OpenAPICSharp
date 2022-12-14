﻿using System;
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
    public class HeroeControllerTests
    {
        #region Select

        /// <summary>
        /// Verificar que el metodo api/heroe/select funciona segun lo necesitado al enviar parametros vacios
        /// </summary>
        [TestMethod]
        public void HeroeControllerSelectMethodIsEmptyParameters()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> selectMethod = heroeController.Select(0) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod);
            Assert.IsInstanceOfType(selectMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void HeroeControllerSelectMethodIsCorrectAndNotExist()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<Heroe> selectMethod = heroeController.Select(100) as NegotiatedContentResult<Heroe>;
            Assert.IsNotNull(selectMethod);
            Assert.IsNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void HeroeControllerSelectMethodIsCorrect()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<Heroe> selectMethod = heroeController.Select(2) as NegotiatedContentResult<Heroe>;
            Assert.IsNotNull(selectMethod);
            Assert.IsInstanceOfType(selectMethod.Content, typeof(Heroe));
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            heroeController.Dispose();
        }

        #endregion
                
        #region Insert

        /// <summary>
        /// Verificar que el metodo api/heroe/insert funciona segun lo necesitado al enviar el objecto nulo
        /// </summary>
        [TestMethod]
        public void HeroeControllerInsertMethodIsNullObject()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> insertMethod = heroeController.Insert(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/insert funciona segun lo necesitado al enviar el objecto con parametros vacios
        /// </summary>
        [TestMethod]
        public void HeroeControllerInsertMethodIsEmptyParametersOfObject()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> insertMethod = heroeController.Insert(new HeroeInsertDTO() { Name = string.Empty, Description = string.Empty, ImgBase64String = string.Empty, Appearance = DateTimeOffset.MinValue, Home = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            heroeController.Dispose();
        }
        
        /// <summary>
        /// Verificar que el metodo api/heroe/insert funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad existe y no se inserta
        /// </summary>
        [TestMethod]
        public void HeroeControllerInsertMethodIsExist()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> insertMethod = heroeController.Insert(new HeroeInsertDTO() { Name = "Batman", Description = "El poder más reconocido de Aquaman es la capacidad telepática para comunicarse con la vida marina, la cual puede convocar a grandes distancias.", ImgBase64String = "123456asdqwe", Appearance = new DateTimeOffset(new DateTime(1941, 11, 1)), Home = "DC" }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, insertMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/insert funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad no existe y se inserta
        /// </summary>
        [TestMethod]
        public void HeroeControllerInsertMethodIsCorrect()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<Heroe> insertMethod = heroeController.Insert(new HeroeInsertDTO() { Name = "Thor", Description = "El personaje, que se basa en la deidad nórdica homónima, es el dios del trueno asgardiano poseedor del martillo encantado, Mjolnir, que le otorga capacidad de volar y manipular el clima entre sus otros atributos sobrehumanos, además de concentrar su poder.", ImgBase64String = "asdfassf12345678", Appearance = new DateTimeOffset(new DateTime(1962, 8, 1)), Home = "Marvel" }) as NegotiatedContentResult<Heroe>;
            Assert.IsNotNull(insertMethod);
            Assert.IsInstanceOfType(insertMethod.Content, typeof(Heroe));
            Assert.AreEqual(HttpStatusCode.OK, insertMethod.StatusCode);
            heroeController.Dispose();
        }

        #endregion

        #region Update

        /// <summary>
        /// Verificar que el metodo api/heroe/update funciona segun lo necesitado al enviar el objecto nulo
        /// </summary>
        [TestMethod]
        public void HeroeControllerUpdateMethodIsNullObject()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> updateMethod = heroeController.Update(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            heroeController.Dispose();
        }
        
        /// <summary>
        /// Verificar que el metodo api/heroe/update funciona segun lo necesitado al enviar el objecto con parametros vacios
        /// </summary>
        [TestMethod]
        public void HeroeControllerUpdateMethodIsEmptyParametersOfObject()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> updateMethod = heroeController.Update(new Heroe() { Id = 0, Name = string.Empty, Description = string.Empty, ImgBase64String = string.Empty, Appearance = DateTimeOffset.MinValue, Home = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateMethod);
            Assert.IsInstanceOfType(updateMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, updateMethod.StatusCode);
            heroeController.Dispose();
        }
        
        /// <summary>
        /// Verificar que el metodo api/heroe/update funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad no existe y no se actualiza
        /// </summary>
        [TestMethod]
        public void HeroeControllerUpdateMethodIsNotExist()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<bool> updateMethod = heroeController.Update(new Heroe() { Id = 100, Name = "Superman", Description = "Superman es un hombre alto, musculoso, hombre de raza blanca con ojos azules y pelo negro corto con un rizo. Tiene habilidades sobrehumanas, como una fuerza increíble y una piel impermeable.", ImgBase64String = "123456asdqwe", Appearance = new DateTimeOffset(new DateTime(1938, 6, 1)), Home = "DC" }) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod);
            Assert.IsFalse(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/update funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad existe y se actualiza
        /// </summary>
        [TestMethod]
        public void HeroeControllerUpdateMethodIsCorrect()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<bool> updateMethod = heroeController.Update(new Heroe() { Id = 2, Name = "Batman", Description = "El poder más reconocido de Aquaman es la capacidad telepática para comunicarse con la vida marina, la cual puede convocar a grandes distancias.", ImgBase64String = "123456asdqwe", Appearance = new DateTimeOffset(new DateTime(1941, 11, 1)), Home = "DC" }) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(updateMethod);
            Assert.IsTrue(updateMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateMethod.StatusCode);
            heroeController.Dispose();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Verificar que el metodo api/heroe/delete funciona segun lo necesitado al enviar parametros vacios
        /// </summary>
        [TestMethod]
        public void HeroeControllerDeleteMethodIsInvalidParameters()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> deleteMethod = heroeController.Delete(0) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsInstanceOfType(deleteMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void HeroeControllerDeleteMethodIsCorrectAndNotExist()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<bool> deleteMethod = heroeController.Delete(100) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsFalse(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/delete funciona segun lo necesitado al enviar parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void HeroeControllerDeleteMethodIsCorrect()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<bool> deleteMethod = heroeController.Delete(1) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteMethod);
            Assert.IsTrue(deleteMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteMethod.StatusCode);
            heroeController.Dispose();
        }

        #endregion

        #region List

        /// <summary>
        /// Verificar que el metodo api/heroe/list funciona segun lo necesitado al enviar el objecto nulo
        /// </summary>
        [TestMethod]
        public void HeroeControllerListMethodIsNullObject()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> listMethod = heroeController.List(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/list funciona segun lo necesitado al enviar el objecto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void HeroeControllerListMethodIsInvalidParametersOfObject()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> listMethod = heroeController.List(new HeroeListDTO() { PageIndex = 0, PageSize = 0 }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, listMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/list funciona segun lo necesitado al enviar el objecto con parametros correctos
        /// </summary>
        [TestMethod]
        public void HeroeControllerListMethodIsCorrect()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<List<Heroe>> listMethod = heroeController.List(new HeroeListDTO() { PageIndex = 1, PageSize = 10 }) as NegotiatedContentResult<List<Heroe>>;
            Assert.IsNotNull(listMethod);
            Assert.IsInstanceOfType(listMethod.Content, typeof(List<Heroe>));
            Assert.AreEqual(HttpStatusCode.OK, listMethod.StatusCode);
            heroeController.Dispose();
        }

        #endregion

        #region TotalRecords

        [TestMethod]
        public void HeroeControllerTotalRecordsMethodIsCorrect()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<long> countMethod = heroeController.TotalRecords() as NegotiatedContentResult<long>;
            Assert.IsNotNull(countMethod);
            Assert.IsInstanceOfType(countMethod.Content, typeof(long));
            Assert.AreEqual(HttpStatusCode.OK, countMethod.StatusCode);
            heroeController.Dispose();
        }
        #endregion

        #region ExistByNameAndNotSameEntity

        /// <summary>
        /// Verificar que el metodo api/heroe/existbynameandnotsameentity funciona segun lo necesitado al enviar el objecto nulo
        /// </summary>
        [TestMethod]
        public void HeroeControllerExistByNameAndNotSameEntityMethodIsNullObject()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> existByNameAndNotSameEntityMethod = heroeController.ExistByNameAndNotSameEntity(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(existByNameAndNotSameEntityMethod);
            Assert.IsInstanceOfType(existByNameAndNotSameEntityMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, existByNameAndNotSameEntityMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/existbynameandnotsameentity funciona segun lo necesitado al enviar el objecto con parametros vacios
        /// </summary>
        [TestMethod]
        public void HeroeControllerExistByNameAndNotSameEntityMethodIsEmptyParametersOfObject()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<MessageVO> existByNameAndNotSameEntityMethod = heroeController.ExistByNameAndNotSameEntity(new HeroeExistByNameAndNotSameEntityDTO() { Id = 0, Name = string.Empty }) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(existByNameAndNotSameEntityMethod);
            Assert.IsInstanceOfType(existByNameAndNotSameEntityMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.BadRequest, existByNameAndNotSameEntityMethod.StatusCode);
            heroeController.Dispose();
        }        

        /// <summary>
        /// Verificar que el metodo api/heroe/existbynameandnotsameentity funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void HeroeControllerExistByNameAndNotSameEntityMethodIsNotExist()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<bool> existByNameAndNotSameEntityMethod = heroeController.ExistByNameAndNotSameEntity(new HeroeExistByNameAndNotSameEntityDTO() { Id = 100, Name = "Superman" }) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(existByNameAndNotSameEntityMethod);
            Assert.IsFalse(existByNameAndNotSameEntityMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, existByNameAndNotSameEntityMethod.StatusCode);
            heroeController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/heroe/existbynameandnotsameentity funciona segun lo necesitado al enviar el objecto con parametros correctos en donde la entidad existe
        /// </summary>
        [TestMethod]
        public void HeroeControllerExistByNameAndNotSameEntityMethodIsCorrect()
        {
            HeroeController heroeController = new HeroeController();
            NegotiatedContentResult<bool> existByNameAndNotSameEntityMethod = heroeController.ExistByNameAndNotSameEntity(new HeroeExistByNameAndNotSameEntityDTO() { Id = 4, Name = "Linterna Verde" }) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(existByNameAndNotSameEntityMethod);
            Assert.IsTrue(existByNameAndNotSameEntityMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, existByNameAndNotSameEntityMethod.StatusCode);
            heroeController.Dispose();
        }

        #endregion
    }
}
