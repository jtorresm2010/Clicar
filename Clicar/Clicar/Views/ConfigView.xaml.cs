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
    public partial class ConfigView : ContentPage
    {
        public ConfigView()
        {
            InitializeComponent();
            instance = this;
        }

        private static ConfigView instance;
        public static ConfigView GetInstance()
        {
            if (instance == null)
            {
                instance = new ConfigView();
            }
            return instance;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}