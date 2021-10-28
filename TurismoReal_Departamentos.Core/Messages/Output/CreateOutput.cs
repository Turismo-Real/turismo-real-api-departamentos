using System;
using System.Collections.Generic;
using System.Text;

namespace TurismoReal_Departamentos.Core.Messages.Output
{
    public class CreateOutput
    {
        public CreateOutput() { }

        public CreateOutput(string message, bool saved, object departamento)
        {
            this.message = message;
            this.saved = saved;
            this.departamento = departamento;
        }

        public string message { get; set; }
        public bool saved { get; set; }
        public object departamento { get; set; }
    }
}
