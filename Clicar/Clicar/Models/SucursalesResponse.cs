using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class SucursalesResponse
    {
        public bool Resultado { get; set; }
        public object Mensaje { get; set; }
        public List<Sucursal> Elemento { get; set; }
    }
}