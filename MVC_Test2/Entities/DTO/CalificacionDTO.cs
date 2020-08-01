namespace MVC_Test2.Entities.DTO
{
    public class CalificacionDTO
    {
        public CalificacionDTO(int acietos, int preguntas)
        {
            Aciertos = acietos;
            Preguntas = preguntas;
        }

        public int Aciertos { get; set; }
        public int Preguntas { get; set; }
    }
}
