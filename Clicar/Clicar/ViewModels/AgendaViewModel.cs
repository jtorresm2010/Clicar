﻿using Clicar.Models;
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
        private ObservableCollection<Inspeccion> listaPendientes;
        private ObservableCollection<Inspeccion> listaCompletados;
        private int PendienteCount;
        private int CompletadosCount;
        private double pendientesHeight;
        private double completadosHeight;
        private bool listReady;
        private int ListCount = 1;
        private double rowHeight = 110;
        private Sucursal currentSucursal;
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
        public Sucursal CurrentSucursal
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
            InicializarLista();

            CurrentDate = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
        }

        public void MaestroName(Maestro maestro)
        {
            Maestro = maestro;
            NombreMaestro = $"{Maestro.USU_NOMBRES} {Maestro.USU_APELLIDO_PATERNO}";
        }

        public void InicializarLista()
        {
            List<Inspeccion> listaInpecciones = new ListaInspecciones().GetListaInspeccion();

            //var listaVehiulos = new VehiculosList().GetListaVehiculos();

            var pendientesQuery =
                from inspeccion in listaInpecciones
                where inspeccion.Estado != "Completado"
                select inspeccion;

            ListaPendientes = new ObservableCollection<Inspeccion>(pendientesQuery);
            PendienteCount = ListaPendientes.Count;
            PendientesHeight = PendienteCount * RowHeight;

            //var listaModificada = listaInpecciones.GetRange(2, 5);

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

        public ICommand ItemTappedICommand
        {
            get
            {
                return new RelayCommand<Inspeccion>(inspeccion => ItemTappedCommand(inspeccion));
            }

        }

        private void ItemTappedCommand(Inspeccion inspeccion)
        {
            MainInstance.DetalleInspeccion = new DetalleInspeccionViewModel();
            MainInstance.DetalleInspeccion.CurrentInspeccion = inspeccion;
            Application.Current.MainPage.Navigation.PushAsync(new DetalleInspeccionView());
        }


        public void RechazarInspeccion(Inspeccion inspeccion)
        {
            var InspeccionRechazada = inspeccion;
            InspeccionRechazada.Estado = "Anulado";

            ListaPendientes[ListaPendientes.IndexOf(inspeccion)] = InspeccionRechazada;
        }



    }
}
