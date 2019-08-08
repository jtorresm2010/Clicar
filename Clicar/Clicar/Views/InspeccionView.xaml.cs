using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

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

        #region Comando cambio de color e icono 'Bloquear'
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
        #endregion cambio de color

        private void PrimerItemCommand(object sender, EventArgs e)
        {
            PrimerItem.ClosePanel();
            SegundoItem.OpenPanel();

            PrimerItem.ButtonBackgroundColor = baseGreen;
            PrimerItem.BorderColor = baseGreen;
        }


        private void SegundoItemCommand(object sender, EventArgs e)
        {
            SegundoItem.ClosePanel();
            TercerItem.OpenPanel();

            SegundoItem.ButtonBackgroundColor = baseGreen;
            SegundoItem.BorderColor = baseGreen;
        }
        private void SegundoItemBackCommand(object sender, EventArgs e)
        {
            PrimerItem.OpenPanel();
            SegundoItem.ClosePanel();
        }


        private void TercerItemCommand(object sender, EventArgs e)
        {
            TercerItem.ClosePanel();
            CuartoItem.OpenPanel();

            TercerItem.ButtonBackgroundColor = baseGreen;
            TercerItem.BorderColor = baseGreen;
        }
        private void TercerItemBackCommand(object sender, EventArgs e)
        {
            SegundoItem.OpenPanel();
            TercerItem.ClosePanel();
        }


        private void CuartoItemCommand(object sender, EventArgs e)
        {
            CuartoItem.ClosePanel();
            QuintoItem.OpenPanel();

            CuartoItem.ButtonBackgroundColor = baseGreen;
            CuartoItem.BorderColor = baseGreen;
        }
        private void CuartoItemBackCommand(object sender, EventArgs e)
        {
            TercerItem.OpenPanel();
            CuartoItem.ClosePanel();
        }


        private void QuintoItemCommand(object sender, EventArgs e)
        {
            QuintoItem.ClosePanel();
            SextoItem.OpenPanel();

            QuintoItem.ButtonBackgroundColor = baseGreen;
            QuintoItem.BorderColor = baseGreen;
        }
        private void QuintoItemBackCommand(object sender, EventArgs e)
        {
            CuartoItem.OpenPanel();
            QuintoItem.ClosePanel();
        }


        private void SextoItemCommand(object sender, EventArgs e)
        {
            SextoItem.ClosePanel();
            SeptimoItem.OpenPanel();

            SextoItem.ButtonBackgroundColor = baseGreen;
            SextoItem.BorderColor = baseGreen;
        }
        private void SextoItemBackCommand(object sender, EventArgs e)
        {
            QuintoItem.OpenPanel();
            SextoItem.ClosePanel();
        }


        private void SeptimoItemCommand(object sender, EventArgs e)
        {
            SeptimoItem.ClosePanel();
            OctavoItem.OpenPanel();

            SeptimoItem.ButtonBackgroundColor = baseGreen;
            SeptimoItem.BorderColor = baseGreen;
        }
        private void SeptimoItemBackCommand(object sender, EventArgs e)
        {
            SextoItem.OpenPanel();
            SeptimoItem.ClosePanel();
        }


        private void OctavoItemCommand(object sender, EventArgs e)
        {
            OctavoItem.ClosePanel();
            //OctavoItem.OpenPanel();
            Console.WriteLine("Termino de inspeccion");

            OctavoItem.ButtonBackgroundColor = baseGreen;
            OctavoItem.BorderColor = baseGreen;
        }
        private void OctavoItemBackCommand(object sender, EventArgs e)
        {
            SeptimoItem.OpenPanel();
            OctavoItem.ClosePanel();
        }







        private void EditarCommand(object sender , EventArgs e)
        {
            Console.WriteLine("Comando Editar");
        }
        
        private void DetalleCommand(object sender, EventArgs e)
        {
            Console.WriteLine("Comando editar detalles");
        }

    }
}