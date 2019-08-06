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
    public partial class InspeccionPopupView : PopupPage
    {
        public InspeccionPopupView()
        {
            InitializeComponent();
        }

        private async void CancelarCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PopAsync();
        }

        private async void ContinuarCommand(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InspeccionView());
            var popup = PopupNavigation.Instance;
            await popup.PopAsync();
        }
    }
}