using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class Sucursal
    {
        public int MAESU_ID { get; set; }
        public string MAESU_NOMBRE_SUCURSAL { get; set; }
        public string MAESU_DIRECCION { get; set; }
        public int MAESU_MAE_CIUDAD { get; set; }
        public bool MAESU_HABILITAR_FESTIVOS { get; set; }
        public double MAESU_LATITUD { get; set; }
        public double MAESU_LONGITUD { get; set; }
        public object MAESU_LATITUD_RANGO { get; set; }
        public string MAESU_EMAIL { get; set; }
        public object MAESU_LONGITUD_RANGO { get; set; }
        public bool MAESU_ACTIVO { get; set; }
        public ClcarMaeCiudad CLCAR_MAE_CIUDAD { get; set; }
        public double MAESU_DISTANCE_USU { get; set; }
    }
}
