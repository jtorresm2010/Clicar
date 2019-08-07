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
           
            Frame layout = (Frame)sender;  //Frame principal


            StackLayout labelParent= (StackLayout)layout.Content; //Stacklayout que contiene el numero
            var labelParentChildren = labelParent.Children;
            
            var textFrame = (Frame)labelParentChildren[0]; //Frame que contiene el label con el numero
            Label text = (Label)textFrame.Content; //Label con el numero

            
            StackLayout parent = (StackLayout)layout.Parent; //StackLayout padre del frame Tapped
            var Children = parent.Children;  //Lista de elementos dentro del Stacklayout



            Children[1].IsVisible = Children[1].IsVisible ? false : true;

            if (layout.BackgroundColor.Equals(Application.Current.Resources["BaseGreen"]))
            {
                text.TextColor= (Color)Application.Current.Resources["BaseOrange"];
                layout.BackgroundColor = (Color)Application.Current.Resources["BaseOrange"];
            }
            else
            {
                text.TextColor = (Color)Application.Current.Resources["BaseGreen"];
                layout.BackgroundColor = (Color)Application.Current.Resources["BaseGreen"];
            }

        }
    }
}