﻿using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Clicar.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Clicar.Utils;
using Clicar.ViewModels;
using System.Diagnostics;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaView : ContentPage
    {
        public AgendaView()
        {
            InitializeComponent();
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private bool ActionBusy = false;
        private async void LogOutCommand(object sender, EventArgs e)
        {
            if (ActionBusy)
                return;

            ActionBusy = true;


            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new LogOutPopupView());

            ActionBusy = false;
        }
    }
}