using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.DatastoreSettings;
using KwetService.Models;
using KwetService.Repositories;
using Mongo2Go;
using Moq;
using Xunit;

namespace KwetServiceTests.Repository
{
    public class KwetRepositoryTest
    {
        private readonly IKwetRepository _kwetRepository;
        private readonly MongoDbRunner _mongoDbRunner;

        public KwetRepositoryTest()
        {
            _mongoDbRunner = MongoDbRunner.Start();
            var settings = new KwetstoreDatabaseSettings()
            {
                ConnectionString = _mongoDbRunner.ConnectionString,
                DatabaseName = "KwetIntergrationTest",
                KwetCollectionName = "TestCollection"
            };
            _kwetRepository = new KwetRepository(settings);
        }

        [Fact]
        public async Task CreateNewKwet_PlaceSuccess_ReturnsPlacedKwet()
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

            var result1 = await _kwetRepository.Create(kwet1);
            var result2 = await _kwetRepository.Create(kwet2);

            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotEqual(result1.TimeStamp, result2.TimeStamp);
            Assert.Equal(kwet1.Likes, result2.Likes);
            Assert.Equal(kwet2.UserId, result2.UserId);
        }

        [Fact]
        public async Task LikeKwet_UpdateSuccess_ReturnsLikedKwet()
        {
            var userGuid = Guid.NewGuid();
            var likeUser = Guid.NewGuid();
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
            var like = new Likes()
            {
                Name = "LikeUser",
                userId = likeUser
            };

            var kwetWithLikes = kwet1;
            kwetWithLikes.Likes.Add(like);
            
            await _kwetRepository.Create(kwet1);
            var result = await _kwetRepository.Update(kwetWithLikes);
            
            Assert.Single(result.Likes);
            Assert.Equal(result.Likes[0].Name, like.Name);
            Assert.Equal(result.Likes[0].userId, like.userId);

        }

        [Fact]
        public async Task UnlikeKwet_UnLikeSuccess_ReturnsUnlikedKwet()
        {
            var userGuid = Guid.NewGuid();
            var likeUser = Guid.NewGuid();
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
            var like = new Likes()
            {
                Name = "LikeUser",
                userId = likeUser
            };
            
            kwet1.Likes.Add(like);
            
            var kwetWithoutLikes = await _kwetRepository.Create(kwet1);
            kwetWithoutLikes.Likes.Remove(like);
            
            var result = await _kwetRepository.Update(kwetWithoutLikes);
            Assert.Empty(result.Likes);
        }


        [Fact]
        public async Task GetKwetsById_GetFailBadGuid_ReturnsNull()
        {
            var guid = Guid.Empty;
            var result = await _kwetRepository.GetByUserId(guid);
            Assert.Equal(new List<Kwet>(), result);
        }

        [Fact]
        public async Task GetKwets_GetSuccess_ReturnsKwetList()
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

            await _kwetRepository.Create(kwet1);
            await _kwetRepository.Create(kwet2);

            var result = await _kwetRepository.Get();
            
            Assert.Equal(2, result.Count);
            
        }
        [Fact]
        public async Task GetKwetsById_GetSuccess_ReturnsKwetList()
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
                UserId = new Guid(),
                UserName = "TestUser",
                Message = "This is my other placed Kwet",
                TimeStamp = timeStamp.AddHours(1),
                Likes =  new List<Likes>()
            };

            await _kwetRepository.Create(kwet1);
            await _kwetRepository.Create(kwet2);

            var result = await _kwetRepository.GetByUserId(userGuid);
            
            Assert.Single(result);
            
        }
    }
}