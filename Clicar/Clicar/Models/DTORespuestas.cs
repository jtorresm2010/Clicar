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


    public partial class DTOGenerico
    {
        public bool Resultado { get; set; }
        public object Mensaje { get; set; }
        public object Elemento { get; set; }
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

    public partial class EncabezadoElement
    {
        public int INSP_ID { get; set; }
        public int INSP_SOINS_ID { get; set; }
        public int INSP_USU_ID { get; set; }
        public DateTime INPS_FECHA_INICIO { get; set; }
        public DateTime INSP_FECHA_FIN { get; set; }
        public string INSP_COMENTARIOS { get; set; }
        public int INSP_NRO_ESTRELLAS { get; set; }
        public int INSP_ESTDO_ID { get; set; }
        public List<object> CLCAR_FOTOS_INSPECCION { get; set; }
        public object CLCAR_MAE_ESTADOS_INSPECCION { get; set; }
        public List<object> CLCAR_PROPUESTA { get; set; }
        public List<object> CLCAR_ITEMS_INSPECCIONADOS { get; set; }
        public object CLCAR_MAE_USUARIOS { get; set; }
        public object CLCAR_SOLICITUD_INSPECCION { get; set; }
    }

    public partial class EncabezadoResponse
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }
        public EncabezadoElement Elemento { get; set; }
    }











}
