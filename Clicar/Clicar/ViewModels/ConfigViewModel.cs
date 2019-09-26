﻿using Clicar.Helpers;
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

        private bool isReady;

        public bool IsReady
        {
            get { return isReady; }
            set { SetValue(ref isReady, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals("SelectedSucursal"))
                ClosestSucursal = SelectedSucursal;
        }

        MainViewModel MainInstance;
        public ConfigViewModel()
        {
            isReady = false;
            MainInstance = MainViewModel.GetInstance();

            ClosestSucursal = new SucursalDB
            {
                MAESU_NOMBRE_SUCURSAL = ". . ."
            };

        }

        public ICommand ConfigICommand
        {
            get
            {
                return new RelayCommand(ConfigCommand, () => IsReady);
            }
        }

        private void ConfigCommand()
        {
            MainInstance.Agenda.CurrentSucursal = 
                ClosestSucursal != SelectedSucursal && SelectedSucursal != null? 
                SelectedSucursal : 
                ClosestSucursal;

            MainInstance.Agenda.LoadMainList();
            Application.Current.MainPage = new NavigationPage(new AgendaView());
        }
    }
}
