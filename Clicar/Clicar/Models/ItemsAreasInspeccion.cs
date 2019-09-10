using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{

    [Table("ItemsAreasInspeccionDB")]
    public class ItemsAreasInspeccionDB
    {
        [PrimaryKey, MaxLength(20)]
        public int ITINS_ID { get; set; }
        public int ITINS_AINSP_ID { get; set; }
        public string ITINS_DESCRIPCION { get; set; }
        public string ITINS_CONDICION { get; set; }
        public bool ITINS_DESHABILITAR { get; set; }
        public bool ITINS_REQUIERE_FOTO { get; set; }
        public int ITINS_ORDEN_APP { get; set; }
        public bool ITINS_ACTIVO { get; set; }
    }

    public class ItemsAreasInspeccion
    {
        public int      ITINS_ID             { get; set; }
        public int      ITINS_AINSP_ID        { get; set; }
        public string   ITINS_DESCRIPCION     { get; set; }
        public string   ITINS_CONDICION         { get; set; }
        public bool     ITINS_DESHABILITAR       { get; set; }
        public bool     ITINS_REQUIERE_FOTO     { get; set; }
        public int      ITINS_ORDEN_APP         { get; set; }
        public bool     ITINS_ACTIVO             { get; set; }
        public object   CLCAR_AREA_INSPECCION   { get; set; }
        public List<object> CLCAR_VALOR_SUSTITUIR { get; set; }
        public List<object> CLCAR_VALOR_REPARAR_ITEM { get; set; }
    }




}
