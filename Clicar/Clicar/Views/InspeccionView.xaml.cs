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


            MainInstance.Inspeccion.CrearListaCompuesta();
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