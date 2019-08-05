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
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }
        public void ShowPass(object sender, EventArgs args) //Intercambia el entry de contraseña entre texto visible y protegido
        {
            Password.IsPassword = Password.IsPassword ? false : true;


            if (Password.IsPassword)
            {
                eye_icon.Source = ImageSource.FromFile("eye_regular.jpg");
            }
            else
            {
                eye_icon.Source = ImageSource.FromFile("eye_faded.jpg");
            }

        }
    }
}