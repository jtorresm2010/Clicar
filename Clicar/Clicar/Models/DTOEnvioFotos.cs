using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class FotosInspeccion
    {
        public int FINSP_INSP_ID { get; set; }
        public object FINSP_FOTO_ID { get; set; }
        public string FINSP_NOMBRE_ARCHIVO { get; set; }
        public string FINSP_FECHA_CREACION { get; set; }
        public string FINSP_ARCHIVO_BASE64 { get; set; }
    }

    public class FotositemsInspeccion
    {
        public int INSPE_ID { get; set; }
        public string FIINS_NOMBRE_ARCHIVO { get; set; }
        public string FIINS_FECHA_CREACION { get; set; }
        public string FIINS_ARCHIVO_BASE64 { get; set; }
    }

    public class EnvioFotos
    {
        public List<FotosInspeccion> fotosInspeccion { get; set; }
        public List<FotositemsInspeccion> fotositemsInspeccion { get; set; }
    }
}
