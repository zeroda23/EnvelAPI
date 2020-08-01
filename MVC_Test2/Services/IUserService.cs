using MVC_Test2.Entities.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public interface IUserService
    {
        Task<string> CreateAsync(UsuarioDTO item);
        Task<List<UsuarioDTO>> GetAllAsync();
        Task<List<UsuarioDTO>> GetChats(string id, decimal longitud, decimal latitud);
        Task<UsuarioDTO> GetByKeyAsync(string id);
        Task<UsuarioDTO> GetUserInformation(string email, string id);
        Task ModifyUserInformation(UsuarioDTO usuario, int tipoModificacion);
    }
}
