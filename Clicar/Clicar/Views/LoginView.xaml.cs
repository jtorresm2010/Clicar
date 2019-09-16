﻿using Clicar.ViewModels;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Plugin.Fingerprint;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {

        MainViewModel MainInstance;
        public LoginView()
        {
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            InitializeComponent();
            MainInstance = MainViewModel.GetInstance();
        }
        public void ShowPass(object sender, EventArgs args) //Intercambia el entry de contraseña entre texto visible y protegido
        {
            Password.IsPassword = Password.IsPassword ? false : true;


            if (Password.IsPassword)
            {
                eye_icon.Source = ImageSource.FromFile("eye_faded.png");
            }
            else
            {
                eye_icon.Source = ImageSource.FromFile("eye_regular.png");
            }

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void PasswordRecoveryCommand(object sender, EventArgs e)
        {
            if (!MainInstance.Login.IsIdle)
                return;

            UsuarioEntry.FontFamily = "OpenSans-Bold";
            Console.WriteLine("Fuente actual: " + UsuarioEntry.FontFamily.ToString());
            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new PasswordRecoveryPopup());
        }


    }
}