using Clicar.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportePreliminarView : ContentPage
    {
        public ReportePreliminarView()
        {
            InitializeComponent();
            TitleImage.Margin = Funciones.SetTitleMargin(TitleImage.WidthRequest);
        }

        private void VerResumenCommand(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResumenInspeccion());
        }
    }
}