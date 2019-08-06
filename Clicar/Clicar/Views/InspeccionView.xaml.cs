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
    public partial class InspeccionView : ContentPage
    {
        public InspeccionView()
        {
            InitializeComponent();
        }

        private void ToogleView(object sender, EventArgs e)
        {
            var layout = (StackLayout)sender;

            var Children = layout.Children;

            if (Children[0].GetType().ToString().Equals("Xamarin.Forms.Frame"))
            {
                Children[1].IsVisible = Children[1].IsVisible ? false : true;

            }


        }
    }
}