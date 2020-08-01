using MVC_Test2.Entities.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public class TestService : ITestService
    {
        private readonly IPreguntaService _cloudantService;

        public TestService(IPreguntaService cloudantService) => _cloudantService = cloudantService;

        public async Task<CalificacionDTO> EvaluaExamen(CuestionarioDTO cuestionario)
        {
            var preguntas = await _cloudantService.GetAllAsync();

            int calificacion = ExaminarPreguntas(preguntas, cuestionario);

            return new CalificacionDTO(calificacion, preguntas.Count);
        }

        private int ExaminarPreguntas(List<PreguntaDTO> preguntas, CuestionarioDTO test)
        {
            int aciertos = 0;
            test.preguntas.ForEach(pregunta =>
            {
                PreguntaDTO preguntaSeleccionada = preguntas.Where(element => element.Key == pregunta.key).FirstOrDefault();
                RespuestaDTO respuestaSeleccionada = preguntaSeleccionada.Respuestas.Where(element => element.Id == pregunta.idRespuesta).FirstOrDefault();

                if(respuestaSeleccionada != null)
                {
                    if (respuestaSeleccionada.EsCorrecta)
                        aciertos++;
                }
            });

            return aciertos;
        }
    }
}
