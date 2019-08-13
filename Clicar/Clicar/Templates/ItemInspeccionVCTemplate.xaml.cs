using Clicar.ViewModels;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemInspeccionVCTemplate : ViewCell
    {

        private Color baseGreyLight;
        private Color baseOrange;
        private Color baseGreen;
        public ItemInspeccionVCTemplate()
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

    }
}