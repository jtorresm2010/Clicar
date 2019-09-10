using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class AccordionItem : AreasInspeccion
    {
        public string Image { get; set; }
        public List<ItemsAreasInspeccion> Items { get; set; }
        public bool IsImageSet { get; set; }

    }
}
