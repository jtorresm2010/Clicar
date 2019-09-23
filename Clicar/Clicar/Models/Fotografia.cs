using Clicar.ViewModels;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Models
{
    public partial class Fotografia : BaseViewModel
    {
        public int      FOTO_ID                 { get; set; }
        public int      FOTO_TIPOF_ID           { get; set; }
        public string   FOTO_DESCRIPCION        { get; set; }
        public bool     FOTO_REQUIERE_MARCO     { get; set; }
        public string   FOTO_MARCO              { get; set; }
        public bool     FOTO_ACTIVO             { get; set; }
        public bool     FOTO_OBLIGATORIA        { get; set; }
        public object   CLCAR_TIPO_FOTOGRAFIA   { get; set; }

        private string fOTO_BASE64;

        public string FOTO_BASE64
        {
            get { return  fOTO_BASE64; }
            set { SetValue(ref  fOTO_BASE64, value); }
        }


        private string currentImageBig;

        public string CurrentImageBig
        {
            get { return currentImageBig; }
            set { SetValue(ref currentImageBig, value); }
        }

        private string currentImageSmall;

        public string CurrentImageSmall
        {
            get { return currentImageSmall; }
            set { SetValue(ref currentImageSmall, value); }
        }




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
        public string FOTO_MARCO { get; set; }
        public string FOTO_BASE64 { get; set; }
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
