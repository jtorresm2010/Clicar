using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class MenuElement
    {
        public string Titulo { get; set; }
        public string Bloqueo { get; set; }
        public bool Estado { get; set; }
        public string Condicion { get; set; }
        public string Imagen { get; set; }
        public bool IDElemento { get; set; }
    }
}
