using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurismoReal_Departamentos.Core.Interfaces;

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


        // GET: /api/v1/departamento/{id}


        // POST: /api/v1/departamento


        // PUT: /api/v1/departamento/{id}



        // DELETE: /api/v1/departamento/{id}



    }
}
