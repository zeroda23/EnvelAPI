using System.Collections.Generic;

namespace MVC_Test2.Entities.DTO
{
    public class CuestionarioDTO
    {
        public CuestionarioDTO() => preguntas = new List<PreguntaElegidaDTO>();
        public List<PreguntaElegidaDTO> preguntas { get; set; }
    }

    public class PreguntaElegidaDTO
    {
        public string key { get; set; }
        public string idRespuesta { get; set; }
    }
}
