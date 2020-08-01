using MVC_Test2.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public interface IRolService
    {
        Task<string> CreateAsync(RolDTO item);
        Task<List<RolDTO>> GetAllAsync();
    }
}
