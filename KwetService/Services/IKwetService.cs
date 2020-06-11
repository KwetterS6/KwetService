using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.Models;

namespace KwetService.Services
{
    public interface IKwetService
    {
        Task<List<Kwet>> Get();
        Task<List<Kwet>> GetByUserId(Guid Id);
        Task<Kwet> InsertKwet(NewKwetModel kwet);
        Task<Kwet> LikeKwet(LikeModel kwet);
    }
}