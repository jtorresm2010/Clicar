using Clicar.ViewModels;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspeccionPopupView : PopupPage
    {
        MainViewModel MainInstance;

        public InspeccionPopupView()
        {
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            InitializeComponent();
            ButtonWorking = false;
            MainInstance = MainViewModel.GetInstance();
        }

        private async void CancelarCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PopAsync();
        }

        public bool ButtonWorking { get; set; }
        private async void ContinuarCommand(object sender, EventArgs e)
        {
            if (ButtonWorking)
                return;
            ButtonWorking = true;

            var mech = MainInstance.DetalleInspeccion.MechanicTransmission;
            var auto = MainInstance.DetalleInspeccion.AutomaticTransmission;

            if((!MainInstance.DetalleInspeccion.CurrentInspeccion.SOINS_VIN.ToString().Equals(VinEntry.Text)))
            {
                Debug.WriteLine($"~(>'.')> error de vin");
                ButtonWorking = false;
                return;
            }
            else
            {

                if (MainInstance.DetalleInspeccion.CurrentInspeccion.SOINS_TRANSMISION.Equals(auto))
                {

                    MainInstance.Inspeccion.CurrentInspeccion = MainInstance.DetalleInspeccion.CurrentInspeccion;
                    //MainInstance.Inspeccion.CrearListaCompuesta();
                    var popup = PopupNavigation.Instance;


                    Navigation.PushAsync(new InspeccionView());
                    Navigation.RemovePage(Navigation.NavigationStack[1]);

                    await popup.PopAsync();


                }
                else
                {
                    Debug.WriteLine($"~(>'.')> error de transmission");
                    ButtonWorking = false;
                    return;
                }






            }





            ButtonWorking = false;
        }
    }
}