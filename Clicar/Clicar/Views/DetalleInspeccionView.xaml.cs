using Clicar.Utils;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleInspeccionView : ContentPage
    {
        public DetalleInspeccionView()
        {
            InitializeComponent();
            instance = this;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();



        }



        private async void RechazarCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new RechazarPopupView());
        }

        private async void InspeccionarCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new InspeccionPopupView());
        }

        private static DetalleInspeccionView instance;
        public static DetalleInspeccionView GetInstance()
        {
            if (instance == null)
            {
                instance = new DetalleInspeccionView();
            }
            return instance;
        }
    }
}