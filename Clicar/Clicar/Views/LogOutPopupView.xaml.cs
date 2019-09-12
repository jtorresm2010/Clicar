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
    public partial class LogOutPopupView : PopupPage
    {
        IPopupNavigation PopupInstance;
        public LogOutPopupView()
        {
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            InitializeComponent();
            PopupInstance = PopupNavigation.Instance;
        }


        public bool ButtonWorking { get; set; }

        private async void LogoutCommand(object sender, EventArgs e)
        {

            if (ButtonWorking)
                return;
            ButtonWorking = true;



            var popup = PopupNavigation.Instance;
            await Navigation.PopToRootAsync();
            await popup.PopAsync();

            ButtonWorking = true;
            Application.Current.MainPage = new LoginView();


        }


        private async void CancelCommand(object sender, EventArgs e)
        {
            await PopupInstance.PopAsync();
        }

    }
}