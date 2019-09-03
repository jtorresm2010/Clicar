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

        public string SelectedSucursal { get; set; }

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
            Debug.WriteLine("~(>'.')> Binding Test");
        }


    }
}
