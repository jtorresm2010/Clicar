using System;
using System.Collections.Generic;

namespace Clicar.Models
{
    public partial class LoginResponse
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
        public Maestro Elemento { get; set; }

    }

    public partial class SucursalesResponse
    {
        public bool Resultado { get; set; }
        public object Mensaje { get; set; }
        public List<Sucursal> Elemento { get; set; }
    }

    public partial class AreasResponse
    {
        public bool Resultado { get; set; }
        public object Mensaje { get; set; }
        public Elemento Elemento { get; set; }
    }

    public partial class AgendaResponse
    {
        public bool Resultado { get; set; }
        public object Mensaje { get; set; }
        public ElementoAgenda Elemento { get; set; }
    }

    public partial class ElementoAgenda
    {
        public List<SolicitudesInspeccionPendiente> solicitudes_inspeccion_pendiente { get; set; }
        public List<SolicitudesInspeccionTerminada> solicitudes_inspeccion_terminada { get; set; }
    }



}
