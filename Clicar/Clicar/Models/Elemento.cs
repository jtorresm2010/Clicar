using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class Elemento
    {
        public List<Itemsdetalleinspeccion> itemsdetalleinspeccion { get; set; }
        public List<AreasInspeccion> areas_inspeccion { get; set; }
        public List<ItemsAreasInspeccion> items_areas_inspeccion { get; set; }
        public List<Fotografia> fotografias { get; set; }
        public List<TipoFotografia> tipo_fotografias { get; set; }
        public List<Subtipo> subtipo { get; set; }



    }

    public class Subtipo
    {
        public int SUBTI_ID { get; set; }
        public string SUBTI_DESCRIPCION { get; set; }
        public bool SUBTI_ACTIVO { get; set; }
        public List<object> CLCAR_VALOR_REPARAR_ITEM { get; set; }
        public List<object> CLCAR_VALOR_SUSTITUIR { get; set; }
        public List<object> CLCAR_SOLICITUD_INSPECCION { get; set; }
    }

}
