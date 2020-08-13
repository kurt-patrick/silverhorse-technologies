using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SilverHorseTech.WebApi.Controllers;
using SilverHorseTech.WebApi.Interfaces.Services;
using SilverHorseTech.WebApi.Models;

namespace SilverHorseTech.WebApi.Tests.Controllers
{
    [TestClass]
    public class PostsControllerTest
    {

        [TestMethod]
        public async Task Post_NullBodyShouldReturnBadRequest()
        {
            // Arrange
            PostsController controller = new PostsController();

            // Act
            var actionResult = await controller.Post(null);
            var contentResult = actionResult as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("post body required", contentResult.Message);
        }

        [TestMethod]
        public async Task Post_InvalidModelShouldReturnBadRequest()
        {
            // Arrange
            PostsController controller = new PostsController();
            controller.ModelState.AddModelError("userId", "required");

            // Act
            var actionResult = await controller.Post(new Post());
            var contentResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.ModelState.Keys.Count);
        }

        [TestMethod]
        public async Task Post_FailureToSaveShouldReturnInternalServerError()
        {
            // Arrange
            Mock<IPostsService> mockService = new Mock<IPostsService>();
            mockService.Setup(ur => ur.UpdatePostAsync(1, null)).Returns<Task<Post>>(null);
            PostsController controller = new PostsController(mockService.Object);

            // Act
            var actionResult = await controller.Post(new Post());
            var contentResult = actionResult as InternalServerErrorResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public async Task Post_SuccessShouldReturnOk()
        {
            // Arrange
            PostsController controller = new PostsController();

            // Act
            var expected = new Post(23, "title", "body");
            var actionResult = await controller.Post(expected);
            var contentResult = actionResult as OkNegotiatedContentResult<Post>;
            var actual = contentResult?.Content;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Id > 0);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Body, actual.Body);
            Assert.AreEqual(expected.Title, actual.Title);
        }


        [TestMethod]
        public async Task Delete_IdLessThanOneShouldReturnBadRequest()
        {
            // Arrange
            PostsController controller = new PostsController();

            // Act
            var actionResult = await controller.Delete(0);
            var contentResult = actionResult as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("invalid id", contentResult.Message);
        }

        [TestMethod]
        public async Task Delete_SuccessShouldReturnOk()
        {
            // Arrange
            PostsController controller = new PostsController();

            // Act
            var actionResult = await controller.Delete(234);
            var responseMessage = actionResult as OkNegotiatedContentResult<HttpResponseMessage>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(responseMessage);
        }

        [TestMethod]
        public async Task Get_ShouldReturnList()
        {
            // Arrange
            PostsController controller = new PostsController();

            // Act
            var actionResult = await controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Post>>;
            var actual = contentResult?.Content;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(actual);
            Assert.AreEqual(100, actual.Count());
            Assert.IsTrue(actual.All(m => m.Id > 0));
            Assert.IsTrue(actual.All(m => m.UserId > 0));
            Assert.IsFalse(actual.All(m => string.IsNullOrWhiteSpace(m.Body)));
            Assert.IsFalse(actual.All(m => string.IsNullOrWhiteSpace(m.Title)));
            Assert.AreNotEqual(actual.First().Id, actual.Last().Id);
        }

        [TestMethod]
        public async Task Get_ShouldReturnById()
        {
            // Arrange
            PostsController controller = new PostsController();

            // Act
            var actionResult = await controller.Get(23);
            var contentResult = actionResult as OkNegotiatedContentResult<Post>;
            var actual = contentResult?.Content;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(actual);
            Assert.AreEqual(23, actual.Id);
            Assert.AreNotEqual(0, actual.UserId);
            Assert.IsFalse(string.IsNullOrWhiteSpace(actual.Body));
            Assert.IsFalse(string.IsNullOrWhiteSpace(actual.Title));
        }

        [TestMethod]
        public async Task Put_NullBodyShouldReturnBadRequest()
        {
            // Arrange
            PostsController controller = new PostsController();

            // Act
            var actionResult = await controller.Put(1, null);
            var contentResult = actionResult as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("post body required", contentResult.Message);
        }

        [TestMethod]
        public async Task Put_IdLessThanOneShouldReturnBadRequest()
        {
            // Arrange
            PostsController controller = new PostsController();

            // Act
            var actionResult = await controller.Put(0, null);
            var contentResult = actionResult as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("invalid id", contentResult.Message);
        }

        [TestMethod]
        public async Task Put_InvalidModelShouldReturnBadRequest()
        {
            // Arrange
            PostsController controller = new PostsController();
            controller.ModelState.AddModelError("userId", "required");

            // Act
            var actionResult = await controller.Put(1, new Post());
            var contentResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.ModelState.Keys.Count);
        }

        [TestMethod]
        public async Task Put_FailureToUpdateShouldReturnInternalServerError()
        {
            // Arrange
            Mock<IPostsService> mockService = new Mock<IPostsService>();
            mockService.Setup(ur => ur.UpdatePostAsync(1, null)).Returns<Task<Post>>(null);
            PostsController controller = new PostsController(mockService.Object);

            // Act
            var actionResult = await controller.Put(1, new Post());
            var contentResult = actionResult as InternalServerErrorResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public async Task Put_SuccessShouldReturnOk()
        {
            // Arrange
            PostsController controller = new PostsController();

            // Act
            var expected = new Post(23, "newtitle", "newbody");
            var actionResult = await controller.Put(45, expected);
            var contentResult = actionResult as OkNegotiatedContentResult<Post>;
            var actual = contentResult?.Content;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(actual);
            Assert.AreEqual(45, actual.Id);
            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Body, actual.Body);
            Assert.AreEqual(expected.Title, actual.Title);
        }

    }
}
