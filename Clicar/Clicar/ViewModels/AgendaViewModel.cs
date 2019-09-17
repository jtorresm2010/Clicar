using Clicar.Helpers;
using Clicar.Models;
using Clicar.Services;
using Clicar.Templates;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clicar.ViewModels
{
    public class AgendaViewModel : BaseViewModel
    {
        MainViewModel MainInstance;

        #region Variables
        private bool IsBusy = false;
        public List<AreasInspeccion> AreasInspeccion { get; set; }
        private List<SolicitudesInspeccionPendiente> listaInpecciones;
        private ObservableCollection<Inspeccion> listaPendientes;
        private ObservableCollection<Inspeccion> listaCompletados;
        private int PendienteCount;
        private int CompletadosCount;
        private double pendientesHeight;
        private double completadosHeight;
        private bool listReady;
        private int ListCount = 1;
        private double rowHeight = 110;
        private SucursalDB currentSucursal;
        private Maestro maestro;
        private bool isLoading;
        private string nombreMaestro;
        private string currentDate;
        #endregion


        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetValue(ref isRefreshing, value); }
        }


        #region Propiedades
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetValue(ref isLoading, value); }
        }
        private ObservableCollection<SolicitudesInspeccionTerminada> listaCompletadosAPI;
        public ObservableCollection<SolicitudesInspeccionTerminada> ListaCompletadosAPI
        {
            get { return listaCompletadosAPI; }
            set { SetValue(ref listaCompletadosAPI, value); }
        }

        private ObservableCollection<SolicitudesInspeccionPendiente> listaPendientesAPI;
        public ObservableCollection<SolicitudesInspeccionPendiente> ListaPendientesAPI
        {
            get { return this.listaPendientesAPI; }
            set { this.SetValue(ref this.listaPendientesAPI, value); }
        }
        public ObservableCollection<Inspeccion> ListaCompletados
        {
            get { return listaCompletados; }
            set { SetValue(ref listaCompletados, value); }
        }
        public ObservableCollection<Inspeccion> ListaPendientes
        {
            get { return this.listaPendientes; }
            set { this.SetValue(ref this.listaPendientes, value); }
        }
        public double PendientesHeight
        {
            get { return pendientesHeight; }
            set { SetValue(ref pendientesHeight, value); }
        }
        public double CompletadosHeight
        {
            get { return completadosHeight; }
            set { SetValue(ref completadosHeight, value); }
        }
        public bool ListReady
        {
            get { return listReady; }
            set { SetValue(ref listReady, value); }
        }
        public double RowHeight
        {
            get { return rowHeight; }
            set { SetValue(ref rowHeight, value); }
        }
        public Maestro Maestro
        {
            get { return maestro; }
            set { SetValue(ref maestro, value); }
        }
        public string NombreMaestro
        {
            get { return nombreMaestro; }
            set { SetValue(ref nombreMaestro, value); }
        }
        public SucursalDB CurrentSucursal
        {
            get { return currentSucursal; }
            set { SetValue(ref currentSucursal, value); }
        }
        public string CurrentDate
        {
            get { return currentDate; }
            set { SetValue(ref currentDate, value); }
        }

        #endregion


        public AgendaViewModel()
        {
            MainInstance = MainViewModel.GetInstance();
            //LoadMainList();
            CurrentDate = GetFormattedDate();


        }

        private string GetFormattedDate()
        {
            var day = DateTime.Now.Day.ToString().Length > 1 ? DateTime.Now.Day.ToString() : $"0{DateTime.Now.Day}";
            var month = DateTime.Now.Month.ToString().Length > 1 ? DateTime.Now.Month.ToString() : $"0{DateTime.Now.Month}";
            var year = DateTime.Now.Year;

            string date = $"{day}/{month}/{year}";

            return date;
        }

        public void MaestroName(Maestro maestro)
        {
            Maestro = maestro;
            NombreMaestro = $"{Maestro.USU_NOMBRES} {Maestro.USU_APELLIDO_PATERNO}";
        }

        public async void LoadMainList()
        {
            IsRefreshing = true;


            //listaInpecciones = new ListaInspecciones().GetListaInspeccion();

            IsLoading = true;

            var connection = MainInstance.RestService.CheckConnection();
            if (!connection.IsSuccess)
            {
                var popup = PopupNavigation.Instance;
                await popup.PushAsync(new AlertPopup("", connection.Message, Languages.Accept));
                return;
            }

            //var parameter = $"?suc_id=1";
            var parameter = $"?suc_id={CurrentSucursal.MAESU_ID}";


            var response = await MainInstance.RestService.PostAsync<AgendaResponse>(MainInstance.Url, MainInstance.Prefix, $"{MainInstance.AgendaInspeccion}{parameter}");

            //Debug.WriteLine($"~(>'.')> {response.Message}");

            try
            {
                var AgendaResponse = (AgendaResponse)response.Result;

                if (AgendaResponse.Resultado)
                {

                    if(AgendaResponse.Elemento.solicitudes_inspeccion_pendiente.Count > 0)
                    {
                        CargarListas(AgendaResponse.Elemento.solicitudes_inspeccion_pendiente, AgendaResponse.Elemento.solicitudes_inspeccion_terminada);

                    }
                    else
                    {
                        var testList = new List<SolicitudesInspeccionPendiente>();

                        testList.AddRange(new[] {
                        new SolicitudesInspeccionPendiente
                        {
                            VERSION_DESCRIPCION = "asdf",
                            SOTAS_PATENTE = "PJPK66",
                            SUBTI_DESCRIPCION = "No se de tipos :P",
                            SOTAS_APELLIDO = "Perez",
                            SOINS_VIN = "123456",
                            SOINS_TRANSMISION = true,
                            SOINS_RUT_CLIENTE = "12345675",
                            SOINS_FECHA_CITA = DateTime.Now,
                            SOINS_FECHA_ACTUALIZACION = DateTime.Now,
                            SOINS_ESINS_ID = 123,
                            SOINS_CODIGO_CASO = "AB123",
                            MODELO = "Corolla",
                            MARCA = "Toyota",
                            MACOL_DESCRIPCION = "Gamboge",
                            SOTAS_NOMBRE = "Peter",
                            MAE_ANIO = 1997,
                        }
                        });

                        CargarListas(testList, AgendaResponse.Elemento.solicitudes_inspeccion_terminada);
                    }





                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error: " + ex.Message);
            }

            IsLoading = false;
            IsRefreshing = false;

        }

        public void CargarListas(List<SolicitudesInspeccionPendiente> pendientes, List<SolicitudesInspeccionTerminada> terminadas)
        {
            ListaPendientesAPI = new ObservableCollection<SolicitudesInspeccionPendiente>(pendientes);
            PendienteCount = ListaPendientesAPI.Count;
            PendientesHeight = PendienteCount * RowHeight;

            ListaCompletadosAPI = new ObservableCollection<SolicitudesInspeccionTerminada>(terminadas);
            CompletadosCount = ListaCompletadosAPI.Count;
            CompletadosHeight = CompletadosCount * RowHeight;

            ListReady = true;
        }

        public bool IsLastItem()
        {
            if (PendienteCount == ListCount)
            {
                ListCount = 1;
                PendienteCount = CompletadosCount;
                return true;
            }

            else
            {
                ListCount++;
                return false;
            }
        }



        #region ICommands
        public ICommand ItemTappedICommand
        {
            get
            {
                return new RelayCommand<SolicitudesInspeccionPendiente>(inspeccion => ItemTappedCommand(inspeccion));
            }

        }
        public ICommand ItemCompletadoICommand
        {
            get
            {
                return new RelayCommand<SolicitudesInspeccionTerminada>(inspeccion => ItemCompletadoCommand(inspeccion));
            }

        }



        #endregion




        private void ItemCompletadoCommand(SolicitudesInspeccionTerminada inspeccion)
        {
            Debug.WriteLine($"~(>'.')> Inspeccion completada {inspeccion.SOINS_RUT_CLIENTE}");
        }

        private async void ItemTappedCommand(SolicitudesInspeccionPendiente inspeccion)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            string Vin = inspeccion.SOINS_VIN;
            string hiddenVin = "";

            for (int i = 0; i < Vin.Length; i++)
            {
                hiddenVin = hiddenVin + " *";
            }

            //for(int u=0: u< )


            MainInstance.DetalleInspeccion = new DetalleInspeccionViewModel();
            MainInstance.DetalleInspeccion.CurrentInspeccion = inspeccion;
            MainInstance.DetalleInspeccion.HiddenVin = hiddenVin;

            await Application.Current.MainPage.Navigation.PushAsync(new DetalleInspeccionView());

            IsBusy = false;
        }

        public void RechazarInspeccion(SolicitudesInspeccionPendiente inspeccion)
        {
            Debug.WriteLine($"~(>'.')> rechazasda para {inspeccion.SOINS_RUT_CLIENTE}");

            //var InspeccionRechazada = inspeccion;
            //InspeccionRechazada.Estado = "Rechazado";

            //listaInpecciones[listaInpecciones.IndexOf(inspeccion)] = InspeccionRechazada;

            //CargarListas();
        }

    }
}
