using MVC_Test2.Entities;
using MVC_Test2.Entities.DataBase;
using MVC_Test2.Entities.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MVC_Test2.Repository
{
    public class PreguntaRepository: IPreguntaRepository
    {
        private static readonly string _dbName = "preguntas";
        private readonly Credenciales _cloudantCreds;
        private readonly UrlEncoder _urlEncoder;
        private readonly IHttpClientFactory _factory;

        public PreguntaRepository(Credenciales creds, UrlEncoder urlEncoder, IHttpClientFactory factory)
        {
            _cloudantCreds = creds;
            _urlEncoder = urlEncoder;
            _factory = factory;
        }

        public async Task<dynamic> CreateAsync(Pregunta item)
        {
            string jsonInString = JsonConvert.SerializeObject(item);

            return await new CloudantRepository(_factory, _dbName).Create(jsonInString);
        }

        public async Task<List<PreguntaDTO>> GetAllAsync()
        {
            var resultadoDinamico = await new CloudantRepository(_factory, _dbName).GetAll();
            List<PreguntaDTO> preguntas = new List<PreguntaDTO>();
            Encabezado pregunta = JsonConvert.DeserializeObject<Encabezado>(resultadoDinamico);
            pregunta.Rows.ForEach(element =>
            {
                PreguntaDTO preguntaJson = JsonConvert.DeserializeObject<PreguntaDTO>(element.Doc.ToString());
                preguntas.Add(new PreguntaDTO()
                {
                    Descripcion = preguntaJson.Descripcion,
                    Key = element.Key,
                    Respuestas = preguntaJson.Respuestas
                });
            });

            return preguntas;
        }
    }
}
