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
    public class CollectionsControllerTest
    {
        [TestMethod]
        public async Task Get_ShouldReturnList()
        {
            // Arrange
            var controller = new CollectionController();

            // Act
            var actionResult = await controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Aggregate>>;
            var actual = contentResult?.Content;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(actual);
            Assert.AreEqual(30, actual.Count());

            // make sure all 30 records contain valid objects
            Assert.IsTrue(actual.All(m => m.Album != null));
            Assert.IsTrue(actual.All(m => m.Post != null));
            Assert.IsTrue(actual.All(m => m.User != null));

            // make sure randomness has been applied
            Assert.AreNotEqual(30, actual.Count(m => m.Album.Id == actual.First().Album.Id));
            Assert.AreNotEqual(30, actual.Count(m => m.Post.Id == actual.First().Post.Id));
            Assert.AreNotEqual(30, actual.Count(m => m.User.Id == actual.First().User.Id));

        }
    }
}
