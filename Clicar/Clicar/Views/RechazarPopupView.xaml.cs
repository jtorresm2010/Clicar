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
        public RechazarPopupView()
        {
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            InitializeComponent();
            MainInstance = MainViewModel.GetInstance();
            ButtonWorking = false;
        }
        public bool ButtonWorking { get; set; }
        private async void ConfirmarCommand(object sender, EventArgs e)
        {
            if (ButtonWorking)
                return;
            ButtonWorking = true;

            MainInstance.Agenda.RechazarInspeccion(MainInstance.DetalleInspeccion.CurrentInspeccion);
            var popup = PopupNavigation.Instance;
            await Navigation.PopToRootAsync();
            await popup.PopAsync();
            ButtonWorking = false;
        }

    }
}