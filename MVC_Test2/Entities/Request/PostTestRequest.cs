using System.Collections.Generic;

namespace MVC_Test2.Entities.Request
{
    public class PostTestRequest
    {
        public PostTestRequest()
        {
            preguntas = new List<PreguntaRequest>();
        }

        public List<PreguntaRequest> preguntas { get; set; }

        public class PreguntaRequest
        {
            public string key { get; set; }
            public string idRespuesta { get; set; }
        }
    }

    
}
