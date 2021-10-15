using System;
using System.Collections.Generic;
using System.Text;

namespace TurismoReal_Departamentos.Core.Messages.Output
{
    public class CreateOutput
    {
        public CreateOutput() { }

        public CreateOutput(string message, int id_departamento)
        {
            this.message = message;
            this.id_departamento = id_departamento;
        }

        public string message { get; set; }
        public int id_departamento { get; set; }
    }
}
