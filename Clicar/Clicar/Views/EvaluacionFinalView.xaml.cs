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
    public partial class EvaluacionFinalView : ContentPage
    {
        public EvaluacionFinalView()
        {
            InitializeComponent();
        }

        private void FinalizarCommand(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResumenInspeccion());
            
        }
    }
}