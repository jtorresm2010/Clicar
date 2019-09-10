using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class Itemsdetalleinspeccion
    {
        public int IDINSP_ID { get; set; }
        public int IDINSP_DEINSP_ID { get; set; }
        public string IDINSP_DESCRIPCION { get; set; }
        public DateTime IDINSP_FECHA_CREACION { get; set; }
        public object CLCAR_DETALLES_INSPECCION { get; set; }

    }
}
