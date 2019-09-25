using Clicar.Templates;
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

            var popup = PopupNavigation.Instance;


            var mech = MainInstance.DetalleInspeccion.MechanicTransmission;
            var auto = MainInstance.DetalleInspeccion.AutomaticTransmission;

            var vinText = VinEntry.Text ?? "";

            if (vinText.Length == 0)
            {
                await popup.PushAsync(new AlertPopup("Error de autenticación", "Ingrese un valor de VIN", "Continuar"));
                ButtonWorking = false;
                return;
            }

            if (!MainInstance.DetalleInspeccion.CurrentInspeccion.SOINS_VIN.Equals(vinText))
            {
                await popup.PushAsync(new AlertPopup("Error de autenticación", "VIN Incorrecto", "Continuar"));
                ButtonWorking = false;
                return;
            }

            if (MainInstance.DetalleInspeccion.CurrentInspeccion.SOINS_TRANSMISION  != null)
            {
                if ((bool)MainInstance.DetalleInspeccion.CurrentInspeccion.SOINS_TRANSMISION && (bool)auto)
                {
                    await popup.PushAsync(new AlertPopup("Error de autenticación", "Transmision incorrecta", "Continuar"));
                    Debug.WriteLine("Transmision incorrecta");
                    ButtonWorking = false;
                    return;
                }

                if (!(bool)MainInstance.DetalleInspeccion.CurrentInspeccion.SOINS_TRANSMISION && mech)
                {
                    await popup.PushAsync(new AlertPopup("Error de autenticación", "Transmision incorrecta", "Continuar"));
                    Debug.WriteLine("Transmision incorrecta");
                    ButtonWorking = false;
                    return;
                }
            }

            MainInstance.Inspeccion.CurrentInspeccion = MainInstance.DetalleInspeccion.CurrentInspeccion;

            MainInstance.Inspeccion.HoraInicio = DateTime.Now;

            //var popup = PopupNavigation.Instance;


            await Navigation.PushAsync(new InspeccionView());
            Navigation.RemovePage(Navigation.NavigationStack[1]);

            await popup.PopAsync();

            ButtonWorking = false;
        }
    }
}