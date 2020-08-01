using System.Collections.Generic;

namespace MVC_Test2.Entities.DataBase
{
    public class Pregunta
    {
        public string Descripcion { get; set; }
        public List<Respuesta> Respuestas { get; set; }

        public Pregunta()
        {
            Respuestas = new List<Respuesta>();
        }
    }
}
