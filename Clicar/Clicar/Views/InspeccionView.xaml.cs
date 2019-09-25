using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CustomControls;
using Xamarin.Forms;
using Clicar.Models;
using Xamarin.Forms.Xaml;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Clicar.ViewModels;
using Clicar.Utils;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspeccionView : ContentPage
    {
        private bool _canClose = true;
        MainViewModel MainInstance;
        public InspeccionView()
        {
            MainInstance = MainViewModel.GetInstance();
            InitializeComponent();

            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            instance = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadItemsList();
        }

        private async void LoadItemsList()
        {
            await Task.Delay(500);

            MainInstance.Inspeccion.CrearListaCompuesta();
        }


        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();

            if (_canClose)
            {
                ShowExitDialog();
            }
            return _canClose;
        }



        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert("Confirmar", "¿Hay una inspección en curso, desea volver atras?", "Si", "No");
            if (answer)
            {
                _canClose = false;
                OnBackButtonPressed();
            }
        }



        private static InspeccionView instance;
        public static InspeccionView GetInstance()
        {
            if (instance == null)
            {
                instance = new InspeccionView();
            }
            return instance;
        }
    }
}