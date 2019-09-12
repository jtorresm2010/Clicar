using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public partial class Fotografia
    {
        public int      FOTO_ID                 { get; set; }
        public int      FOTO_TIPOF_ID           { get; set; }
        public string   FOTO_DESCRIPCION        { get; set; }
        public bool     FOTO_REQUIERE_MARCO     { get; set; }
        public object   FOTO_MARCO              { get; set; }
        public bool     FOTO_ACTIVO             { get; set; }
        public bool     FOTO_OBLIGATORIA        { get; set; }
        public object   CLCAR_TIPO_FOTOGRAFIA   { get; set; }
    }

    [Table("FotografiaDB")]
    public partial class FotografiaDB
    {

        [PrimaryKey, MaxLength(20)]
        public int FOTO_ID { get; set; }
        public int FOTO_TIPOF_ID { get; set; }
        public string FOTO_DESCRIPCION { get; set; }
        public bool FOTO_REQUIERE_MARCO { get; set; }
        public bool FOTO_ACTIVO { get; set; }
        public bool FOTO_OBLIGATORIA { get; set; }
        //public int CLCAR_TIPO_FOTOGRAFIA_ID { get; set; }
    }

    [Table("TipoFotografia")]
    public partial class TipoFotografia
    {
        [PrimaryKey, MaxLength(20)]
        public int TIPOF_ID { get; set; }
        public string TIPOF_DESCRIPCION { get; set; }
        public bool TIPOF_ACTIVO { get; set; }
    }

    [Table("FotografiaLocal")]
    public partial class FotografiaLocal
    {
        [PrimaryKey, AutoIncrement]
        [JsonProperty("ID")]
        public int LOCAL_IMAGE_ID { get; set; }
        public string LOCAL_IMAGERUTA { get; set; }
        public int ITEM_CORRESP { get; set; }
    }


}
