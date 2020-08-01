using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Test2.Entities.Request;
using MVC_Test2.Entities.Response;
using MVC_Test2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_Test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUserService _cloudantService;


        public ChatController(IUserService cloudantService = null)
        {
            _cloudantService = cloudantService;
        }

        // GET: api/<ChatController>
        [HttpGet]
        public async Task<ActionResult<List<GetChatResponse>>> Get([FromQuery] GetChatRequest data)
        {
            try
            {
                if (_cloudantService == null)
                {
                    return BadRequest("No database connection");
                }
                else
                {
                    var result = await _cloudantService.GetChats(data.id, data.longitud, data.latitud);
                    List<GetChatResponse> chats = new List<GetChatResponse>();

                    result.ForEach(element =>
                    {
                        chats.Add(new GetChatResponse()
                        {
                            id = element.id,
                            usuario = element.usuario
                        });
                    });

                    if (chats.Any())
                        return Ok(chats.FirstOrDefault());
                    else
                        return Ok(new List<GetChatResponse>());
                }
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }

        [HttpPut("UpdateMatches")]
        public async Task<ActionResult> PutMatches([Required][FromQuery] string id, [FromBody] PutMatchesRequest data)
        {
            try
            {
                Entities.DTO.UsuarioDTO usuarioDTO = new Entities.DTO.UsuarioDTO()
                {
                    id = id
                };

                data.matches.ForEach(element =>
                {
                    usuarioDTO.matches.Add(new Entities.DTO.ItemMatchDTO()
                    {
                        id = element.id,
                        filtrado = element.filtrado
                    });
                });

                await _cloudantService.ModifyUserInformation(usuarioDTO, 3);

                return Ok();
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }
    }
}
