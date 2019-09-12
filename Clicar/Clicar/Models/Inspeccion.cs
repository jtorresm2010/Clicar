using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class Inspeccion
    {
        public string Num_Inspeccion { get; set; }
        public string Patente { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string StartTime { get; set; }
        public string Year { get; set; }
        public string Version { get; set; }
        public string Mileage { get; set; }
        public string Type { get; set; }
        public string Client_Name { get; set; }
        public string Client_Rut { get; set; }
        public string Estado { get; set; }
    }


    public partial class SolicitudesPendiente
    {
        public int SOINS_ID { get; set; }
        public int SOINS_SOTAS_ID { get; set; }
        public int SOINS_MAESU_ID { get; set; }
        public object SOINS_USU_ID { get; set; }
        public string SOINS_CODIGO_CASO { get; set; }
        public DateTime SOINS_FECHA_CREACION { get; set; }
        public int SOINS_HOSUC_ID { get; set; }
        public string SOINS_HORA_INICIO { get; set; }
        public string SOINS_HORA_FIN { get; set; }
        public object SOINS_RUT_CLIENTE { get; set; }
        public int SOINS_MACOL_ID { get; set; }
        public DateTime SOINS_FECHA_CITA { get; set; }
        public object SOINS_KILOMETRAJE { get; set; }
        public object SOINS_VIN { get; set; }
        public int SOINS_SUBTI_ID { get; set; }
        public object SOINS_CONT_TASACIONES { get; set; }
        public DateTime SOINS_FECHA_ACTUALIZACION { get; set; }
        public object SOINS_TRANSMISION { get; set; }
        public int SOINS_ULTIMO_PRECIO_AUTORED { get; set; }
        public int SOINS_ULTIMO_PRECIO_TASACION { get; set; }
        public int SOINS_ESINS_ID { get; set; }
        public List<object> CLCAR_ANEXOS_INSPECCION { get; set; }
        public object CLCAR_HORARIO_SUCURSAL { get; set; }
        public List<object> CLCAR_INSPECCION { get; set; }
        public object CLCAR_MAE_COLORES { get; set; }
        public object CLCAR_MAE_SUBTIPO { get; set; }
        public object CLCAR_MAE_SUCURSAL { get; set; }
        public object CLCAR_MAE_USUARIOS { get; set; }
        public List<object> CLCAR_RESULTADO_HISTORIAL { get; set; }
        public object CLCAR_SOLICITUD_TASACION { get; set; }
        public object CLCAR_ESTADOS_SOLINSPECCION { get; set; }
    }

    public partial class SolicitudesTerminada
    {
        public int SOINS_ID { get; set; }
        public int SOINS_SOTAS_ID { get; set; }
        public int SOINS_MAESU_ID { get; set; }
        public object SOINS_USU_ID { get; set; }
        public string SOINS_CODIGO_CASO { get; set; }
        public DateTime SOINS_FECHA_CREACION { get; set; }
        public int SOINS_HOSUC_ID { get; set; }
        public string SOINS_HORA_INICIO { get; set; }
        public string SOINS_HORA_FIN { get; set; }
        public string SOINS_RUT_CLIENTE { get; set; }
        public int SOINS_MACOL_ID { get; set; }
        public DateTime SOINS_FECHA_CITA { get; set; }
        public int SOINS_KILOMETRAJE { get; set; }
        public string SOINS_VIN { get; set; }
        public int SOINS_SUBTI_ID { get; set; }
        public int? SOINS_CONT_TASACIONES { get; set; }
        public DateTime SOINS_FECHA_ACTUALIZACION { get; set; }
        public bool? SOINS_TRANSMISION { get; set; }
        public int SOINS_ULTIMO_PRECIO_AUTORED { get; set; }
        public int SOINS_ULTIMO_PRECIO_TASACION { get; set; }
        public int SOINS_ESINS_ID { get; set; }
        public List<object> CLCAR_ANEXOS_INSPECCION { get; set; }
        public object CLCAR_HORARIO_SUCURSAL { get; set; }
        public List<object> CLCAR_INSPECCION { get; set; }
        public object CLCAR_MAE_COLORES { get; set; }
        public object CLCAR_MAE_SUBTIPO { get; set; }
        public object CLCAR_MAE_SUCURSAL { get; set; }
        public object CLCAR_MAE_USUARIOS { get; set; }
        public List<object> CLCAR_RESULTADO_HISTORIAL { get; set; }
        public object CLCAR_SOLICITUD_TASACION { get; set; }
        public object CLCAR_ESTADOS_SOLINSPECCION { get; set; }

    }




}
