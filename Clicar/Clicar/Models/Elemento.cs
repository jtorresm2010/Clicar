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
        public List<object> fotografias { get; set; }
        public List<TipoFotografia> tipo_fotografias { get; set; }
    }
}
