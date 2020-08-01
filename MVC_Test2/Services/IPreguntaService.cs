using MVC_Test2.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public interface IPreguntaService
    {
        Task<string> CreateAsync(PreguntaDTO item);
        Task<List<PreguntaDTO>> GetTestAsync();
        Task<List<PreguntaDTO>> GetAllAsync();
    }
}
