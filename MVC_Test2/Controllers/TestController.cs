using Microsoft.AspNetCore.Mvc;
using MVC_Test2.Entities.Request;
using MVC_Test2.Entities.Response;
using MVC_Test2.Services;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_Test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService) => _testService = testService;
        // POST api/<TestController>
        [HttpPost]
        public async Task<ActionResult<PostTestResponse>> Post([FromBody] PostTestRequest data)
        {
            try
            {
                var cuestionario = new Entities.DTO.CuestionarioDTO();

                data.preguntas.ForEach(pregunta =>
                {
                    cuestionario.preguntas.Add(new Entities.DTO.PreguntaElegidaDTO()
                    {
                        key = pregunta.key,
                        idRespuesta = pregunta.idRespuesta
                    });
                });

                var result = await _testService.EvaluaExamen(cuestionario);

                PostTestResponse resultadoExamen = new PostTestResponse()
                {
                    aciertos = result.Aciertos,
                    preguntas = result.Preguntas
                };

                return Ok(resultadoExamen);
            }
            catch
            {
                return BadRequest("Ocurrio un error inesperado");
            }
        }
    }
}
