using System.Collections.Generic;
using System.Threading.Tasks;

namespace TurismoReal_Departamentos.Core.Interfaces
{
    public interface IDepartamentoRepository
    {
        // GET ALL
        Task<List<object>> GetDepartamentos();

        // GET BY ID
        Task<object> GetDepartamento(int id);

        // CREATE
        Task<object> CreateDepartamento(object depto);

        // UPDATE
        Task<object> UpdateDepartamento(int id, object depto);

        // DELETE
        Task<object> DeleteDepartamento(int id);
    }
}
