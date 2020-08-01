using System.Collections.Generic;

namespace MVC_Test2.Request
{
    public class GetPreguntaRequest
    {
        public string Descripcion { get; set; }
        public List<RespuestaRequest> Respuestas { get; set; }

        public GetPreguntaRequest()
        {
            Respuestas = new List<RespuestaRequest>();
        }
    }
    public class RespuestaRequest
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public bool EsCorrecta { get; set; }
    }
}
