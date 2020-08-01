using MVC_Test2.Entities.DTO;
using MVC_Test2.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public class PreguntaService : IPreguntaService
    {
        private readonly IPreguntaRepository _cloudantRepository;

        public PreguntaService(IPreguntaRepository cloudantRepository)
        {
            _cloudantRepository = cloudantRepository;
        }

        public async Task<string> CreateAsync(PreguntaDTO item)
        {
            Entities.DataBase.Pregunta pregunta = new Entities.DataBase.Pregunta();

            pregunta.Descripcion = item.Descripcion;

            item.Respuestas.ForEach(element =>
            {
                pregunta.Respuestas.Add(new Entities.DataBase.Respuesta()
                {
                    Id = element.Id,
                    Descripcion = element.Descripcion,
                    EsCorrecta = element.EsCorrecta
                });
            });

            await _cloudantRepository.CreateAsync(pregunta);

            return pregunta.Descripcion;
        }

        public async Task<List<PreguntaDTO>> GetAllAsync()
        {
            var result = await _cloudantRepository.GetAllAsync();
            return result;
        }

        public async Task<List<PreguntaDTO>> GetTestAsync()
        {
            var result = await _cloudantRepository.GetAllAsync();
    
            return PreguntasAleaotiras(result);
        }

        //Este metodo se puede hacer generico para el ordenamiento de listas
        private List<PreguntaDTO> PreguntasAleaotiras(List<PreguntaDTO> listaPreguntas)
        {

            List<PreguntaDTO> listAleatoria = new List<PreguntaDTO>();

            int totalRegistrosPorMostrar = 10;
            var random = new Random();
            //El numero 2 se puede representación con una variable statica para y cargarla desde un inicio           
            for (int elemento = 0; elemento < listaPreguntas.Count; elemento++)
            {
                if (totalRegistrosPorMostrar == listAleatoria.Count) break;

                int numeroElemento = random.Next(0, (listaPreguntas.Count));

                if (!listAleatoria.Contains(listaPreguntas[numeroElemento]))
                {
                    listAleatoria.Add(listaPreguntas[numeroElemento]);
                }
                else
                    elemento--;
            }
            return listAleatoria;
        }
    } 
}
