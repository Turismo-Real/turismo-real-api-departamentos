using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurismoReal_Departamentos.Core.Interfaces;

namespace TurismoReal_Departamentos.Infra.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        public Task<object> CreateDepartamento(object depto)
        {
            throw new NotImplementedException();
        }

        public Task<object> DeleteDepartamento(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetDepartamento(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<object>> GetDepartamentos()
        {
            throw new NotImplementedException();
        }

        public Task<object> UpdateDepartamento(int id, object depto)
        {
            throw new NotImplementedException();
        }
    }
}
