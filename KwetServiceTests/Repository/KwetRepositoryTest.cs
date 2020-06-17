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
            Assert.Equal(result1.Likes, result2.Likes);
            Assert.Equal(result1.UserId, result2.UserId);
        }
    }
}