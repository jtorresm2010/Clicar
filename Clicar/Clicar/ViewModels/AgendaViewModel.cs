using Clicar.Helpers;
using Clicar.Models;
using Clicar.Services;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
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
        public List<AreasInspeccion> AreasInspeccion { get; set; }
        public List<ItemsAreasInspeccionDB> ItemsInspeccion { get; set; }
        private List<Inspeccion> listaInpecciones;
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



        private ObservableCollection<SolicitudesTerminada> listaCompletadosAPI;
        public ObservableCollection<SolicitudesTerminada> ListaCompletadosAPI
        {
            get { return listaCompletadosAPI; }
            set { SetValue(ref listaCompletadosAPI, value); }
        }


        private ObservableCollection<SolicitudesPendiente> listaPendientesAPI;
        public ObservableCollection<SolicitudesPendiente> ListaPendientesAPI
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
            LoadMainList();
            //CargararListas();
            CargarAreasDeInspeccion();
            CurrentDate = GetFormattedDate();


        }

        private async void CargarAreasDeInspeccion()
        {
            //AreasInspeccion = await MainInstance.DataService.GetAreasInspeccion();
            //ItemsInspeccion = await MainInstance.DataService.GetItemsInspeccion();
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


            listaInpecciones = new ListaInspecciones().GetListaInspeccion();

            IsLoading = true;

            var connection = MainInstance.RestService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("", connection.Message, Languages.Accept);
                return;
            }

            var parameter = $"?suc_id=1";
            //var parameter = $"suc_id={CurrentSucursal.MAESU_ID}";


            var response = await MainInstance.RestService.PostAsync<AgendaResponse>(MainInstance.Url, MainInstance.Prefix, $"{MainInstance.AgendaInspeccion}{parameter}");

            Debug.WriteLine($"~(>'.')> {MainInstance.Url}{MainInstance.Prefix}{MainInstance.AgendaInspeccion}{parameter}");

            try
            {
                var AgendaResponse = (AgendaResponse)response.Result;

                if (AgendaResponse.Resultado)
                {
                    var agendaResponse = JsonConvert.DeserializeObject<ElementoAgenda>(AgendaResponse.Elemento);

                    //foreach(SolicitudesPendiente ele in agendaResponse.solicitudes_pendientes)
                    //{
                    //    Debug.WriteLine($"~(>'.')> {ele.SOINS_FECHA_CREACION}");
                    //}

                    //Debug.WriteLine($"~(>'.')> {AgendaResponse.Elemento}");


                    CargarListas(agendaResponse.solicitudes_pendientes, agendaResponse.solicitudes_terminadas);


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error: " + ex.Message);
            }

            IsLoading = false;
            IsRefreshing = false;

        }

        public void CargarListas(List<SolicitudesPendiente> pendientes, List<SolicitudesTerminada> terminadas)
        {
            ListaPendientesAPI = new ObservableCollection<SolicitudesPendiente>(pendientes);
            PendienteCount = ListaPendientesAPI.Count;
            PendientesHeight = PendienteCount * RowHeight;

            ListaCompletadosAPI = new ObservableCollection<SolicitudesTerminada>(terminadas);
            CompletadosCount = ListaCompletadosAPI.Count;
            CompletadosHeight = CompletadosCount * RowHeight;

            ListReady = true;
        }


        public void CargarListas()
        {
            var pendientesQuery =
                from inspeccion in listaInpecciones
                where inspeccion.Estado != "Completado"
                orderby inspeccion.Estado descending
                select inspeccion;

            ListaPendientes = new ObservableCollection<Inspeccion>(pendientesQuery);
            PendienteCount = ListaPendientes.Count;
            PendientesHeight = PendienteCount * RowHeight;


            var completadasQuery =
                from inspeccion in listaInpecciones
                where inspeccion.Estado == "Completado"
                select inspeccion;

            ListaCompletados = new ObservableCollection<Inspeccion>(completadasQuery);
            CompletadosCount = ListaCompletados.Count;
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
        public ICommand ItemCompletadoICommand
        {
            get
            {
                return new RelayCommand<Inspeccion>(inspeccion => ItemCompletadoCommand(inspeccion));
            }

        }

        public ICommand ItemTappedICommand
        {
            get
            {
                return new RelayCommand<Inspeccion>(inspeccion => ItemTappedCommand(inspeccion));
            }

        }

        private void ItemCompletadoCommand(Inspeccion inspeccion)
        {
            
            Debug.WriteLine($"~(>'.')> Inspeccion completada {inspeccion.Num_Inspeccion}");
            //MainInstance.DetalleInspeccion = new DetalleInspeccionViewModel();
            //MainInstance.DetalleInspeccion.CurrentInspeccion = inspeccion;
            //Application.Current.MainPage.Navigation.PushAsync(new DetalleInspeccionView());
        }

        private bool IsBusy = false;
        private async void ItemTappedCommand(Inspeccion inspeccion)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            MainInstance.DetalleInspeccion = new DetalleInspeccionViewModel();
            MainInstance.DetalleInspeccion.CurrentInspeccion = inspeccion;
            await Application.Current.MainPage.Navigation.PushAsync(new DetalleInspeccionView());
            IsBusy = false;
        }

        public void RechazarInspeccion(Inspeccion inspeccion)
        {
            var InspeccionRechazada = inspeccion;
            InspeccionRechazada.Estado = "Rechazado";

            listaInpecciones[listaInpecciones.IndexOf(inspeccion)] = InspeccionRechazada;

            CargarListas();
        }
    }
}
