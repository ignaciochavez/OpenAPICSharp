using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System.Web.Http.Results;
using System.Net;
using Business.Tool;
using System.Web.Http;

namespace WebAPIUnitTest
{
    [TestClass]
    public class CheckControllerTests
    {
        CheckController checkController = new CheckController();
                
        /// <summary>
        /// Verificar que el metodo api/check/check funciona correctamente
        /// </summary>
        [TestMethod]
        public void CheckMethodIsCorrect()
        {            
            NegotiatedContentResult<MessageVO> checkMethod = checkController.Check() as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(checkMethod);
            Assert.IsInstanceOfType(checkMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.OK, checkMethod.StatusCode);     
        }

        /// <summary>
        /// Verificar que el metodo api/check/checkauth funciona correctamente
        /// </summary>
        [TestMethod]
        public void CheckAuthMethodIsCorrect()
        {
            NegotiatedContentResult<MessageVO> checkAuthMethod = checkController.CheckAuth() as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(checkAuthMethod);
            Assert.IsInstanceOfType(checkAuthMethod.Content, typeof(MessageVO));
            Assert.AreEqual(HttpStatusCode.OK, checkAuthMethod.StatusCode);
        }
    }
}
