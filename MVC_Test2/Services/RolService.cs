using MVC_Test2.Entities.DataBase;
using MVC_Test2.Entities.DTO;
using MVC_Test2.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _cloudantRepository;

        public RolService(IRolRepository cloudantRepository)
        {
            _cloudantRepository = cloudantRepository;
        }

        public async Task<string> CreateAsync(RolDTO item)
        {
            var result = await _cloudantRepository.CreateAsync(new Rol()
            {
                Nombre = item.Nombre,
                Descripcion = item.Descripcion,
                Estatus = item.Estatus
            });

            return item.Nombre;
        }

        public async Task<List<RolDTO>> GetAllAsync()
        {
            var result = await _cloudantRepository.GetAllAsync();
            return result;
        }
    }
}
