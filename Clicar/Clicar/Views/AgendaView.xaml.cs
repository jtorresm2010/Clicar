using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Clicar.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

            TitleImage.Margin = Funciones.SetTitleMargin(TitleImage.WidthRequest, RefreshImages.WidthRequest);


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
        private void InicializarLista()
        {
            listaVehiculos = new VehiculosList().GetListaVehiculos();

            lastItem = listaVehiculos.Count;
            PendientesListView.ItemsSource = listaVehiculos;
            PendientesHeight = listaVehiculos.Count * PendientesListView.RowHeight;
            PendientesListView.HeightRequest = PendientesHeight;
            PendientesListView.ItemAppearing += ListViewOnItemAppearing;

            lastItem = listaVehiculos.Count; //esta deberia ser la segunda lista, pero en este momento ambos lv usan la misma lista
            CompletadosListView.ItemsSource = listaVehiculos;
            CompletadosHeight = listaVehiculos.Count * CompletadosListView.RowHeight;
            CompletadosListView.HeightRequest = CompletadosHeight;
            CompletadosListView.ItemAppearing += ListViewOnItemAppearing;
        }
        public int lastItem { get; set; }
        public bool IsLast { get; set; }
        private void ListViewOnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var list = (ListView)sender;
            if (e.ItemIndex == (lastItem-2))
            {
                IsLast = true;
                list.ItemAppearing -= ListViewOnItemAppearing;
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
            if((Frame)sender == ListaPendientesFrame)
            {
                CloseOpenAnimation(PendientesListView, PendientesHeight, PendientesListView.IsVisible);
            }
            else
            {
                CloseOpenAnimation(CompletadosListView, CompletadosHeight, CompletadosListView.IsVisible);
            }
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
        }
    }
}