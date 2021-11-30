using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurismoReal_Departamentos.Core.DTOs;
using TurismoReal_Departamentos.Core.Interfaces;
using TurismoReal_Departamentos.Core.Log;
using TurismoReal_Departamentos.Core.Messages.Output;

namespace TurismoReal_Departamentos.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        public readonly IDepartamentoRepository _departamentoRepository;
        private readonly string serviceName = "turismo_real_departamentos";
        private LogModel log;

        public DepartamentoController(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        // GET: /api/v1/departamento
        [HttpGet]
        public async Task<List<Departamento>> GetDepartamentos()
        {
            log = new LogModel();
            log.InitLog(serviceName, "GET", "/api/v1/departamento", DateTime.Now);

            List<Departamento> deptos = await _departamentoRepository.GetDepartamentos();

            // LOG
            log.EndLog(DateTime.Now, 200, "Lista departamentos");
            Console.WriteLine(log.parseJson());
            // LOG
            return deptos;
        }

        // GET: /api/v1/departamento/{id}
        [HttpGet("{id}")]
        public async Task<object> GetDepartamento(int id)
        {
            log = new LogModel();
            log.InitLog(serviceName, "GET", "/api/v1/departamento/{id}", DateTime.Now);

            Departamento depto = await _departamentoRepository.GetDepartamento(id);

            if(depto.id_departamento == 0)
            {
                NotFoundOutput response = new NotFoundOutput($"No se encontró departamento con ID {id}");
                // LOG
                log.EndLog(DateTime.Now, 200, response);
                Console.WriteLine(log.parseJson());
                // LOG
                return response;
            }

            // LOG
            log.EndLog(DateTime.Now, 200, depto);
            Console.WriteLine(log.parseJson());
            // LOG
            return depto;
        }

        // POST: /api/v1/departamento
        [HttpPost]
        public async Task<CreateOutput> CreateDepartamento([FromBody] Departamento payload)
        {
            log = new LogModel();
            log.InitLog(serviceName, "POST", "/api/v1/departamento", DateTime.Now);

            int result = await _departamentoRepository.CreateDepartamento(payload);

            if(result > 0)
            {
                Departamento depto = await _departamentoRepository.GetDepartamento(result);
                depto.fechasReservadas = new List<FechaReservada>();
                CreateOutput response = new CreateOutput("Departamento creado exitosamente.", true, depto);
                // LOG
                log.EndLog(DateTime.Now, 200, response);
                Console.WriteLine(log.parseJson());
                // LOG
                return response;
            }
            else
            {
                CreateOutput response = new CreateOutput("Error al crear departamento", false, null);
                // LOG
                log.EndLog(DateTime.Now, 200, response);
                Console.WriteLine(log.parseJson());
                // LOG
                return response;
            }
        }

        // PUT: /api/v1/departamento/{id}
        [HttpPut("{id}")]
        public async Task<object> UpdateDepartamento(int id, [FromBody] Departamento depto)
        {
            log = new LogModel();
            log.InitLog(serviceName, "PUT", "/api/v1/departamento/{id}", DateTime.Now);

            int updated = await _departamentoRepository.UpdateDepartamento(id, depto);

            if (updated == 0) return new { message = "Error al actualizar departamento.", updated = false };
            if (updated == -1) return new { message = $"No existe departamento con ID {id}.", updated = false };

            Departamento newDepto = await _departamentoRepository.GetDepartamento(id);
            var response = new { message = "Departamento actualizado.", updated = true, departamento = newDepto };

            // LOG
            log.EndLog(DateTime.Now, 200, response);
            Console.WriteLine(log.parseJson());
            // LOG
            return response;
        }

        // DELETE: /api/v1/departamento/{id}
        [HttpDelete("{id}")]
        public async Task<DeleteOutput> DeleteDepartamento(int id)
        {
            log = new LogModel();
            log.InitLog(serviceName, "DELETE", "/api/v1/departamento/{id}", DateTime.Now);

            int removed = await _departamentoRepository.DeleteDepartamento(id);

            if(removed == 0) return new DeleteOutput("Error al eliminar departamento.", false);
            if(removed == -1) return new DeleteOutput($"No existe el departamento con ID {id}", false);

            DeleteOutput response = new DeleteOutput("Departamento eliminado.", true);
            // LOG
            log.EndLog(DateTime.Now, 200, response);
            Console.WriteLine(log.parseJson());
            // LOG
            return response;
        }

    }
}
