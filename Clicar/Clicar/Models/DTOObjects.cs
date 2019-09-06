using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    [Table("Usuario")]
    public partial class Usuario
    {
        public string USU_USERNAME { get; set; }
        public string USU_CLAVE { get; set; }
        public string ORIGEN { get; set; }
    }


    public partial class dTOUbicacion
    {
        public double LATITUD { get; set; }
        public double LONGITUD { get; set; }
    }



}
