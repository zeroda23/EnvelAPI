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
    public class PreguntaController : ControllerBase
    {        
        private readonly IPreguntaService _preguntaService;

        public PreguntaController(IPreguntaService preguntaService)
        {
            _preguntaService = preguntaService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<List<GetPreguntaResponse>>> Get()
        {
            try
            {
                if (_preguntaService == null)
                {
                    return BadRequest("No database connection");
                }
                else
                {
                    List<GetPreguntaResponse> preguntas = new List<GetPreguntaResponse>();
                    var result = await _preguntaService.GetTestAsync();

                    result.ForEach(element =>
                    {
                        GetPreguntaResponse pregunta = new GetPreguntaResponse()
                        {
                            Key = element.Key,
                            Descripcion = element.Descripcion
                        };

                        element.Respuestas.ForEach(respuesta =>
                        {
                            pregunta.Respuestas.Add(new RespuestaResponse()
                            {
                                Id = respuesta.Id,
                                Descripcion = respuesta.Descripcion,
                                EsCorrecta = respuesta.EsCorrecta
                            });
                        });

                        preguntas.Add(pregunta);
                    });

                    return Ok(preguntas);
                }
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] GetPreguntaRequest pregunta)
        {
            try
            {
                Entities.DTO.PreguntaDTO preguntaDTO = new Entities.DTO.PreguntaDTO()
                {
                    Descripcion = pregunta.Descripcion
                };

                pregunta.Respuestas.ForEach(respuesta =>
                {
                    preguntaDTO.Respuestas.Add(new Entities.DTO.RespuestaDTO()
                    {
                        Id = respuesta.Id,
                        Descripcion = respuesta.Descripcion,
                        EsCorrecta = respuesta.EsCorrecta
                    });
                });

                var response = await _preguntaService.CreateAsync(preguntaDTO);

                return Ok(response);
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }
    }
}
