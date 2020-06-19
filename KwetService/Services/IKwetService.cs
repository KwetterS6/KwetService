using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.Models;

namespace KwetService.Services
{
    public interface IKwetService
    {
        /// <summary>
        /// Returns a list of all kwets
        /// </summary>
        /// <returns>List<Kwet></returns>
        Task<List<Kwet>> Get();
        /// <summary>
        /// Get a list of all kwets placed by a specific users
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>List<Kwet></returns>
        Task<List<Kwet>> GetByUserId(Guid Id);
        /// <summary>
        /// Inserts a single kwet 
        /// </summary>
        /// <param name="kwet"></param>
        /// <param name="token"></param>
        /// <returns>Kwet</returns>
        Task<Kwet> InsertKwet(NewKwetModel kwet, string token);
        /// <summary>
        /// Likes a kwet using the user data inside of the likemodel
        /// </summary>
        /// <param name="kwet"></param>
        /// <returns>kwet</returns>
        Task<Kwet> LikeKwet(LikeModel kwet);
        /// <summary>
        /// removes the like by the user from a kwet
        /// </summary>
        /// <param name="kwet"></param>
        /// <returns>kwet</returns>
        Task<Kwet> RemoveLike(LikeModel kwet);
    }
}