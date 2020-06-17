using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.Controllers;
using KwetService.Models;
using KwetService.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KwetServiceTests.Controller
{
    public class KwetControllerTest
    {
        private readonly KwetController _kwetController;
        private readonly Mock<IKwetService> _kwetService;

        public KwetControllerTest()
        {
            _kwetService = new Mock<IKwetService>();
            _kwetController = new KwetController(_kwetService.Object);
        }

        [Fact]
        public async Task InsertKwet_ValidKwet_ReturnsKwet()
        {
            var userGuid = Guid.NewGuid();
            var timeStamp = DateTime.Now;
            var newkwet = new NewKwetModel()
            {
                Id = userGuid.ToString(),
                Message = "This is my placed Kwet",
                UserName = "TestUser"
            };

            var returnKwet = new Kwet()
            {
                KwetId = new Guid(),
                UserId = userGuid,
                UserName = "TestUser",
                Message = "This is my placed Kwet",
                TimeStamp = timeStamp,
                Likes =  new List<Likes>()
            };

            _kwetService.Setup(x => x.InsertKwet(It.IsAny<NewKwetModel>())).ReturnsAsync(returnKwet);
            var result = await _kwetController.Insert(newkwet) as ObjectResult;

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(returnKwet.KwetId, ((Kwet) result.Value).KwetId);
        }
    }
}