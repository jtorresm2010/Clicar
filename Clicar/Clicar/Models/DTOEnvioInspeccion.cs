using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{

    public class Encabezado
    {
        public object INSP_ID { get; set; }
        public int INSP_SOINS_ID { get; set; }
        public int INSP_USU_ID { get; set; }
        public DateTime INPS_FECHA_INICIO { get; set; }
        public DateTime INSP_FECHA_FIN { get; set; }
        public string INSP_COMENTARIOS { get; set; }
        public int INSP_NRO_ESTRELLAS { get; set; }
        public int INSP_ESTDO_ID { get; set; }
    }

    public partial class EnvioInspeccion
    {
        public List<ItemsInspeccionado> itemsInspeccionados { get; set; }
    }


    public class ItemsInspeccionado
    {
        public object INSPE_ID { get; set; }
        public int INSPE_INSP_ID { get; set; }
        public int INSPE_ITINS_ID { get; set; }
        public string INSPE_OBSERVACION { get; set; }
        public int INSPE_DESHABILITADO { get; set; }
        //public ClcarFotosItemInspeccion CLCAR_FOTOS_ITEM_INSPECCION { get; set; }
        //public ClcarItemInspeccion CLCAR_ITEM_INSPECCION { get; set; }
        public ClcarInspeccionSustituir CLCAR_INSPECCION_SUSTITUIR { get; set; }
        public ClcarInspeccionReparar CLCAR_INSPECCION_REPARAR { get; set; }
    }

    public partial class ClcarInspeccionReparar
    {
        public object INSPE_ID { get; set; }
        public string IREPA_TIREP_DESCRIPCION { get; set; }
        public int IREPA_VALOR_REPARAR { get; set; }
        public string IREPA_COMENTARIO { get; set; }
    }

    public partial class ClcarInspeccionSustituir
    {
        public object INSPE_ID { get; set; }
        public int ISUST_VALOR_SUSTITUIR { get; set; }
        public string ISUST_COMENTARIO { get; set; }
    }


    public partial class ClcarFotosItemInspeccion
    {
        public int INSPE_ID { get; set; }
        public string FIINS_NOMBRE_ARCHIVO { get; set; }
        public DateTime FIINS_FECHA_CREACION { get; set; }
    }

    public partial class ClcarItemInspeccion
    {
        public int ITINS_ID { get; set; }
        public int ITINS_AINSP_ID { get; set; }
        public string ITINS_DESCRIPCION { get; set; }
        public string ITINS_CONDICION { get; set; }
        public bool ITINS_DESHABILITAR { get; set; }
        public bool ITINS_REQUIERE_FOTO { get; set; }
        public int ITINS_ORDEN_APP { get; set; }
        public bool ITINS_ACTIVO { get; set; }
    }
}
