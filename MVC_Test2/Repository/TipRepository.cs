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
    public class TipRepository: ITipRepository
    {
        private static readonly string _dbName = "tips";
        private readonly Credenciales _cloudantCreds;
        private readonly UrlEncoder _urlEncoder;
        private readonly IHttpClientFactory _factory;

        public TipRepository(Credenciales creds, UrlEncoder urlEncoder, IHttpClientFactory factory)
        {
            _cloudantCreds = creds;
            _urlEncoder = urlEncoder;
            _factory = factory;
        }

        public async Task<string> CreateAsync(Tip item)
        {
            string jsonInString = JsonConvert.SerializeObject(item);
            var result = await new CloudantRepository(_factory, _dbName).Create(jsonInString);
            return  result;
        }

        public async Task<List<TipDTO>> GetAllAsync()
        {
            var resultadoDinamico = await new CloudantRepository(_factory, _dbName).GetAll();
            List<TipDTO> tips = new List<TipDTO>();
            Encabezado tip = JsonConvert.DeserializeObject<Encabezado>(resultadoDinamico);
            tip.Rows.ForEach(element =>
            {
                TipDTO tipJson = JsonConvert.DeserializeObject<TipDTO>(element.Doc.ToString());
                tips.Add(new TipDTO()
                {
                    Key = element.Key,
                    Detalle = tipJson.Detalle,
                    Encabezado = tipJson.Encabezado,
                    FechaPublicacion = tipJson.FechaPublicacion
                });
            });

            return tips;
        }

        public async Task<Tip> GetTipByRequestAsync(string key)
        {
            var resultadoDinamico = await new CloudantRepository(_factory, _dbName).GetByKey(key);
            Tip tip = JsonConvert.DeserializeObject<Tip>(resultadoDinamico);          
            return tip;
        }
    }
}
