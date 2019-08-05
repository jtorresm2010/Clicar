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
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }
        public void ShowPass(object sender, EventArgs args) //Intercambia el entry de contraseña entre texto visible y protegido
        {
            Password.IsPassword = Password.IsPassword ? false : true;


            if (Password.IsPassword)
            {
                eye_icon.Source = ImageSource.FromFile("eye_regular.jpg");
            }
            else
            {
                eye_icon.Source = ImageSource.FromFile("eye_faded.jpg");
            }

        }


        private async void PasswordRecoveryCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new PasswordRecoveryPopup());
        }



        private async void FingerprintCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new FingerPrintPopupView());
        }
        
        private void LoginCommand(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ConfigView();
        }
    }
}