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
    public class TipController : ControllerBase
    {
        private readonly ITipService _tipService;

        public TipController(ITipService tipService)
        {
            _tipService = tipService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<List<GetTipResponse>>> Get()
        {
            try
            {
                if (_tipService == null)
                {
                    return BadRequest("No database connection");
                }
                else
                {
                    GetTipsResponse tips = new GetTipsResponse();
                    var result = await _tipService.GetAllAsync();

                    result.ForEach(element =>
                    {
                        tips.Tips.Add(new GetTipResponse()
                        {
                            Key = element.Key,
                            FechaPublicacion = element.FechaPublicacion,
                            Encabezado = element.Encabezado,
                            Detalle = element.Detalle
                        });
                    });

                    return Ok(tips.Tips);
                }
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }

        [HttpGet("GetTipByID")]
        public async Task<ActionResult<GetTipDetailResponse>> Get([FromQuery] GetTipRequest data)
        {
            try
            {
                if (_tipService == null)
                {
                    return BadRequest("No database connection");
                }
                else
                {

                    var result = await _tipService.GetTipByRequestAsync(data.Key);
                    GetTipDetailResponse tip = new GetTipDetailResponse()
                    {
                        FechaPublicacion = result.FechaPublicacion,
                        Detalle = result.Detalle,
                        Encabezado = result.Encabezado
                    };

                    return Ok(tip);
                }
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] PostTipRequest tip)
        {

            try
            {
                var response = await _tipService.CreateAsync(new Entities.DTO.TipDTO()
                {
                    Detalle = tip.Detalle,
                    Encabezado = tip.Encabezado,
                    FechaPublicacion = tip.FechaPublicacion
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
