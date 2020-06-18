using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.Controllers;
using KwetService.Helpers;
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
        private readonly Mock<IJwtIdClaimReaderHelper> _jwtIdClaimReaderHelper;


        public KwetControllerTest()
        {
            _jwtIdClaimReaderHelper = new Mock<IJwtIdClaimReaderHelper>();
            _kwetService = new Mock<IKwetService>();
            _kwetController = new KwetController(_kwetService.Object);
        }

        [Fact]
        public async Task InsertKwet_ValidKwet_ReturnsKwet()
        {
            var userGuid = Guid.NewGuid();
            var timeStamp = DateTime.Now;
            const string jwt = "";

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
            _jwtIdClaimReaderHelper.Setup(x => x.getUserIdFromToken(jwt)).Returns(userGuid);
            _kwetService.Setup(x => x.InsertKwet(It.IsAny<NewKwetModel>(), jwt)).ReturnsAsync(returnKwet);
            var result = await _kwetController.Insert(newkwet, jwt) as ObjectResult;
        
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(returnKwet.KwetId, ((Kwet) result.Value).KwetId);
        }

        [Fact]
        public async Task GetKwet_GetSuccess_ReturnKwetList()
        {
            var userGuid = Guid.NewGuid();
            var timeStamp = DateTime.Now;

            var kwet1 = new Kwet()
            {
                KwetId = new Guid(),
                UserId = userGuid,
                UserName = "TestUser",
                Message = "This is my placed Kwet",
                TimeStamp = timeStamp,
                Likes =  new List<Likes>()
            };
            var kwet2= new Kwet()
            {
                KwetId = new Guid(),
                UserId = userGuid,
                UserName = "TestUser",
                Message = "This is my other placed Kwet",
                TimeStamp = timeStamp.AddHours(1),
                Likes =  new List<Likes>()
            };
            var kwets = new List<Kwet>();
            kwets.Add(kwet1);
            kwets.Add(kwet2);

            _kwetService.Setup(x => x.Get()).ReturnsAsync(kwets);
            var result = await _kwetController.Get() as ObjectResult;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(kwets, ((List<Kwet>) result.Value));
        }
        
        [Fact]
        public async Task GetKwetbyId_GetSuccess_ReturnKwetList()
        {
            var userGuid = Guid.NewGuid();
            var timeStamp = DateTime.Now;

            var kwet1 = new Kwet()
            {
                KwetId = new Guid(),
                UserId = userGuid,
                UserName = "TestUser",
                Message = "This is my placed Kwet",
                TimeStamp = timeStamp,
                Likes =  new List<Likes>()
            };
            var kwet2= new Kwet()
            {
                KwetId = new Guid(),
                UserId = userGuid,
                UserName = "TestUser",
                Message = "This is my other placed Kwet",
                TimeStamp = timeStamp.AddHours(1),
                Likes =  new List<Likes>()
            };
            var kwets = new List<Kwet>();
            kwets.Add(kwet1);
            kwets.Add(kwet2);

            _kwetService.Setup(x => x.GetByUserId(userGuid)).ReturnsAsync(kwets);
            var result = await _kwetController.Get(userGuid) as ObjectResult;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(kwets, ((List<Kwet>) result.Value));
        }
    }
}