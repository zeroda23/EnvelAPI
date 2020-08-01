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
    public class UserRepository: IUserRepository
    {
        private static readonly string _dbName = "users";
        private readonly Credenciales _cloudantCreds;
        private readonly UrlEncoder _urlEncoder;
        private readonly IHttpClientFactory _factory;

        public UserRepository(Credenciales creds, UrlEncoder urlEncoder, IHttpClientFactory factory)
        {
            _cloudantCreds = creds;
            _urlEncoder = urlEncoder;
            _factory = factory;
        }

        public async Task<dynamic> CreateAsync(User item)
        {
            string jsonInString = JsonConvert.SerializeObject(item);

            return await new CloudantRepository(_factory, _dbName).Create(jsonInString);
        }
        public async Task UpdateAsync(User item)
        {
            string jsoinInString = JsonConvert.SerializeObject(item, Formatting.None);

            

            await new CloudantRepository(_factory, _dbName).Update(jsoinInString);
        }

        public async Task<List<UsuarioDTO>> GetAllAsync()
        {
   
                var resultadoDinamico = await new CloudantRepository(_factory, _dbName).GetAll();
                List<UsuarioDTO> users = new List<UsuarioDTO>();
                Encabezado user = JsonConvert.DeserializeObject<Encabezado>(resultadoDinamico);
                user.Rows.ForEach(element =>
                {
                    if (!element.Doc.ToString().Contains("_design/geometry"))
                    {
                        UsuarioDTO userJson = JsonConvert.DeserializeObject<UsuarioDTO>(element.Doc.ToString());
                        users.Add(new UsuarioDTO()
                        {
                            _id = userJson._id,
                            _rev   = userJson._rev,
                            id = userJson.id,
                            geometry = userJson.geometry,
                            key = element.Key,                                                        
                            descripcion = userJson.descripcion,
                            email = userJson.email,
                            estatus = userJson.estatus,
                            fechaCreacion = userJson.fechaCreacion,
                            pass = userJson.pass,
                            telefono = userJson.telefono,
                            ultimaConexion = userJson.ultimaConexion,
                            usuario = userJson.usuario,
                            rolId = userJson.rolId,
                            rolNombre = userJson.rolNombre,
                            rolesPreferentes = userJson.rolesPreferentes,
                            limiteBusqueda = userJson.limiteBusqueda,
                            matches = userJson.matches
                        });
                    }
                });

                return users;      
        }

        public async Task<UsuarioDTO> GetByKeyAsync(string key)
        {
            var resultadoDinamico = await new CloudantRepository(_factory, _dbName).GetByKey(key);

            UsuarioDTO user = JsonConvert.DeserializeObject<UsuarioDTO>(resultadoDinamico);
            user.key = resultadoDinamico.ToString().Split("_id")[1].Split(",")[0].Split(":")[1].ToString();
            return user;
        }

        public async Task<dynamic> GetOriginalObjectByKeyAsync(string key)
        {
            var resultadoDinamico = await new CloudantRepository(_factory, _dbName).GetByKey(key);

            UsuarioDTO user = JsonConvert.DeserializeObject<UsuarioDTO>(resultadoDinamico);
            user.key = resultadoDinamico.ToString().Split("_id")[1].Split(",")[0].Split(":")[1].ToString();
            return resultadoDinamico;
        }
    }
}
