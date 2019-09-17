using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertPopup : PopupPage
    {
        public AlertPopup()
        {
            InitializeComponent();
        }

        public AlertPopup(string Titulo, string Texto, string Boton)
        {
            InitializeComponent();
            Title.Text = Titulo;
            Details.Text = Texto;
            Button.Text = Boton;
        }

        private async void DismissCommand(object o, EventArgs e)
        {
            var popup = PopupNavigation.Instance;
            await popup.PopAsync();
        }
    }
}