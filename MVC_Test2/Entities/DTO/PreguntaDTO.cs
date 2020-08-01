using System.Collections.Generic;

namespace MVC_Test2.Entities.DTO
{
    public class PreguntaDTO
    {
        public string Key { get; set; }
        public string Descripcion { get; set; }
        public List<RespuestaDTO> Respuestas { get; set; }

        public PreguntaDTO()
        {
            Respuestas = new List<RespuestaDTO>();
        }
    }
}
