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

        private SucursalDB selectedSucursal;
        public SucursalDB SelectedSucursal
        {
            get { return selectedSucursal; }
            set { SetValue(ref selectedSucursal, value); }
        }

        private SucursalDB configuredSucursal;
        public SucursalDB ClosestSucursal
        {
            get { return configuredSucursal; }
            set { SetValue(ref configuredSucursal, value); }
        }

        private ObservableCollection<SucursalDB> ListaSucursales;
        public ObservableCollection<SucursalDB> Sucursales
        {
            get { return this.ListaSucursales; }
            set { this.SetValue(ref this.ListaSucursales, value); } 
        }


        MainViewModel MainInstance;
        public ConfigViewModel()
        {
            MainInstance = MainViewModel.GetInstance();
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
            MainInstance.Agenda.CurrentSucursal = 
                ClosestSucursal != SelectedSucursal && SelectedSucursal != null? 
                SelectedSucursal : 
                ClosestSucursal;

            Application.Current.MainPage = new NavigationPage(new AgendaView());
        }
    }
}
