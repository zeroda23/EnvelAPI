using MVC_Test2.Entities.DataBase;
using MVC_Test2.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Repository
{
    public interface IPreguntaRepository
    {
        public Task<dynamic> CreateAsync(Pregunta item);
        public Task<List<PreguntaDTO>> GetAllAsync();
    }
}
