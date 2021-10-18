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
                Departamento depto = await _departamentoRepository.GetDepartamento(result);
                return new CreateOutput("Departamento creado exitosamente.", depto);
            }
            else
            {
                return new CreateOutput("Error al crear departamento", null);
            }
        }

        // PUT: /api/v1/departamento/{id}
        [HttpPut("{id}")]
        public async Task<object> UpdateDepartamento(int id, [FromBody] Departamento depto)
        {
            int updated = await _departamentoRepository.UpdateDepartamento(id, depto);

            if (updated == 0) return new { message = "Error al actualizar departamento.", updated = false };
            if (updated == -1) return new { message = $"No existe departamento con ID {id}.", updated = false };

            Departamento newDepto = await _departamentoRepository.GetDepartamento(updated);
            return new { message = "Departamento actualizado.", updated = true, departamento = newDepto };
        }

        // DELETE: /api/v1/departamento/{id}
        [HttpDelete("{id}")]
        public async Task<DeleteOutput> DeleteDepartamento(int id)
        {
            int removed = await _departamentoRepository.DeleteDepartamento(id);

            if(removed == 0) return new DeleteOutput("Error al eliminar departamento.", false);
            if(removed == -1) return new DeleteOutput($"No existe el departamento con ID {id}", false);
            return new DeleteOutput("Departamento eliminado.", true);
        }

    }
}
