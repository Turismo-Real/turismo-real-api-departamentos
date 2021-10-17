using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurismoReal_Departamentos.Core.DTOs;
using TurismoReal_Departamentos.Core.Interfaces;
using TurismoReal_Departamentos.Core.Messages.Output;

namespace TurismoReal_Departamentos.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        public readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoController(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        // GET: /api/v1/departamento
        [HttpGet]
        public async Task<List<Departamento>> GetDepartamentos()
        {
            List<Departamento> deptos = await _departamentoRepository.GetDepartamentos();
            return deptos;
        }

        // GET: /api/v1/departamento/{id}
        [HttpGet("{id}")]
        public async Task<object> GetDepartamento(int id)
        {
            Departamento depto = await _departamentoRepository.GetDepartamento(id);

            if(depto.id_departamento == 0)
            {
                return new NotFoundOutput($"No se encontró departamento con ID {id}");
            }

            return depto;
        }

        // POST: /api/v1/departamento
        [HttpPost]
        public async Task<CreateOutput> CreateDepartamento([FromBody] Departamento payload)
        {
            int result = await _departamentoRepository.CreateDepartamento(payload);

            if(result > 0)
            {
                return new CreateOutput("Departamento creado exitosamente.", result);
            }
            else
            {
                return new CreateOutput("Error al crear departamento", result);
            }
        }

        // PUT: /api/v1/departamento/{id}
        [HttpPost("{id}")]
        public async Task<object> DeleteDepartamento(int id, [FromBody] object payload)
        {
            await Task.Delay(2);
            return "";
        }

        // DELETE: /api/v1/departamento/{id}
        [HttpDelete("{id}")]
        public async Task<object> DeleteDepartamento(int id)
        {
            await Task.Delay(2);
            return "";
        }

    }
}
