using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.Exceptions;
using KwetService.Helpers;
using KwetService.Models;
using KwetService.Repositories;
using KwetService.Services;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace KwetServiceTests.ServiceTests
{
    public class KwetServiceTest
    {
        private readonly IKwetService _kwetService;
        private readonly Mock<IKwetRepository> _repository;
        private readonly Mock<IJwtIdClaimReaderHelper> _jwtIdClaimReaderHelper;


        public KwetServiceTest()
        {
            _repository = new Mock<IKwetRepository>();
            _jwtIdClaimReaderHelper = new Mock<IJwtIdClaimReaderHelper>();

            _kwetService = new KwetService.Services.KwetService(
                _repository.Object, _jwtIdClaimReaderHelper.Object
                );
        }

        [Fact]
        public async Task InsertKwets_ValidKwet_ReturnsPlacedKwet()
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

            _repository.Setup(x => x.Create(It.IsAny<Kwet>())).ReturnsAsync(returnKwet);
            _jwtIdClaimReaderHelper.Setup(x => x.getUserIdFromToken(jwt)).Returns(userGuid);

            var result = await _kwetService.InsertKwet(newkwet, jwt);

            Assert.Equal(returnKwet.Message, result.Message);
            Assert.Equal(returnKwet.KwetId, result.KwetId);
        }
        
        [Fact]
        public async Task InsertKwets_InvalidJWT_ThrowsException()
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

            _repository.Setup(x => x.Create(It.IsAny<Kwet>())).ReturnsAsync(returnKwet);
            _jwtIdClaimReaderHelper.Setup(x => x.getUserIdFromToken(jwt)).Returns(Guid.Empty);

            var result = await Assert.ThrowsAsync<JwtInvalidException>(() => 
                _kwetService.InsertKwet(newkwet, jwt));

            Assert.IsType<JwtInvalidException>(result);
        }
        
        [Fact]
        public async Task LikeKwet_SuccessfullLike_ReturnsKwetWithLikes()
        { 
            var userGuid = Guid.NewGuid();
            var kwetGuid = Guid.NewGuid();
            var timeStamp = DateTime.Now;
            
            var likedKwet = new Kwet()
            {
                KwetId = kwetGuid,
                UserId = userGuid,
                UserName = "TestUser",
                Message = "This is my placed Kwet",
                TimeStamp = timeStamp,
                Likes =  new List<Likes>()
            };

            var likemodel = new LikeModel()
            {
                Id = userGuid,
                KwetId = kwetGuid,
                UserName = "TestUser"
            };
            var returnKwet = likedKwet;
            returnKwet.Likes.Add(new Likes(){userId = likemodel.Id, Name = likemodel.UserName});

            _repository.Setup(x => x.Get(likemodel.KwetId)).ReturnsAsync(likedKwet);
            _repository.Setup(x => x.Update(It.IsAny<Kwet>())).ReturnsAsync(returnKwet);
            var result = await _kwetService.LikeKwet(likemodel);
            
            Assert.NotEmpty(result.Likes);
            Assert.Equal(returnKwet.Likes, result.Likes);
            Assert.Equal(likemodel.KwetId, result.KwetId);
        }
    }
}