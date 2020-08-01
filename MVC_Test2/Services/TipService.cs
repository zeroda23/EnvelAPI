using MVC_Test2.Entities.DTO;
using MVC_Test2.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public class TipService: ITipService
    {
        private readonly ITipRepository _cloudantRepository;

        public TipService(ITipRepository cloudantRepository)
        {
            _cloudantRepository = cloudantRepository;
        }

        public async Task<string> CreateAsync(TipDTO item)
        {
            await _cloudantRepository.CreateAsync(new Entities.DataBase.Tip()
            {
                FechaPublicacion = item.FechaPublicacion,
                Detalle = item.Detalle,
                Encabezado = item.Encabezado
            });

            return item.Encabezado;

        }

        public async Task<List<TipDTO>> GetAllAsync()
        {
            var result = await _cloudantRepository.GetAllAsync();          

            return result;
        }

        public async Task<TipDTO> GetTipByRequestAsync(string key)
        {
            var result = await _cloudantRepository.GetTipByRequestAsync(key);
            TipDTO tip = new TipDTO()
            {                
                Encabezado = result.Encabezado,
                Detalle = result.Detalle,
                FechaPublicacion = result.FechaPublicacion
            };
           
            return tip;
        }
    }
}
