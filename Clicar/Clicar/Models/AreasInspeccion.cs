using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    [Table("AreasInspeccion")]
    public class AreasInspeccion
    {
        [PrimaryKey, MaxLength(20)]
        public int AINSP_ID { get; set; }
        public string AINSP_DESCRIPCION { get; set; }
        public int AINSP_ORDEN_APP { get; set; }
        public int AINSP_ACTIVO { get; set; }
        public int AINSP_PAIS_ID { get; set; }

    }
}
