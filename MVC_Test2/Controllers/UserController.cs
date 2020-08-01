using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Test2.Request;
using MVC_Test2.Response;
using MVC_Test2.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_Test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _cloudantService;

        public UserController(IUserService cloudantService = null)
        {
            _cloudantService = cloudantService;
        }

        // GET: api/values
        [HttpGet("GetUserByKey")]
        public async Task<ActionResult<GetUsuarioResponse>> GetByKey([FromQuery] GetUsuarioRequest data)
        {
            try
            {
                if (_cloudantService == null)
                {
                    return BadRequest("No database connection");
                }
                else
                {
                    var result = await _cloudantService.GetByKeyAsync(data.id);
                    GetUsuarioResponse response = new GetUsuarioResponse()
                    {
                        id = result.id,
                        descripcion = result.descripcion,
                        email = result.email,
                        estatus = result.estatus,
                        latitud = result.geometry.coordinates[1],
                        longitud = result.geometry.coordinates[0],
                        limiteBusqueda = result.limiteBusqueda,
                        rolesPreferentes = result.rolesPreferentes,
                        rolId = result.rolId,
                        rolNombre = result.rolNombre,
                        telefono = result.telefono,
                        ultimaConexion = result.ultimaConexion,
                        usuario = result.usuario
                    };

                    result.matches.ForEach(element =>
                    {
                        response.matches.Add(new Entities.Database.ItemMatch()
                        {
                            filtrado = element.filtrado,
                            id = element.id
                        });
                    });

                    return Ok(response);
                }
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }

        // GET: api/values
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<GetUsuarioResponse>>> GetAllUsers()
        {
            try
            {
                if (_cloudantService == null)
                {
                    return BadRequest("No database connection");
                }
                else
                {
                    var result = await _cloudantService.GetAllAsync();
                    List<GetUsuarioResponse> response = new List<GetUsuarioResponse>();

                    result.ForEach(element =>
                    {
                        var newElement = new GetUsuarioResponse()
                        {
                            id = element.id,
                            pass = element.pass,
                            descripcion = element.descripcion,
                            email = element.email,
                            estatus = element.estatus,
                            latitud = element.geometry.coordinates[1],
                            longitud = element.geometry.coordinates[0],
                            limiteBusqueda = element.limiteBusqueda,
                            rolesPreferentes = element.rolesPreferentes,
                            rolId = element.rolId,
                            rolNombre = element.rolNombre,
                            telefono = element.telefono,
                            ultimaConexion = element.ultimaConexion,
                            usuario = element.usuario
                        };

                        element.matches.ForEach(match =>
                        {
                            newElement.matches.Add(new Entities.Database.ItemMatch()
                            {
                                filtrado = match.filtrado,
                                id = match.id
                            });
                        });

                        response.Add(newElement);
                    });

                    return Ok(response);
                }
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<PostUsuarioResponse>> Post([FromBody] PostUsuarioRequest data)
        {
            try
            {
                PostUsuarioResponse result = new PostUsuarioResponse();
                if (_cloudantService != null)
                {
                    Entities.DTO.UsuarioDTO usuarioDTO = new Entities.DTO.UsuarioDTO()
                    {
                        id = data.id,
                        matches = new List<Entities.DTO.ItemMatchDTO>(),
                        usuario = data.usuario,
                        rolesPreferentes = data.rolesPreferentes,
                        descripcion = data.descripcion,
                        email = data.email,
                        estatus = data.estatus,
                        limiteBusqueda = 0.050000M,
                        pass = data.pass,
                        rolId = data.rolId,
                        rolNombre = data.rolNombre,
                        telefono = data.telefono
                    };


                    usuarioDTO.geometry.coordinates[0] = data.longitud;
                    usuarioDTO.geometry.coordinates[1] = data.latitud;

                    var response = await _cloudantService.CreateAsync(usuarioDTO);

                    result.usuario = response;
                }

                return Ok(result.usuario);
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }

        [HttpPut("UpdateUserInfo")]
        public async Task<ActionResult> PutUserInfo([Required][FromQuery] string id,[FromBody] PutUsuarioRequest data)
        {
            try
            {
                Entities.DTO.UsuarioDTO usuarioDTO = new Entities.DTO.UsuarioDTO()
                {
                    id = id,
                    descripcion = data.descripcion,
                    usuario = data.usuario,
                    rolNombre = data.rolNombre,
                    rolId = data.rolId,
                    limiteBusqueda = 0.050000M,
                    rolesPreferentes = data.rolesPreferentes
                };

                await _cloudantService.ModifyUserInformation(usuarioDTO, 1);

                return Ok();
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }               
       
    }
}
