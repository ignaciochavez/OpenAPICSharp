using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System.Web.Http.Results;
using Business.Tool;
using System.Net;
using Business.Entity;

namespace WebAPIUnitTests
{
    [TestClass]
    public class BiographyControllerTests
    {
        BiographyController biographyController = new BiographyController();

        /// <summary>
        /// Verificar que el metodo api/biography/select funciona segun lo necesitado al enviar parametros invalidos
        /// </summary>
        [TestMethod]
        public void BiographyControllerSelectMethodIsInvalidParameter()
        {
            NegotiatedContentResult<MessageVO> selectMethod = biographyController.Select(0) as NegotiatedContentResult<MessageVO>;
            Assert.IsNotNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, selectMethod.StatusCode);
            biographyController.Dispose();
        }

        /// <summary>
        /// Verificar que el metodo api/biography/select funciona segun lo necesitado al enviar parametros correctos en donde la entidad no existe
        /// </summary>
        [TestMethod]
        public void BiographyControllerSelectMethodIsCorrectAndNotExist()
        {
            NegotiatedContentResult<Biography> selectMethod = biographyController.Select(100) as NegotiatedContentResult<Biography>;
            Assert.IsNull(selectMethod.Content);
            Assert.AreEqual(HttpStatusCode.OK, selectMethod.StatusCode);
            biographyController.Dispose();
        }
    }
}
