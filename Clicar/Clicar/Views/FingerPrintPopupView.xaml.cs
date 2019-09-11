using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Plugin.Fingerprint;
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
    public partial class FingerPrintPopupView : PopupPage
    {
        public FingerPrintPopupView()
        {
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var result = await CrossFingerprint.Current.AuthenticateAsync("Valide su huella para continuar");
            if (result.Authenticated)
            {
                // do secret stuff :)
                var popup = PopupNavigation.Instance;
                await popup.PopAsync();
            }
            else
            {
                // not allowed to do secret stuff :(
            }
        }
        private async void FingerprintCancel(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PopAsync();
        }


    }
}