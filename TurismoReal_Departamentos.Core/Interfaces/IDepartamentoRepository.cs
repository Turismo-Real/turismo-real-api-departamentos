﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TurismoReal_Departamentos.Core.DTOs;

namespace TurismoReal_Departamentos.Core.Interfaces
{
    public interface IDepartamentoRepository
    {
        // GET ALL
        Task<List<Departamento>> GetDepartamentos();

        // GET BY ID
        Task<object> GetDepartamento(int id);

        // CREATE
        Task<int> CreateDepartamento(Departamento depto);

        // UPDATE
        Task<object> UpdateDepartamento(int id, object depto);

        // DELETE
        Task<object> DeleteDepartamento(int id);
    }
}
