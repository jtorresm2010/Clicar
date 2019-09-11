using Clicar.Models;
using Clicar.ViewModels;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RechazarPopupView : PopupPage
    {
        MainViewModel MainInstance;
        //private Inspeccion rechazarInspeccion;

        //public Inspeccion RechazarInspeccion
        //{
        //    get { return rechazarInspeccion; }
        //    set { SetValue(ref rechazarInspeccion, value); }
        //}

        public RechazarPopupView()
        {
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            InitializeComponent();
            MainInstance = MainViewModel.GetInstance();
            
        }

        private async void ConfirmarCommand(object sender, EventArgs e)
        {
            MainInstance.Agenda.RechazarInspeccion(MainInstance.DetalleInspeccion.CurrentInspeccion);
            var popup = PopupNavigation.Instance;
            await Navigation.PopToRootAsync();
            await popup.PopAsync();
        }

    }
}