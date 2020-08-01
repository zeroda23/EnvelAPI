using MVC_Test2.Entities.DataBase;
using MVC_Test2.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Repository
{
    public interface IRolRepository
    {
        public Task<string> CreateAsync(Rol item);
        public Task<List<RolDTO>> GetAllAsync();
    }
}
