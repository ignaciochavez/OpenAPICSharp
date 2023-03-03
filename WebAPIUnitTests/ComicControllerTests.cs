using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System.Web.Http.Results;
using Business.Tool;
using System.Net;
using Business.DTO;
using Business.Entity;

namespace WebAPIUnitTests
{
    [TestClass]
    public class ComicControllerTests
    {
        ComicController comicController = new ComicController();

        #region DeleteHero

        /// <summary>
        /// Verificar que el metodo api/comic/deletehero funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void ComicControllerDeleteHeroMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> deleteHeroMethod = comicController.DeleteHero(-1) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteHeroMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/deletehero funciona segun lo necesitado al enviar parametros correctos en donde las entidades no existen y no se eliminan
        /// </summary>
        [TestMethod]
        public void ComicControllerDeleteHeroMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<bool> deleteHeroMethod = comicController.DeleteHero(100) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteHeroMethod.Content);
            Assert.IsFalse(deleteHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteHeroMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/deletehero funciona segun lo necesitado al enviar parametros correctos en donde las entidades existen y se eliminan
        /// </summary>
        [TestMethod]
        public void ComicControllerDeleteHeroMethodIsCorrect()
        {
            NegotiatedContentResult<bool> deleteHeroMethod = comicController.DeleteHero(4) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteHeroMethod.Content);
            Assert.IsTrue(deleteHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteHeroMethod.StatusCode);
            comicController.Dispose();
        }

        #endregion

        #region DeleteUser

        /// <summary>
        /// Verificar que el metodo api/comic/deleteuser funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void ComicControllerDeleteUserMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> deleteUserMethod = comicController.DeleteUser(-1) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(deleteUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, deleteUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/deleteuser funciona segun lo necesitado al enviar parametros correctos en donde las entidades no existen y no se eliminan
        /// </summary>
        [TestMethod]
        public void ComicControllerDeleteUserMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<bool> deleteUserMethod = comicController.DeleteUser(100) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteUserMethod.Content);
            Assert.IsFalse(deleteUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/deleteuser funciona segun lo necesitado al enviar parametros correctos en donde las entidades existen y se eliminan
        /// </summary>
        [TestMethod]
        public void ComicControllerDeleteUserHeroMethodIsCorrect()
        {
            NegotiatedContentResult<bool> deleteUserMethod = comicController.DeleteUser(3) as NegotiatedContentResult<bool>;
            Assert.IsNotNull(deleteUserMethod.Content);
            Assert.IsTrue(deleteUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, deleteUserMethod.StatusCode);
            comicController.Dispose();
        }

        #endregion

        #region InsertHero

        /// <summary>
        /// Verificar que el metodo api/comic/inserthero funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ComicControllerInsertHeroMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> insertHeroMethod = comicController.InsertHero(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertHeroMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/inserthero funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ComicControllerInsertHeroMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertHeroMethod = comicController.InsertHero(new ComicInsertHeroDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertHeroMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/inserthero funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ComicControllerInsertHeroMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertHeroMethod = comicController.InsertHero(new ComicInsertHeroDTO("Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test ", 
                                                                                                                "Test ", "Test Test Test Test Test Test Test Test Test Test Test ", "Test ", DateTime.MinValue, "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test " +
                                                                                                                "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test", "Test Test Test Test Test Test ", 0, 0, 0, 0, 0, 0)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertHeroMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/inserthero funciona segun lo necesitado al enviar el objeto con parametros correctos en donde las entidades se insertan
        /// </summary>
        [TestMethod]
        public void ComicControllerInsertHeroMethodIsCorrect()
        {
            NegotiatedContentResult<bool> insertHeroMethod = comicController.InsertHero(new ComicInsertHeroDTO("Star-Lord", "Poderes y habilidades. Star-Lord es un maestro estratega y solucionador de problemas que es un experto en combate cuerpo a cuerpo, varias armas de fuego humanas y alienígenas y técnicas de batalla . Tiene un amplio conocimiento de varias costumbres, sociedades y culturas alienígenas, y un conocimiento considerable sobre abstracciones cósmicas, como Oblivion", Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}Contents\\api-200.png"), "Peter Jason Quill", "M", new DateTime(1976, 1, 1), "Starlord", "Marvel Comics", 69, 20, 33, 50, 25, 70)) as NegotiatedContentResult<bool>;
            Assert.AreNotEqual(0, insertHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, insertHeroMethod.StatusCode);
            comicController.Dispose();
        }

        #endregion

        #region InsertUser

        /// <summary>
        /// Verificar que el metodo api/comic/insertuser funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ComicControllerInsertUserMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> insertUserMethod = comicController.InsertUser(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/insertuser funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ComicControllerInsertUserMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertUserMethod = comicController.InsertUser(new ComicInsertUserDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/insertuser funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ComicControllerInsertUserMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> insertUserMethod = comicController.InsertUser(new ComicInsertUserDTO("Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", DateTime.MinValue, "Test Test Test Test ", null, "Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test ", -1)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(insertUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, insertUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/insertuser funciona segun lo necesitado al enviar el objeto con parametros correctos en donde las entidades se insertan
        /// </summary>
        [TestMethod]
        public void ComicControllerInsertUserMethodIsCorrect()
        {
            NegotiatedContentResult<bool> insertUserMethod = comicController.InsertUser(new ComicInsertUserDTO("5-1", "Diego" , "Muñoz", new DateTime(2008, 11, 15), "123456", true, "diego.munoz@gmail.com", "+56912345678", 3)) as NegotiatedContentResult<bool>;
            Assert.AreNotEqual(0, insertUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, insertUserMethod.StatusCode);
            comicController.Dispose();
        }

        #endregion

        #region Login

        /// <summary>
        /// Verificar que el metodo api/comic/login funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ComicControllerLoginMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> loginMethod = comicController.Login(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(loginMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, loginMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/login funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ComicControllerLoginMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> loginMethod = comicController.Login(new ComicLoginDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(loginMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, loginMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/login funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ComicControllerLoginMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> loginMethod = comicController.Login(new ComicLoginDTO("Test Test Test ", "Test Test Test Test ")) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(loginMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, loginMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/login funciona segun lo necesitado al enviar el objeto con parametros correctos en donde se retorna el token
        /// </summary>
        [TestMethod]
        public void ComicControllerLoginMethodIsCorrect()
        {
            NegotiatedContentResult<string> loginMethod = comicController.Login(new ComicLoginDTO("1-9", "123456")) as NegotiatedContentResult<string>;
            Assert.IsNotNull(loginMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, loginMethod.StatusCode);
            comicController.Dispose();
        }

        #endregion

        #region SelectHero

        /// <summary>
        /// Verificar que el metodo api/comic/selecthero funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void ComicControllerSelectHeroMethodIsInvalidParameters()
        {
            NegotiatedContentResult<MessageVO> selectHeroMethod = comicController.SelectHero(-1) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, selectHeroMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/selecthero funciona segun lo necesitado al enviar parametros correctos en donde las entidades no existen
        /// </summary>
        [TestMethod]
        public void ComicControllerSelectHeroMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<SelectHero> selectHeroMethod = comicController.SelectHero(100) as NegotiatedContentResult<SelectHero>;
            Assert.IsNull(selectHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectHeroMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/selecthero funciona segun lo necesitado al enviar parametros correctos en donde las entidades existen
        /// </summary>
        [TestMethod]
        public void ComicControllerSelectHeroMethodIsCorrect()
        {
            NegotiatedContentResult<SelectHero> selectHeroMethod = comicController.SelectHero(1) as NegotiatedContentResult<SelectHero>;
            Assert.IsNotNull(selectHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectHeroMethod.StatusCode);
            comicController.Dispose();
        }

        #endregion

        #region SelectUser
        
        /// <summary>
        /// Verificar que el metodo api/comic/selectuser funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ComicControllerSelectUserMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> selectUserMethod = comicController.SelectUser(new ComicSelectUserDTO(0, "Test Test Test Test Test Test Test Test Test Test Test ")) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, selectUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/selectuser funciona segun lo necesitado al enviar el objeto con parametros incorrectos en donde las entidades no existen
        /// </summary>
        [TestMethod]
        public void ComicControllerSelectUserMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<SelectUser> selectUserMethod = comicController.SelectUser(new ComicSelectUserDTO(100, Useful.GetAppSettings("TimeZoneInfoName"))) as NegotiatedContentResult<SelectUser>;
            Assert.IsNull(selectUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/selectuser funciona segun lo necesitado al enviar el objeto con parametros correctos en donde las entidades existen
        /// </summary>
        [TestMethod]
        public void ComicControllerSelectUserMethodIsCorrect()
        {
            NegotiatedContentResult<SelectUser> selectUserMethod = comicController.SelectUser(new ComicSelectUserDTO(1, Useful.GetAppSettings("TimeZoneInfoName"))) as NegotiatedContentResult<SelectUser>;
            Assert.IsNotNull(selectUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectUserMethod.StatusCode);
            comicController.Dispose();
        }

        #endregion

        #region UpdateHero

        /// <summary>
        /// Verificar que el metodo api/comic/updatehero funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ComicControllerUpdateHeroMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> updateHeroMethod = comicController.UpdateHero(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateHeroMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/updatehero funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ComicControllerUpdateHeroMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateHeroMethod = comicController.UpdateHero(new ComicUpdateHeroDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateHeroMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/updatehero funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ComicControllerUpdateHeroMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateHeroMethod = comicController.UpdateHero(new ComicUpdateHeroDTO(-1, "Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test ",
                                                                                                                "Test ", -1 , "Test Test Test Test Test Test Test Test Test Test Test ", "Test ", DateTime.MinValue, "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test " +
                                                                                                                "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test", "Test Test Test Test Test Test ", -1, 0, 0, 0, 0, 0, 0)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateHeroMethod.StatusCode);
            comicController.Dispose();
        }
        
        /// <summary>
        /// Verificar que el metodo api/comic/updatehero funciona segun lo necesitado al enviar el objeto con parametros correctos en donde las entidades se actualizan
        /// </summary>
        [TestMethod]
        public void ComicControllerUpdateHeroMethodIsCorrect()
        {
            NegotiatedContentResult<bool> updateHeroMethod = comicController.UpdateHero(new ComicUpdateHeroDTO(1, "Aquaman Update", "El poder más reconocido de Aquaman es la capacidad telepática para comunicarse con la vida marina, la cual puede convocar a grandes distancias.", Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}Contents\\Img\\Aquaman.jpg"), 1, "Orin", "M", new DateTime(1941, 11, 1), "Dweller in the Depths, Swimmer, Waterbearer, Mental Man, Aquaboy, Water Wraith", "DC Comics", 1, 81, 85, 79, 80, 100, 80)) as NegotiatedContentResult<bool>;
            Assert.IsTrue(updateHeroMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateHeroMethod.StatusCode);
            comicController.Dispose();
        }

        #endregion

        #region UpdateUser

        /// <summary>
        /// Verificar que el metodo api/comic/updateuser funciona segun lo necesitado al enviar el objeto nulo
        /// </summary>
        [TestMethod]
        public void ComicControllerUpdateUserMethodIsNullObject()
        {
            NegotiatedContentResult<MessageVO> updateUserMethod = comicController.UpdateUser(null) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/updateuser funciona segun lo necesitado al enviar el objeto con parametros vacios
        /// </summary>
        [TestMethod]
        public void ComicControllerUpdateUserMethodIsEmptyParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateUserMethod = comicController.UpdateUser(new ComicUpdateUserDTO()) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/updateuser funciona segun lo necesitado al enviar el objeto con parametros invalidos
        /// </summary>
        [TestMethod]
        public void ComicControllerUpdateUserMethodIsInvalidParametersOfObject()
        {
            NegotiatedContentResult<MessageVO> updateUserMethod = comicController.UpdateUser(new ComicUpdateUserDTO(-1, "Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test Test Test Test Test Test Test ", DateTime.MinValue, "Test Test Test Test ", null, -1, "Test Test Test Test Test Test Test Test Test Test Test ", "Test Test Test Test ", -1)) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(updateUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, updateUserMethod.StatusCode);
            comicController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/comic/updateuser funciona segun lo necesitado al enviar el objeto con parametros correctos en donde las entidades se insertan
        /// </summary>
        [TestMethod]
        public void ComicControllerUpdateUserMethodIsCorrect()
        {
            NegotiatedContentResult<bool> updateUserMethod = comicController.UpdateUser(new ComicUpdateUserDTO(5, "6-K", "Cristobal", "Lopez", new DateTime(2011, 6, 7), "123456", true, 5, "cristobal.lopez@gmail.com", "+56912345678", 3)) as NegotiatedContentResult<bool>;
            Assert.IsTrue(updateUserMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, updateUserMethod.StatusCode);
            comicController.Dispose();
        }

        #endregion
    }
}
