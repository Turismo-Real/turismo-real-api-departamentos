﻿using System.Collections.Generic;

namespace TurismoReal_Departamentos.Core.DTOs
{
    public class Departamento
    {
        public int id_departamento { get; set; }
        public string  rol { get; set; }
        public int dormitorios { get; set; }
        public int banios { get; set; }
        public string descripcion { get; set; }
        public int superficie { get; set; }
        public double valorDiario { get; set; }
        public string tipo { get; set; }
        public string estado { get; set; }
        public Direccion direccion { get; set; }
        public List<string> instalaciones { get; set; }
        public List<FechaReservada> fechasReservadas { get; set; }
    }
}
