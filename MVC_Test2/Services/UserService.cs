using MVC_Test2.Entities.DataBase;
using MVC_Test2.Entities.DTO;
using MVC_Test2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository cloudantRepository;

        public UserService(IUserRepository _cloudantRepository)
        {
            cloudantRepository = _cloudantRepository;
        }

        public async Task<string> CreateAsync(UsuarioDTO item)
        {
            await cloudantRepository.CreateAsync(new User()
            {
                id = item.id,
                limiteBusqueda = 0.050000M,
                matches = new List<Entities.Database.ItemMatch>(),
                geometry = item.geometry,
                descripcion = item.descripcion,
                email = item.email,
                estatus = item.estatus,
                fechaCreacion = DateTime.Now.ToShortDateString(),
                pass = item.pass,
                rolesPreferentes = item.rolesPreferentes,
                rolId = item.rolId,
                rolNombre = item.rolNombre,
                telefono = item.telefono,
                usuario = item.usuario
            });

            return item.usuario;
        }

        public async Task<List<UsuarioDTO>> GetAllAsync()
        {
            return await cloudantRepository.GetAllAsync();
        }

        public async Task<UsuarioDTO> GetByKeyAsync(string id)
        {
            List<UsuarioDTO> result = await cloudantRepository.GetAllAsync();

            UsuarioDTO usuarioDTO = result.Where(user => user.id == id).Select(element => new UsuarioDTO()
            {
                matches = element.matches,
                id = element.id,
                descripcion = element.descripcion,
                email = element.email,
                estatus = element.estatus,
                key = element.key,
                limiteBusqueda = element.limiteBusqueda,
                rolesPreferentes = element.rolesPreferentes,
                rolId = element.rolId,
                rolNombre = element.rolNombre,
                telefono = element.telefono,
                usuario = element.usuario,
                geometry = element.geometry
            }).FirstOrDefault();

            return usuarioDTO;
        }

        public async Task<List<UsuarioDTO>> GetChats(string id, decimal longitud, decimal latitud)
        {
            decimal[] longitudLimite;
            decimal[] latitudLimite;

            decimal medicionEstandar = 0.070000M;

            //UsuarioDTO usuario = await cloudantRepository.GetByKeyAsync(id);
            UsuarioDTO usuario = await GetUserById(id);

            if (!string.IsNullOrEmpty(usuario.usuario))
            {
                //medicionEstandar = usuario.limiteBusqueda;
                longitudLimite = CalculaLimiteBusqueda(medicionEstandar, longitud);
                latitudLimite = CalculaLimiteBusqueda(medicionEstandar, latitud);

                List<UsuarioDTO> users = await cloudantRepository.GetAllAsync();

                users = ObtenerListaDeUsuariosPorLimites(users, longitudLimite, latitudLimite, id, usuario.rolesPreferentes);

                return ListaAleaotira(users);
            }

            return new List<UsuarioDTO>();
        } 

        private async Task<UsuarioDTO> GetUserById(string id)
        {
            List<UsuarioDTO> result = await cloudantRepository.GetAllAsync();

            UsuarioDTO usuarioDTO = result.Where(user => user.id == id).Select(element => new UsuarioDTO()
            {
                fechaCreacion = element.fechaCreacion,
                geometry = element.geometry,
                _rev = element._rev,
                _id = element._id,
                id = element.id,
                matches = element.matches,
                pass = element.pass,
                ultimaConexion = element.ultimaConexion,
                descripcion = element.descripcion,
                email = element.email,
                estatus = element.estatus,
                key = element.key,
                limiteBusqueda = element.limiteBusqueda,
                rolesPreferentes = element.rolesPreferentes,
                rolId = element.rolId,
                rolNombre = element.rolNombre,
                telefono = element.telefono,
                usuario = element.usuario
            }).FirstOrDefault();

            return usuarioDTO;
        }

        public async Task<UsuarioDTO> GetUserInformation(string email, string id)
        {
            List<UsuarioDTO> result = await cloudantRepository.GetAllAsync();

            UsuarioDTO usuarioDTO = result.Where(user => user.email == email && user.id == id).Select(element => new UsuarioDTO()
            {
                descripcion = element.descripcion,
                email = element.email,
                estatus = element.estatus,
                key = element.key,
                limiteBusqueda = element.limiteBusqueda,
                rolesPreferentes = element.rolesPreferentes,
                rolId = element.rolId,
                rolNombre = element.rolNombre,
                telefono = element.telefono,
                usuario = element.usuario
            }).FirstOrDefault();

            return usuarioDTO;
        }

        public async Task ModifyUserInformation(UsuarioDTO usuario, int tipoModificacion)
        {
            var user = await GetUserById(usuario.id);

            switch (tipoModificacion)
            {
                //ModificaConfiguraciones
                case 1:
                    user = ModificaConfiguraciones(usuario, user);
                    break;
                //ModificaCoordinates
                case 2:
                    user = ModificaCoordenadas(usuario, user);
                    break;
                //ModificaMatches
                case 3:
                    user = ModificaMatches(usuario, user);
                    break;
                //ModificaUltimaConexion
                case 4:
                    user = ModificaUltimaConexion(usuario, user);
                    break;
                //ModificaPassword
                case 5:
                    user = ModificaPassword(usuario, user);
                    break;
            }

            var userElement = new User()
            {
                _id = user._id,
                _rev = user._rev,
                id = user.id,
                fechaCreacion = user.fechaCreacion,
                geometry = user.geometry,
                pass = user.pass,
                ultimaConexion = user.ultimaConexion,
                descripcion = user.descripcion,
                email = user.email,
                usuario = user.usuario,
                estatus = user.estatus,
                limiteBusqueda = user.limiteBusqueda,
                rolesPreferentes = user.rolesPreferentes,
                rolId = user.rolId,
                rolNombre = user.rolNombre,
                telefono = user.telefono
            };

            user.matches.ForEach(match =>
            {
                userElement.matches.Add(new Entities.Database.ItemMatch()
                {
                    id = match.id,
                    filtrado = match.filtrado
                });
            });

            await cloudantRepository.UpdateAsync(userElement);
        }

        private UsuarioDTO ModificaCoordenadas(UsuarioDTO modificaciones, UsuarioDTO datosOriginales)
        {
            datosOriginales.geometry = modificaciones.geometry;

            return datosOriginales;
        }

        private UsuarioDTO ModificaMatches(UsuarioDTO modificaciones, UsuarioDTO datosOriginales)
        {
            datosOriginales.matches = modificaciones.matches;

            return datosOriginales;
        }

        private UsuarioDTO ModificaUltimaConexion(UsuarioDTO modificaciones, UsuarioDTO datosOriginales)
        {
            datosOriginales.ultimaConexion = modificaciones.ultimaConexion;

            return datosOriginales;
        }

        private UsuarioDTO ModificaPassword(UsuarioDTO modificaciones, UsuarioDTO datosOriginales)
        {
            datosOriginales.pass = modificaciones.pass;

            return datosOriginales;
        }

        private UsuarioDTO ModificaConfiguraciones(UsuarioDTO modificaciones, UsuarioDTO datosOriginales)
        {
            datosOriginales.descripcion = modificaciones.descripcion;
            datosOriginales.usuario = modificaciones.usuario;
            datosOriginales.rolId = modificaciones.rolId;
            datosOriginales.rolNombre = modificaciones.rolNombre;
            datosOriginales.rolesPreferentes = modificaciones.rolesPreferentes;

            return datosOriginales;
        }


        //Este metodo se puede hacer generico para el ordenamiento de listas
        private List<UsuarioDTO> ListaAleaotira(List<UsuarioDTO> listaPreguntas)
        {

            List<UsuarioDTO> listAleatoria = new List<UsuarioDTO>();
            int totalRegistrosPorMostrar = 5;
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
        private decimal[] CalculaLimiteBusqueda(decimal medicionEstandar, decimal medicionInicial)
        {
            decimal[] result = new decimal[2];

            result[0] = (medicionInicial - medicionEstandar);
            result[1] = (medicionInicial + medicionEstandar);

            return result;
        }
        private List<UsuarioDTO> ObtenerListaDeUsuariosPorLimites(List<UsuarioDTO> usuarios, decimal[] longitud, decimal[] latitud, string id, List<string> rolesPreferentes)
        {
           return usuarios.Where(userElement =>
               (userElement.geometry.coordinates[0] > longitud[0] && userElement.geometry.coordinates[0] < longitud[1])
               &&
               (userElement.geometry.coordinates[1] > latitud[0] && userElement.geometry.coordinates[0] < latitud[1])
               && userElement.id != id
               && rolesPreferentes.Contains(userElement.rolId)).ToList();
        }
    }
}
