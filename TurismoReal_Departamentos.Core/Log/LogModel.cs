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
        public string method = string.Empty;
        public string endpoint = string.Empty;
        public DateTime inicioSolicitud;
        public DateTime finSolicitud;
        public string tiempoSolicitud;
        public object payload = null;
        public object response = null;

        public void InitLog(string service, string method, string path, DateTime startRequest)
        {
            servicio = service;
            this.method = method;
            endpoint = path;
            inicioSolicitud = startRequest;
        }

        public void EndLog(DateTime endRequest, int status, object response)
        {
            finSolicitud = endRequest;
            tiempoSolicitud = (finSolicitud - inicioSolicitud).TotalMilliseconds + "ms";
            statusCode = status;
            this.response = response;
        }

        public string parseJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this) + "\n";
        }
    }
}
