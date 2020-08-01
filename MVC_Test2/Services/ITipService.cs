using MVC_Test2.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public interface ITipService
    {
        public Task<string> CreateAsync(TipDTO item);
        public Task<List<TipDTO>> GetAllAsync();
        public Task<TipDTO> GetTipByRequestAsync(string key);
    }
}
