using MVC_Test2.Entities.DTO;
using System.Threading.Tasks;

namespace MVC_Test2.Services
{
    public interface ITestService
    {
        public Task<CalificacionDTO> EvaluaExamen(CuestionarioDTO cuestionario);
    }
}
