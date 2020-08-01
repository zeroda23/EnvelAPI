using System.Collections.Generic;
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
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<List<GetRolResponse>>> Get()
        {
            try
            {
                if (_rolService == null)
                {
                    return BadRequest("No database connection");
                }
                else
                {
                    List<GetRolResponse> roles = new List<GetRolResponse>();
                    var result = await _rolService.GetAllAsync();

                    result.ForEach(element =>
                    {
                        roles.Add(new GetRolResponse()
                        {
                            Key = element.Key,
                            Nombre = element.Nombre,
                            Descripcion = element.Descripcion
                        });
                    });

                    return Ok(roles);
                }
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<PostRolResponse>> Post([FromBody] PostRolRequest rol)
        {
            try
            {
                var response = await _rolService.CreateAsync(new Entities.DTO.RolDTO()
                {
                    Nombre = rol.Nombre,
                    Descripcion = rol.Descripcion,
                    Estatus = rol.Estatus
                });

                return Ok(response);
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }
    }
}
