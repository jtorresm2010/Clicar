using Clicar.Models;
using Clicar.ViewModels;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Rg.Plugins.Popup.Contracts;
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
        IPopupNavigation PopupInstance;
        MainViewModel MainInstance;
        public RechazarPopupView()
        {

            PopupInstance = PopupNavigation.Instance;
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
            await Navigation.PopToRootAsync();
            await PopupInstance.PopAsync();
            ButtonWorking = false;
        }


    }
}