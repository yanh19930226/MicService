using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MicService.User.Api.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MicService.User.Api.UnitTest
{
    public class UserControllerUnitTests
    {
        private UserContext GetUserContext()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var userContext = new UserContext(options);
            userContext.Users.Add(new Models.Domain.User
            {
                Id = 1,
                Name = "testUser"
            });
            userContext.SaveChanges();
            return userContext;
        }
        private (UserController controller, UserContext usercontext) GetUserContriller()
        {
            var _context = GetUserContext();
            var loggerMoq = new Mock<ILogger<UserController>>();
            var logger = loggerMoq.Object;
            return (controller: new UserController(_context), usercontext: _context);
        }
        [Fact]
        public async Task GetReturnRightUser_WithExpectedParameters()
        {
            (Controllers.UserController controller, UserContext usercontext) = GetUserContriller();
            var response = await controller.Get();
            //Assert.IsType<JsonResult>(response);
            var result = response.Should().BeOfType<JsonResult>().Subject;
            var appUser = result.Value.Should().BeAssignableTo<Models.Domain.User>().Subject;
            appUser.Id.Should().Be(1);
            appUser.Name.Should().Be("testUser");
        }
        [Fact]
        public async Task Pathc_ReturnNewName_WithExpectdNewNameParameter()
        {
            (Controllers.UserController controller, UserContext usercontext) = GetUserContriller();
            var document = new JsonPatchDocument<Models.Domain.User>();
            document.Replace(user => user.Name, "updatedUser");
            var response = await controller.Patch(document);
            var result = response.Should().BeOfType<JsonResult>().Subject;
            //assert response
            var appUser = result.Value.Should().BeAssignableTo<Models.Domain.User>().Subject;
            appUser.Name.Should().Be("updatedUser");
            //assert name of value in context
            var userModel = await usercontext.Users.SingleOrDefaultAsync(u => u.Id == 1);
            userModel.Should().NotBeNull();
            userModel.Name.Should().Be("updatedUser");
        }
        [Fact]
        public async Task Pathc_ReturnNewProperties_WithExpectdAddProperties()
        {
            (Controllers.UserController controller, UserContext usercontext) = GetUserContriller();
            var document = new JsonPatchDocument<Models.Domain.User>();
            document.Replace(user => user.Properties, new List<Models.Domain.UserProperty> {
                new Models.Domain.UserProperty(){Key="test_key",Value="≤‚ ‘",Text="≤‚ ‘"}
            });
            var response = await controller.Patch(document);
            var result = response.Should().BeOfType<JsonResult>().Subject;
            //assert response
            var appUser = result.Value.Should().BeAssignableTo<Models.Domain.User>().Subject;
            appUser.Properties.Count.Should().Be(1);
            appUser.Properties.First().Value.Should().Be("≤‚ ‘");
            appUser.Properties.First().Key.Should().Be("test_key");
            //assert name of value in context
            var userModel = await usercontext.Users.SingleOrDefaultAsync(u => u.Id == 1);
            userModel.Properties.Count.Should().Be(1);
            userModel.Properties.First().Value.Should().Be("≤‚ ‘");
            userModel.Properties.First().Key.Should().Be("test_key");
        }
        [Fact]
        public async Task Pathc_ReturnNewProperties_WithExpectdRemoveProperty()
        {
            (Controllers.UserController controller, UserContext usercontext) = GetUserContriller();
            var document = new JsonPatchDocument<Models.Domain.User>();
            document.Replace(user => user.Properties, new List<Models.Domain.UserProperty>
            {
            });
            var response = await controller.Patch(document);
            var result = response.Should().BeOfType<JsonResult>().Subject;
            //assert response
            var appUser = result.Value.Should().BeAssignableTo<Models.Domain.User>().Subject;
            appUser.Properties.Should().BeEmpty();
            //assert name of value in context
            var userModel = await usercontext.Users.SingleOrDefaultAsync(u => u.Id == 1);
            userModel.Properties.Should().BeEmpty();
        }
    }
}
