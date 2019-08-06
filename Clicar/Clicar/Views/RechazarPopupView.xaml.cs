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
        public RechazarPopupView()
        {
            InitializeComponent();
        }

        private async void ConfirmarCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await Navigation.PopToRootAsync();
            await popup.PopAsync();
        }

    }
}