using System;
using System.Collections.Generic;
using System.Text;

namespace TurismoReal_Departamentos.Core.Messages.Output
{
    public class CreateOutput
    {
        public CreateOutput() { }

        public CreateOutput(string message, object departamento)
        {
            this.message = message;
            this.departamento = departamento;
        }

        public string message { get; set; }
        public object departamento { get; set; }
    }
}
