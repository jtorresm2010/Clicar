using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class DetallesItem
    {
        public object ItemInspeccion { get; set; }
        public bool Estado { get; set; }
        public string Solucion { get; set; }
        public object ItemDetalle { get; set; }
        public string Condicion { get; set; }
        public object Imagen { get; set; }
    }
}
