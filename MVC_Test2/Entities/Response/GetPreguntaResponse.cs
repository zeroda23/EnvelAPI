using System.Collections.Generic;

namespace MVC_Test2.Response
{
    public class GetPreguntaResponse
    {
        public string Key { get; set; }
        public string Descripcion { get; set; }
        public List<RespuestaResponse> Respuestas { get; set; }

        public GetPreguntaResponse()
        {
            Respuestas = new List<RespuestaResponse>();
        }
    }
    public class RespuestaResponse
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public bool EsCorrecta { get; set; }
    }
}
