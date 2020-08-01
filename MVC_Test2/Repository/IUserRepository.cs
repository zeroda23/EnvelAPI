using MVC_Test2.Entities.DataBase;
using MVC_Test2.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Repository
{
    public interface IUserRepository
    {
        public Task<dynamic> CreateAsync(User item);
        public Task<List<UsuarioDTO>> GetAllAsync();
        public Task<UsuarioDTO> GetByKeyAsync(string key);
        public Task UpdateAsync(User item);        
        public Task<dynamic> GetOriginalObjectByKeyAsync(string key);
    }
}
