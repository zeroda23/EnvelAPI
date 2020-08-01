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
    public class LocationController : ControllerBase
    {
        private readonly IUserService _cloudantService;

        public LocationController(IUserService cloudantService = null)
        {
            _cloudantService = cloudantService;
        }

        [HttpPut("UpdateCoordenadas")]
        public async Task<ActionResult> PutCoordenadas([Required][FromQuery] string id, [FromBody] PutCoordenadasRequest data)
        {
            try
            {
                Entities.DTO.UsuarioDTO usuarioDTO = new Entities.DTO.UsuarioDTO()
                {
                    id = id,
                    limiteBusqueda = 0.050000M
                };

                usuarioDTO.geometry.coordinates[0] = data.longitud;
                usuarioDTO.geometry.coordinates[1] = data.latitud;

                await _cloudantService.ModifyUserInformation(usuarioDTO, 2);

                return Ok();
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }
    }
}
