using Clicar.Helpers;
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


        public LoginViewModel()
        {
            restService = new RestService();
        }

        private static LoginViewModel m_Instancia;

        public static LoginViewModel Instancia
        {
            get
            {
                if (m_Instancia == null)
                {
                    m_Instancia = new LoginViewModel();
                }

                return m_Instancia;
            }
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
        private async void LoginCommand()
        {
            var connection = restService.CheckConnection();

            Debug.WriteLine("~(>'.')> " + Usuario);
            Debug.WriteLine("~(>'.')> " + Clave);

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

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlLoginController"].ToString();

            var response = await restService.Login<LoginResponse>(usuario, url, prefix, controller);

            try
            {
                var resp = (LoginResponse)response.Result;

                //if(resp == null)
                //{
                //    Debug.WriteLine("~(>'.')> Respuesta nula");
                //}

                if (resp.Resultado)
                {
                    Debug.WriteLine("~(>^.^)> " + resp.Mensaje);
                    Application.Current.MainPage = new ConfigView();
                }


                
            }
            catch (Exception)
            {
                Debug.WriteLine("~(>-_-)> Datos erroneos");
            }
        }
    }
}
