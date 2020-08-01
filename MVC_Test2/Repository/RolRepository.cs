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
    public class RolRepository: IRolRepository
    {
        private static readonly string _dbName = "roles";
        private readonly Credenciales _cloudantCreds;
        private readonly UrlEncoder _urlEncoder;
        private readonly IHttpClientFactory _factory;

        public RolRepository(Credenciales creds, UrlEncoder urlEncoder, IHttpClientFactory factory)
        {
            _cloudantCreds = creds;
            _urlEncoder = urlEncoder;
            _factory = factory;
        }

        public async Task<string> CreateAsync(Rol item)
        {
            string jsonInString = JsonConvert.SerializeObject(item);

            return await new CloudantRepository(_factory, _dbName).Create(jsonInString);
        }

        public async Task<List<RolDTO>> GetAllAsync()
        {
            var resultadoDinamico = await new CloudantRepository(_factory, _dbName).GetAll();
            List<RolDTO> roles = new List<RolDTO>();
            Encabezado rol = JsonConvert.DeserializeObject<Encabezado>(resultadoDinamico);
            rol.Rows.ForEach(element =>
            {
                RolDTO rolJson = JsonConvert.DeserializeObject<RolDTO>(element.Doc.ToString());
                roles.Add(new RolDTO()
                {
                    Key = element.Key,
                    Descripcion = rolJson.Descripcion,
                    Estatus = rolJson.Estatus,
                    Nombre = rolJson.Nombre
                });
            });
            return roles;
        }
    }
}
