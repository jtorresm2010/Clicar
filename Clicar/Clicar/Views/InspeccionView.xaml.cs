using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspeccionView : ContentPage
    {

        private Color baseGreyLight;
        private Color baseOrange;
        private Color baseGreen;

        public InspeccionView()
        {
            InitializeComponent();
            baseGreyLight = (Color)Application.Current.Resources["BaseGreyLight"];
            baseOrange = (Color)Application.Current.Resources["BaseOrange"];
            baseGreen = (Color)Application.Current.Resources["BaseGreen"];
        }

        private void SwitchStateCommand(object sender, EventArgs e)
        {
            var layout = (StackLayout)sender;

            var Children = layout.Children;

            Label label = (Label)Children[1];
            Image image = (Image)Children[0];


            if (label.TextColor.Equals(baseGreyLight))
            {
                label.TextColor = baseOrange;
                label.Text = "Bloqueado";
                image.Source = ImageSource.FromFile("lock_solid_orange.png");

            }
            else
            {
                label.TextColor = baseGreyLight;
                label.Text = "Bloquear";
                image.Source = ImageSource.FromFile("lock_solid_grey.png");
            }
        }

        private void PrimerItemCommand(object sender, EventArgs e)
        {
            primer.IsOpen = false;
            segundo.IsOpen = true;
            primer.ButtonBackgroundColor = baseGreen;
            //Agregar cambio al color de la linea
        }

        private void SegundoItemBackCommand(object sender, EventArgs e)
        {
            primer.IsOpen = true;
            segundo.IsOpen = false;
        }

        
        private void EditarCommand(object sender , EventArgs e)
        {
            Console.WriteLine("sdlkfakshdflasdhfaks");
        }


    }
}