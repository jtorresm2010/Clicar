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
            var vehiculos = new VehiculosList();
            var listaVehiculos = vehiculos.GetListaVehiculos();
            PendientesListView.ItemsSource = listaVehiculos;
            CompletadosListView.ItemsSource = listaVehiculos;
            //int itemHeight = PendientesListView.RowHeight;
            //int listCount = listaVehiculos.Count();
            //PendientesListView.HeightRequest = itemHeight * listCount;


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

        private void ToggleVisible(object sender, EventArgs e)
        {

            PendientesListView.IsVisible = PendientesListView.IsVisible ? false : true;
            CompletadosListView.IsVisible = CompletadosListView.IsVisible ? false : true;

        }

        private void ToggleVisibleComp(object sender, EventArgs e)
        {
            CompletadosListView.IsVisible = CompletadosListView.IsVisible ? false : true;
            PendientesListView.IsVisible = PendientesListView.IsVisible ? false : true;

        }


    }
}