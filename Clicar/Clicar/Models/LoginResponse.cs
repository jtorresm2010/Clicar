using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class LoginResponse
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
        public Elemento Elemento { get; set; }

    }
}
