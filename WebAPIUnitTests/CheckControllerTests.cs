using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System.Web.Http.Results;
using System.Net;
using Business.Tool;
using System.Web.Http;

namespace WebAPIUnitTests
{
    [TestClass]
    public class CheckControllerTests
    {
        CheckController checkController = new CheckController();
        
        /// <summary>
        /// Verificar que el metodo api/check/check funciona correctamente
        /// </summary>
        [TestMethod]
        public void CheckControllerCheckMethodIsCorrect()
        {
            NegotiatedContentResult<MessageVO> checkMethod = checkController.Check() as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(checkMethod);
            Assert.AreEqual(HttpStatusCode.OK, checkMethod.StatusCode);
            checkController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/check/checkauth funciona correctamente
        /// </summary>
        [TestMethod]
        public void CheckControllerCheckAuthMethodIsCorrect()
        {
            NegotiatedContentResult<MessageVO> checkAuthMethod = checkController.CheckAuth() as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(checkAuthMethod);
            Assert.AreEqual(HttpStatusCode.OK, checkAuthMethod.StatusCode);
            checkController.Dispose();
        }
    }
}
