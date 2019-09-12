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
        public string Elemento { get; set; }
    }

    public partial class ElementoAgenda
    {
        public List<SolicitudesPendiente> solicitudes_pendientes { get; set; }
        public List<SolicitudesTerminada> solicitudes_terminadas { get; set; }
    }



}
