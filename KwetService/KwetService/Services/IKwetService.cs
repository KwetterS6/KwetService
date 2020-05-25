using System.Collections.Generic;
using System.Threading.Tasks;
using KwetService.Models;

namespace KwetService.Services
{
    public interface IKwetService
    {
        Task<List<Kwet>> Get();

        Task<Kwet> InsertKwet(Kwet kwet);
    }
}