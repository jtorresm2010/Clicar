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
    public partial class ConfirmarClavePopup : PopupPage
    {
        public ConfirmarClavePopup()
        {
            InitializeComponent();
        }

        private void ShowPass(object obj, EventArgs e)
        {
            Password.IsPassword = Password.IsPassword ? false : true;

            if (Password.IsPassword)
            {
                eye_icon.Source = ImageSource.FromFile("eye_faded.png");
            }
            else
            {
                eye_icon.Source = ImageSource.FromFile("eye_regular.png");
            }
        }

        private async void Cancel(object obj, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PopAsync();
        }


    }
}