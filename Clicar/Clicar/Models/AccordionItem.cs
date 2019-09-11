using Clicar.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Clicar.Models
{
    public class AccordionItem : BaseViewModel
    {
        public int AINSP_ID { get; set; }
        public string AINSP_DESCRIPCION { get; set; }
        public int AINSP_ORDEN_APP { get; set; }
        public int AINSP_ACTIVO { get; set; }
        public int AINSP_PAIS_ID { get; set; }
        public string Image { get; set; }
        //public List<ItemsAreasInspeccionACC> Items { get; set; }
        public bool IsImageSet { get; set; }





        private List<ItemsAreasInspeccionACC> items;

        public List<ItemsAreasInspeccionACC> Items
        {
            get { return items; }
            set { SetValue(ref items, value); }
        }





    }
}
