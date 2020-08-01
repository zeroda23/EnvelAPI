using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Test2.Entities.Request;
using MVC_Test2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_Test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConexionController : ControllerBase
    {
        private readonly IUserService _cloudantService;

        public ConexionController(IUserService cloudantService = null)
        {
            _cloudantService = cloudantService;
        }

        [HttpPut("UpdateUltimaConexion")]
        public async Task<ActionResult> PutUltimaConexion([Required][FromQuery] string id, [FromBody] PutUltimaConexionRequest data)
        {
            try
            {
                Entities.DTO.UsuarioDTO usuarioDTO = new Entities.DTO.UsuarioDTO()
                {
                    id = id,
                    ultimaConexion = data.ultimaConexion,
                    limiteBusqueda = 0.050000M
                };

                await _cloudantService.ModifyUserInformation(usuarioDTO, 4);

                return Ok();
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }
    }
}
