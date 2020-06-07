using System;
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
        
        public async Task<List<Kwet>> GetByUserId(Guid id)
        {
            return await _repository.GetByUserId(id);
        }

        public async Task<Kwet> InsertKwet(NewKwetModel kwet)
        {
            var newKwet = new Kwet
            {
                KwetId = new Guid(),
                UserId = Guid.Parse(kwet.Id),
                UserName = kwet.UserName,
                Message = kwet.Message,
                TimeStamp = DateTime.Now
                
            };
            return await _repository.Create(newKwet);
        }
    }
}