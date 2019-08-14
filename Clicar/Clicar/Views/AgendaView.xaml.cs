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
            PendientesListView.HeightRequest = listaVehiculos.Count * PendientesListView.RowHeight;



            CompletadosListView.ItemsSource = listaVehiculos;
            CompletadosListView.HeightRequest = listaVehiculos.Count * CompletadosListView.RowHeight;
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

        private async void RefreshCommand(object sender, EventArgs e)
        {
            var parent = (Grid)sender;
            var imageF = (Image)parent.Children[0];
            var imageB = (Image)parent.Children[1];

            uint intervalo = 300;

            parent.IsEnabled = false;

            await Task.WhenAll(
                imageF.FadeTo(0, intervalo), 
                imageB.FadeTo(1, intervalo),
                imageF.RotateTo(imageF.Rotation + 90, intervalo, Easing.CubicIn),
                imageB.RotateTo(imageB.Rotation + 90, intervalo, Easing.CubicIn)
                );

            await Task.WhenAll(
                imageF.FadeTo(1, intervalo),
                imageB.FadeTo(0, intervalo),
                imageF.RotateTo(imageF.Rotation + 90, intervalo, Easing.CubicOut),
                imageB.RotateTo(imageB.Rotation + 90, intervalo, Easing.CubicOut)
                );

            parent.IsEnabled = true;
        }


    }
}