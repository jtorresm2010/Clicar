using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clicar.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaView : ContentPage
    {
        public AgendaView()
        {
            InitializeComponent();
            var vehiculosList = new VehiculosList();
            AgendaListView.ItemsSource = vehiculosList.GetListaVehiculos();



        }

        
        private void AgendaItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new DetalleInspeccionView());
            Console.WriteLine("Test list item tap");
        }

        private async void LogOutCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new LogOutPopupView());
        }
    }
}