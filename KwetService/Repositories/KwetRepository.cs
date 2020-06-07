using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.DatastoreSettings;
using KwetService.Models;
using MongoDB.Driver;

namespace KwetService.Repositories
{
    public class KwetRepository : IKwetRepository
    {
        private readonly IMongoCollection<Kwet> _kwets;
        public KwetRepository(IKwetstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _kwets = database.GetCollection<Kwet>(settings.KwetCollectionName);
        }

        public async Task<List<Kwet>> Get() =>
            await _kwets.Find(kwet => true).ToListAsync();

        public async Task<List<Kwet>> GetByUserId(Guid id) =>
            await _kwets.Find(kwet => kwet.UserId == id).ToListAsync();

        public async Task<Kwet> Create(Kwet kwet)
        {
            await _kwets.InsertOneAsync(kwet);
            return kwet;
        }
    }
}