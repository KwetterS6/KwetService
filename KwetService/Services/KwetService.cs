using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KwetService.Exceptions;
using KwetService.Helpers;
using KwetService.Models;
using KwetService.Repositories;

namespace KwetService.Services
{
    public class KwetService : IKwetService
    {
        private readonly IKwetRepository _repository;
        private readonly IJwtIdClaimReaderHelper _jwtIdClaimReaderHelper;


        public KwetService(IKwetRepository repository, IJwtIdClaimReaderHelper jwtIdClaimReaderHelper)
        {
            _repository = repository;
            _jwtIdClaimReaderHelper = jwtIdClaimReaderHelper;
        }

        public async Task<List<Kwet>> Get()
        {
            var sortedKwets = await _repository.Get();
            sortedKwets.Sort((x, y) => DateTime.Compare(y.TimeStamp, x.TimeStamp)); 
            return sortedKwets;
        }
        
        public async Task<List<Kwet>> GetByUserId(Guid id)
        {
            return await _repository.GetByUserId(id);
        }

        public async Task<Kwet> InsertKwet(NewKwetModel kwet, string token)
        {
            if (Guid.Parse(kwet.Id) != _jwtIdClaimReaderHelper.getUserIdFromToken(token))
            {
                throw new NotImplementedException();
            }
            var newKwet = new Kwet
            {
                KwetId = new Guid(),
                UserId = Guid.Parse(kwet.Id),
                UserName = kwet.UserName,
                Message = kwet.Message,
                TimeStamp = DateTime.Now,
                Likes =  new List<Likes>()
            };
            return await _repository.Create(newKwet);
        }

        public async Task<Kwet> LikeKwet(LikeModel kwet)
        {
            var likedKwet = await _repository.Get(kwet.KwetId);
            if (likedKwet.Likes == null) 
            {
                likedKwet.Likes = new List<Likes>();
            }
            likedKwet.Likes.Add(new Likes(){userId = kwet.Id, Name = kwet.UserName});
            return await _repository.Update(likedKwet);
        }

        public async Task<Kwet> RemoveLike(LikeModel kwet)
        {
            var unlikedKwet = await _repository.Get(kwet.KwetId);
            var like = unlikedKwet.Likes.SingleOrDefault(x => x.userId == kwet.Id);
            if (like == null)
            {
                throw new LikeNotFoundException();
            }
            unlikedKwet.Likes.Remove(like);
            return await _repository.Update(unlikedKwet);
               
        }
    }
}