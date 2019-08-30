using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
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
           
            Navigation.RemovePage(Navigation.NavigationStack[1]);
            var popup = PopupNavigation.Instance;
            await popup.PopAsync();
        }
    }
}