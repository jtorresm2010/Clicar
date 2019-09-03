using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class ClcarMaeCiudad
    {
        public int id { get; set; }
        public string NOMBRE_CIUDAD { get; set; }
        public int PAIS_ID { get; set; }
        public bool ACTIVO { get; set; }
        public object CLCAR_MAE_PAIS { get; set; }
    }
}
