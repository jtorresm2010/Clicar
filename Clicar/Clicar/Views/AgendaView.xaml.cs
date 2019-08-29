using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clicar.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clicar.Templates;
using System.Diagnostics;
using Xamarin.Essentials;
using Clicar.Utils;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaView : ContentPage
    {


        List<Vehiculo> listaVehiculos;

        private double CompletadosHeight;
        private double PendientesHeight;
        public AgendaView()
        {
            InitializeComponent();

            TitleImage.Margin = Funciones.SetTitleMargin(TitleImage, TitleImage.WidthRequest, RefreshImages.WidthRequest);


            instance = this;
        }
        private static AgendaView instance;
        public static AgendaView GetInstance()
        {
            if (instance == null)
            {
                instance = new AgendaView();
            }
            return instance;
        }





        protected override void OnAppearing()
        {
            base.OnAppearing();
            InicializarLista();



        }

        private void SetTitleMargin(View view)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenwidth = mainDisplayInfo.Width / mainDisplayInfo.Density;

            var margin = (screenwidth / 2) - 100;

            view.Margin = new Thickness(0, 0, margin, 0);
        }

        private void InicializarLista()
        {
            listaVehiculos = new VehiculosList().GetListaVehiculos();

            lastItem = listaVehiculos.Count;
            PendientesListView.ItemsSource = listaVehiculos;
            PendientesHeight = listaVehiculos.Count * PendientesListView.RowHeight;
            PendientesListView.HeightRequest = PendientesHeight;
            PendientesListView.ItemAppearing += PendientesListView_ItemAppearing;

            lastItem = listaVehiculos.Count; //esta deberia ser la segunda lista, pero en este momento ambos lv usan la misma lista
            CompletadosListView.ItemsSource = listaVehiculos;
            CompletadosHeight = listaVehiculos.Count * CompletadosListView.RowHeight;
            CompletadosListView.HeightRequest = CompletadosHeight;
            CompletadosListView.ItemAppearing += PendientesListView_ItemAppearing;
        }

        public int lastItem { get; set; }
        public bool IsLast { get; set; }
        private void PendientesListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var list = (ListView)sender;
            if (e.ItemIndex == (lastItem-2))
            {
                IsLast = true;
                list.ItemAppearing -= PendientesListView_ItemAppearing;
            }
            else
            {
                IsLast = false;
            }
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
            CloseOpenAnimation(PendientesListView, PendientesHeight, PendientesListView.IsVisible);
        }

        private void ToggleVisibleComp(object sender, EventArgs e)
        {
            CloseOpenAnimation(CompletadosListView, CompletadosHeight, CompletadosListView.IsVisible);
        }

        private void CloseOpenAnimation(ListView listView, double listHeight, bool IsVisible)
        {

            Action<double> callback = input => 
                {
                    if (!listView.IsVisible)
                    {
                        listView.IsVisible = true;
                    }

                    listView.HeightRequest = input;
                };
            Action<double, bool> endAction = (x, y) => { listView.IsVisible = IsVisible; };
            uint rate = 16;
            uint length = 600;
            double startingHeight = 0;
            double endingHeight = 0;
            Easing easing = Easing.Linear;


            if (IsVisible)
            {
                startingHeight = listHeight;
                endingHeight = 0;
                IsVisible = false;

            }
           else
            {
                startingHeight = 0;
                endingHeight = listHeight;
                IsVisible = true;
            }

            
            
            listView.Animate("ListSize", callback, startingHeight, endingHeight, rate, length, easing, endAction);
            //listView.IsVisible = Visible;
        }


        

        private async void RefreshCommand1(object sender, EventArgs e)
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

        //Test de animacion para carga continua
        private async void RefreshCommand(object sender, EventArgs e)
        {
            var parent = (Grid)sender;
            var imageF = (Image)parent.Children[0];
            var imageB = (Image)parent.Children[1];

            uint intervalo = 600;

            parent.IsEnabled = false;
            MainScrollView.IsEnabled = false;


            //comienza a girar
            await Task.WhenAll(
                MainScrollView.FadeTo(0.5, intervalo/2),
                imageF.FadeTo(0, intervalo, Easing.SinIn),
                imageB.FadeTo(1, intervalo, Easing.SinIn),
                imageF.RotateTo(imageF.Rotation + 180, intervalo, Easing.Linear),
                imageB.RotateTo(imageB.Rotation + 180, intervalo, Easing.Linear)
                );

            //sigue girando por los ciclos necesarios
            for (int i = 0; i < 5; i++)
            {

                await Task.WhenAll(
                    imageF.RotateTo(imageF.Rotation + 180, intervalo, Easing.Linear),
                    imageB.RotateTo(imageB.Rotation + 180, intervalo, Easing.Linear)
                    );
            }

            //termina el giro
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