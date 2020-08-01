using MVC_Test2.Entities.DataBase;
using MVC_Test2.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Repository
{
    public interface ITipRepository
    {
        Task<string> CreateAsync(Tip item);
        Task<List<TipDTO>> GetAllAsync();
        Task<Tip> GetTipByRequestAsync(string key);
    }
}
