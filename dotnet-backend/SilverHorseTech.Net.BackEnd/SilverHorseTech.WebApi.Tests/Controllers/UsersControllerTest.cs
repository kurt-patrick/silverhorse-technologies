using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverHorseTech.WebApi.Controllers;
using SilverHorseTech.WebApi.Models;

namespace SilverHorseTech.WebApi.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public async Task Get_ShouldReturnList()
        {
            // Arrange
            var controller = new UsersController();

            // Act
            var actionResult = await controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<List<User>>;
            var actual = contentResult?.Content;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(actual);
            Assert.AreEqual(10, actual.Count());
            Assert.IsTrue(actual.All(m => m.Id > 0));
            Assert.IsFalse(actual.All(m => string.IsNullOrWhiteSpace(m.Name)));
            Assert.IsFalse(actual.All(m => string.IsNullOrWhiteSpace(m.Username)));
            Assert.AreNotEqual(actual.First().Id, actual.Last().Id);
        }
    }
}
