using Clicar.ViewModels;
using Clicar.Views;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Login = new LoginViewModel();

            MainPage = new LoginView();
            //MainPage = new ConfigView();
            MainPage = new NavigationPage(new InspeccionView());






        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
