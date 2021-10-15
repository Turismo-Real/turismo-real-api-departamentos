using System;
using System.Collections.Generic;
using System.Text;

namespace TurismoReal_Departamentos.Core.Log
{
    public class LogModel
    {
        public string level = "INFO";
        public int statusCode = 0;
        public string servicio = string.Empty;
        public DateTime inicioSolicitud;
        public DateTime finSolicitud;
        public string tiempoSolicitud;
        public object payload = null;
        public object response = null;

        public string parseJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this) + "\n";
        }
    }
}
