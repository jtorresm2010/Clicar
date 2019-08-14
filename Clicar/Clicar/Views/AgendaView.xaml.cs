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

            var listaVehiculos = new VehiculosList().GetListaVehiculos();



            PendientesListView.ItemsSource = listaVehiculos;
            PendientesListView.HeightRequest = (listaVehiculos.Count * PendientesListView.RowHeight) + Math.Round(listaVehiculos.Count * 0.3);


            CompletadosListView.ItemsSource = listaVehiculos;
            CompletadosListView.HeightRequest = (listaVehiculos.Count * CompletadosListView.RowHeight) + Math.Round(listaVehiculos.Count * 0.3);

        }

        
        private void AgendaItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new DetalleInspeccionView());
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
            ListaCompletadosFrame.TranslateTo(ListaPendientesFrame.X, ListaPendientesFrame.Y, 5000);


            //CompletadosListView.IsVisible = CompletadosListView.IsVisible ? false : true;
            //PendientesListView.IsVisible = PendientesListView.IsVisible ? false : true;

        }

        private async void RefreshCommand(object sender, EventArgs e)
        {
            var parent = (Grid)sender;
            var imageF = (Image)parent.Children[0];
            var imageB = (Image)parent.Children[1];

            uint intervalo = 300;

            parent.IsEnabled = false;
            MainScrollView.IsEnabled = false;

            await Task.WhenAll(
                MainScrollView.FadeTo(0.5, intervalo),
                imageF.FadeTo(0, intervalo, Easing.CubicIn),
                imageB.FadeTo(1, intervalo, Easing.CubicIn),
                imageF.RotateTo(imageF.Rotation + 90, intervalo, Easing.CubicIn),
                imageB.RotateTo(imageB.Rotation + 90, intervalo, Easing.CubicIn)
                );

            await Task.WhenAll(
                MainScrollView.FadeTo(1, intervalo),
                imageF.FadeTo(1, intervalo, Easing.CubicIn),
                imageB.FadeTo(0, intervalo, Easing.CubicIn),
                imageF.RotateTo(imageF.Rotation + 90, intervalo, Easing.CubicOut),
                imageB.RotateTo(imageB.Rotation + 90, intervalo, Easing.CubicOut)
                );
            
            MainScrollView.IsEnabled = true;
            parent.IsEnabled = true;
        }

        //Test deanimacion para carga continua
        private async void RefreshAsync(object sender, EventArgs e)
        {
            var parent = (Grid)sender;
            var imageF = (Image)parent.Children[0];
            var imageB = (Image)parent.Children[1];

            uint intervalo = 600;

            parent.IsEnabled = false;
            MainScrollView.IsEnabled = false;

            await Task.WhenAll(
                MainScrollView.FadeTo(0.5, intervalo/2),
                imageF.FadeTo(0, intervalo, Easing.SinIn),
                imageB.FadeTo(1, intervalo, Easing.SinIn),
                imageF.RotateTo(imageF.Rotation + 180, intervalo, Easing.Linear),
                imageB.RotateTo(imageB.Rotation + 180, intervalo, Easing.Linear)
                );

            int i = 0;
            var loading = true;

            while (loading)
            {

            await Task.WhenAll(
                imageF.RotateTo(imageF.Rotation + 180, intervalo, Easing.Linear),
                imageB.RotateTo(imageB.Rotation + 180, intervalo, Easing.Linear)
                );

                i++;

                if(i > 5)
                {
                    loading = false;
                }
            }

            await Task.WhenAll(
                MainScrollView.FadeTo(1, intervalo),
                imageF.FadeTo(1, intervalo),
                imageB.FadeTo(0, intervalo),
                imageF.RotateTo(imageF.Rotation + 180, intervalo, Easing.Linear),
                imageB.RotateTo(imageB.Rotation + 180, intervalo, Easing.Linear)
                );

            MainScrollView.IsEnabled = true;
            parent.IsEnabled = true;
        }
    }
}