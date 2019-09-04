using Clicar.Helpers;
using Clicar.Models;
using Clicar.Services;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clicar.ViewModels
{
    public class ConfigViewModel : BaseViewModel
    {

        private Sucursal selectedSucursal;
        public Sucursal SelectedSucursal
        {
            get { return selectedSucursal; }
            set { SetValue(ref selectedSucursal, value); }
        }

        private Sucursal configuredSucursal;
        public Sucursal ConfiguredSucursal
        {
            get { return configuredSucursal; }
            set { SetValue(ref configuredSucursal, value); }
        }

        private ObservableCollection<Sucursal> ListaSucursales;
        public ObservableCollection<Sucursal> Sucursales
        {
            get { return this.ListaSucursales; }
            set { this.SetValue(ref this.ListaSucursales, value); } 
        }


        MainViewModel MainInstance;
        private RestService restService;
        public ConfigViewModel()
        {
            restService = new RestService();
            instance = this;
            MainInstance = MainViewModel.GetInstance();

            GetListSucursales();
        }


        private static ConfigViewModel instance;
        public static ConfigViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new ConfigViewModel();
            }
            return instance;
        }


        private async void GetListSucursales()
        {
            var connection = restService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("", connection.Message, Languages.Accept);
                return;
            }

            var response = await restService.GetAsync<SucursalesResponse>(
                MainInstance.Url,
                MainInstance.Prefix,
                MainInstance.ControllerSucursal,
                null,
                MainInstance.Token);

            try
            {
                SucursalesResponse resp = (SucursalesResponse)response.Result;

                if (resp.Resultado)
                {
                    Sucursales = new ObservableCollection<Sucursal>(resp.Elemento);
                    ConfiguredSucursal = Sucursales[0]; //Se cambia por la sucursal almacenada en la config local

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error" + ex.Message);
            }
        }

        public ICommand ConfigICommand
        {
            get
            {
                return new RelayCommand(ConfigCommand);
            }
        }

        private void ConfigCommand()
        {
            MainInstance.Agenda = new AgendaViewModel();
            Application.Current.MainPage = new NavigationPage(new AgendaView());
        }


    }
}
