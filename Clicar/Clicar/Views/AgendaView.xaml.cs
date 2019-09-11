                                                                                                                                 using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Clicar.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clicar.Utils;
using Clicar.ViewModels;
using System.Diagnostics;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaView : ContentPage
    {
        public AgendaView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void LogOutCommand(object sender, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new LogOutPopupView());
        }
    }
}