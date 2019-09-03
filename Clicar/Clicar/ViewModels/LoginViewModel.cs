﻿using Clicar.Helpers;
using Clicar.Models;
using Clicar.Services;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clicar.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private RestService restService;
        MainViewModel MainInstance;

        public LoginViewModel()
        {
            MainInstance = MainViewModel.GetInstance();
            restService = new RestService();
            instance = this;
        }

        private static LoginViewModel instance;
        public static LoginViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginViewModel();
            }
            return instance;
        }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public ICommand LoginICommand
        {
            get
            {
                return new RelayCommand(LoginCommand);
            }
        }
        private void LoginCommand4()
        {
            Application.Current.MainPage = new ConfigView();
        }
        private async void LoginCommand()
        {
            var connection = restService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("", connection.Message, Languages.Accept);
                return;
            }

            var usuario = new Usuario
            {
                USU_USERNAME = Usuario,
                USU_CLAVE = Clave,
                ORIGEN = "mobile"
            };

            var response = await restService.PostAsync<LoginResponse>(MainInstance.Url, MainInstance.Prefix, MainInstance.ControllerLogin, usuario);

            try
            {
                var resp = (LoginResponse)response.Result;

                if (resp.Resultado)
                {
                    MainInstance.Token = resp.Mensaje;
                    MainInstance.Config = new ConfigViewModel();
                    Application.Current.MainPage = new ConfigView();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Datos erroneos" + ex.Message);
            }


        }
    }
}
