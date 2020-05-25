using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.Models;
using KwetService.Repositories;

namespace KwetService.Services
{
    public class KwetService : IKwetService
    {
        private readonly IKwetRepository _repository;

        public KwetService(IKwetRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Kwet>> Get()
        {
            return await _repository.Get();
        }

        public async Task<Kwet> InsertKwet(Kwet kwet)
        {
            return await _repository.Create(kwet);
        }
    }
}