using Clicar.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Clicar.Models
{
    public partial class ItemsAreasInspeccion
    {
        public int      ITINS_ID                { get; set; }
        public int      ITINS_AINSP_ID          { get; set; }
        public string   ITINS_DESCRIPCION       { get; set; }
        public string   ITINS_CONDICION         { get; set; }
        public bool     ITINS_DESHABILITAR      { get; set; }
        public bool     ITINS_REQUIERE_FOTO     { get; set; }
        public int      ITINS_ORDEN_APP         { get; set; }
        public bool     ITINS_ACTIVO            { get; set; }
        public AreasInspeccion   CLCAR_AREA_INSPECCION   { get; set; }
        public List<ValorSustituir> CLCAR_VALOR_SUSTITUIR { get; set; }
        public List<ValorRepararItem> CLCAR_VALOR_REPARAR_ITEM { get; set; }
    }

    public partial class ItemsAreasInspeccionACC : BaseViewModel
    {
        public int ITINS_ID { get; set; }
        public int ITINS_AINSP_ID { get; set; }
        public string ITINS_DESCRIPCION { get; set; }
        public string ITINS_CONDICION { get; set; }
        public bool ITINS_DESHABILITAR { get; set; }
        public bool ITINS_REQUIERE_FOTO { get; set; }
        public int ITINS_ORDEN_APP { get; set; }
        public bool ITINS_ACTIVO { get; set; }
        //public bool ITINS_STATE_ACTIVO { get; set; }
        public AreasInspeccion CLCAR_AREA_INSPECCION { get; set; }
        public List<ValorSustituir> CLCAR_VALOR_SUSTITUIR { get; set; }
        public List<ValorRepararItem> CLCAR_VALOR_REPARAR_ITEM { get; set; }

        private bool iTINS_STATE_ACTIVO;
        public bool ITINS_STATE_ACTIVO
        {
            get { return iTINS_STATE_ACTIVO; }
            set { SetValue(ref iTINS_STATE_ACTIVO, value); }
        }

        private bool isLocked;

        public bool ITINS_IS_LOCKED
        {
            get { return isLocked; }
            set { SetValue(ref isLocked, value); }
        }

    }


    [Table("ItemsAreasInspeccionDB")]
    public partial class ItemsAreasInspeccionDB
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

    public partial class ValorRepararItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int VAREP_ITINS_ID { get; set; }
        public int VAREP_SUBTI_ID { get; set; }
        public int VAREP_TIREP_ID { get; set; }
        public int VAREP_VALOR_REPARACION { get; set; }
        public DateTime VAREP_FECHA_ACTUALIZACION { get; set; }

    }

    public partial class ValorSustituir
    {
        public int VASUS_ITINS_ID { get; set; }
        public int VASUS_SUBTI_ID { get; set; }
        public int VASUS_VALOR_REFERENCIAL { get; set; }
        public int VASUS_VALOR_SUSTITUIR { get; set; }
        public DateTime VASUS_FECHA_ACTUALIZACION { get; set; }
    }
}

