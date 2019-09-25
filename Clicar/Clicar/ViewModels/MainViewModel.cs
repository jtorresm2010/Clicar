﻿using Clicar.Services;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Xamarin.Forms;


namespace Clicar.ViewModels
{
    public class MainViewModel
    {
        public string Token { get; set; }
        public string Url { get; set; }
        public string Prefix { get; set; }
        public string ControllerLogin { get; set; }
        public string ControllerSucursal { get; set; }
        public string ControllerMaestros { get; set; }
        public string ControllerCercanaSucursal { get; set; }
        public string AgendaInspeccion { get; set; }
        public string AgendaInspeccionEncabezado { get; set; }
        public string AgendaInspeccionRechazo { get; set; }
        public string AgendaInspeccionCuerpo { get; set; }
        public string AgendaInspeccionEnvioFoto { get; set; }
        public string EnvioInspeccionEncabezado { get; set; }
        public string EnvioInspeccionCuerpo { get; set; }
        public string RecuperaPass { get; set; }
        public string EnvioFotoInspeccion { get; set; }


        #region Instancias VM
        public DetalleItemViewModel DetalleItem { get; set; }
        public LoginViewModel Login { get; set; }
        public ConfigViewModel Config { get; set; }
        public AgendaViewModel Agenda { get; set; }
        public DetalleInspeccionViewModel DetalleInspeccion { get; set; }
        public InspeccionViewModel Inspeccion { get; set; }
        public DataService DataService { get; set; }
        public RestService RestService { get; set; }
        public ReporteViewModel Reporte { get; set; }
        #endregion

        public MainViewModel()
        {
            //Inicializa valores del diccionario
            Application.Current.PageAppearing += CurrentPageAppearing;
            RestService = new RestService();
            DataService = new DataService();
            
            instance = this;
        }

        private async void CurrentPageAppearing(object sender, Page e)
        {
            try
            {
                Url = Application.Current.Resources["UrlAPI"].ToString();
                Prefix = Application.Current.Resources["UrlPrefix"].ToString();
                ControllerLogin = Application.Current.Resources["UrlLoginController"].ToString();
                ControllerSucursal = Application.Current.Resources["UrlSucursalController"].ToString();
                ControllerCercanaSucursal = Application.Current.Resources["UrlSucursalCercanaController"].ToString();
                ControllerMaestros = Application.Current.Resources["UrlMaestrosController"].ToString();
                AgendaInspeccion = Application.Current.Resources["AgendaInspeccion"].ToString();
                AgendaInspeccionEncabezado = Application.Current.Resources["AgendaInspeccionEncabezado"].ToString();
                AgendaInspeccionRechazo = Application.Current.Resources["AgendaInspeccionRechazo"].ToString();
                AgendaInspeccionCuerpo = Application.Current.Resources["AgendaInspeccionCuerpo"].ToString();
                AgendaInspeccionEnvioFoto = Application.Current.Resources["AgendaInspeccionEnvioFoto"].ToString();
                EnvioInspeccionEncabezado = Application.Current.Resources["EnvioInspeccionEncabezado"].ToString();
                EnvioInspeccionCuerpo = Application.Current.Resources["EnvioInspeccionCuerpo"].ToString();
                RecuperaPass = Application.Current.Resources["RecuperaPass"].ToString();
                EnvioFotoInspeccion = Application.Current.Resources["EnvioFotoInspeccion"].ToString();

                await DataService.OpenOrCreateDB();

                Application.Current.PageAppearing -= CurrentPageAppearing;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"~(>'.')> Error Inicializar valores en MVM{ex.Message}");
            }
        }

        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }


        public void SaveProgress()
        {
            var currentData = JsonConvert.SerializeObject(Inspeccion.AreasInspeccion);
            var currentIns = Inspeccion.CurrentInspeccion.SOINS_ID;
        }


    }
}
