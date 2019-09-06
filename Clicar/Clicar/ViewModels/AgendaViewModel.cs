using Clicar.Models;
using Clicar.Services;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clicar.ViewModels
{
    public class AgendaViewModel : BaseViewModel
    {
        MainViewModel MainInstance;

        #region Variables
        public List<AreasInspeccion> AreasInspeccion;
        private RestService RestService;
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
        private string nombreMaestro;
        private string currentDate;
        #endregion


        #region Propiedades
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
            RestService = new RestService();
            LoadMainList();
            CargararListas();
            CurrentDate = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
        }

        public void MaestroName(Maestro maestro)
        {
            Maestro = maestro;
            NombreMaestro = $"{Maestro.USU_NOMBRES} {Maestro.USU_APELLIDO_PATERNO}";
        }

        private void LoadMainList()
        {
            listaInpecciones = new ListaInspecciones().GetListaInspeccion();

        }

        public void CargararListas()
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

        private void ItemTappedCommand(Inspeccion inspeccion)
        {
            ObtenerAreasInspeccion();


            MainInstance.DetalleInspeccion = new DetalleInspeccionViewModel();
            MainInstance.DetalleInspeccion.CurrentInspeccion = inspeccion;
            Application.Current.MainPage.Navigation.PushAsync(new DetalleInspeccionView());
        }

        private async void ObtenerAreasInspeccion()
        {
            string data = $"?UsuID={Maestro.USU_ID}";


            var response = await RestService.GetAsync<AreasResponse>(MainInstance.Url, MainInstance.Prefix, MainInstance.ControllerMaestros, data);

            try
            {
                AreasResponse resp = (AreasResponse)response.Result;

                if (resp.Resultado)
                {
                    AreasInspeccion = resp.Elemento.areas_inspeccion;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error" + ex.Message);
            }



        }

        public void RechazarInspeccion(Inspeccion inspeccion)
        {
            var InspeccionRechazada = inspeccion;
            InspeccionRechazada.Estado = "Rechazado";

            listaInpecciones[listaInpecciones.IndexOf(inspeccion)] = InspeccionRechazada;

            CargararListas();
        }
    }
}
