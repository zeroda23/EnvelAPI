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
    public class LoginController : ControllerBase
    {
        private readonly IUserService _cloudantService;

        public LoginController(IUserService userService) => _cloudantService = userService;

        [HttpPut("UpdatePassword")]
        public async Task<ActionResult> PutPassword([Required][FromQuery] string id, [FromBody] PutPasswordRequest data)
        {
            try
            {
                Entities.DTO.UsuarioDTO usuarioDTO = new Entities.DTO.UsuarioDTO()
                {
                    id = id,
                    pass = data.password,
                    limiteBusqueda = 0.050000M
                };

                await _cloudantService.ModifyUserInformation(usuarioDTO, 5);

                return Ok();
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }
    }
}
