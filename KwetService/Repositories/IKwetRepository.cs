using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.Models;

namespace KwetService.Repositories
{
    public interface IKwetRepository
    {
        Task<List<Kwet>> Get();
        Task<Kwet> Get(Guid id);

        Task<List<Kwet>> GetByUserId(Guid id);

         Task<Kwet> Create(Kwet kwet);
         
         Task<Kwet> Update(Kwet likedKwet);

    }
}