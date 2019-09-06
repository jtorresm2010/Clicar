using Clicar.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspeccionPopupView : PopupPage
    {
        MainViewModel MainInstance;

        public InspeccionPopupView()
        {
            InitializeComponent();
            MainInstance = MainViewModel.GetInstance();
        }

        private async void CancelarCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PopAsync();
        }

        private async void ContinuarCommand(object sender, EventArgs e)
        {
            MainInstance.Inspeccion = new InspeccionViewModel();

            MainInstance.Inspeccion.CurrentInspeccion = MainInstance.DetalleInspeccion.CurrentInspeccion;

            await Navigation.PushAsync(new InspeccionView());

            Navigation.RemovePage(Navigation.NavigationStack[1]);
            var popup = PopupNavigation.Instance;
            await popup.PopAsync();
        }
    }
}