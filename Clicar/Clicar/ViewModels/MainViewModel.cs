using System;
using System.Diagnostics;
using Xamarin.Forms;


namespace Clicar.ViewModels
{
    public class MainViewModel
    {
        public string Token { get; set; }
        public string Url { get; set; }
        public string Prefix { get; set; }
        public string ControllerLogin { get; set; }
        public string ControllerSucursal { get; set; }
        public string ControllerMaestros { get; set; }


        #region Instancias VM
        public LoginViewModel Login { get; set; }
        public ConfigViewModel Config { get; set; }
        public AgendaViewModel Agenda { get; set; }
        public DetalleInspeccionViewModel DetalleInspeccion { get; set; }
        public InspeccionViewModel Inspeccion { get; set; }

        #endregion

        public MainViewModel()
        {
            //Inicializa valores del diccionario
            Application.Current.PageAppearing += CurrentPageAppearing;

            instance = this;
        }

        private void CurrentPageAppearing(object sender, Page e)
        {
            try
            {
                Url = Application.Current.Resources["UrlAPI"].ToString();
                Prefix = Application.Current.Resources["UrlPrefix"].ToString();
                ControllerLogin = Application.Current.Resources["UrlLoginController"].ToString();
                ControllerSucursal = Application.Current.Resources["UrlSucursalController"].ToString();
                ControllerMaestros = Application.Current.Resources["UrlMaestrosController"].ToString();
                Application.Current.PageAppearing -= CurrentPageAppearing;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>'.')>  " + ex.Message);
            }
        }

        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }

    }
}
