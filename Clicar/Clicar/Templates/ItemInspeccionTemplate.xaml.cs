using Clicar.Models;
using Clicar.ViewModels;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemInspeccionTemplate : AccordionItemView
    {
        InspeccionViewModel MainInstance;
        public ItemInspeccionTemplate()
        {
            InitializeComponent();

            MainInstance = MainViewModel.GetInstance().Inspeccion;

            if(MainInstance.AreasInspeccion[MainInstance.CurrentIteration].IsImageSet)
            {
                itemsInspeccionListView.IsVisible = false;
                CreateGrid();
            }

            MainInstance.CurrentIteration++;
        }


        private void CreateGrid()
        {

            var gridRow = 0;
            var gridCol = 0;

            int i = 0;
            foreach (Fotografia foto in MainInstance.ListaImagenes)
            {


                StackLayout layout = new StackLayout();



                Image imagen = new Image
                {
                    HeightRequest = 50,
                    Aspect = Aspect.AspectFill
                };

                imagen.SetBinding(Image.SourceProperty , $"ListaImagenes[{i}].CurrentImageSmall");

                Frame frame = new Frame
                {
                    HeightRequest = 100,
                    WidthRequest = 100,
                    Padding = 0,
                    HasShadow = false,
                    CornerRadius = 5,
                    BackgroundColor = (Color)Application.Current.Resources["BaseGrey63"],
                    BorderColor = (Color)Application.Current.Resources["BaseGrey"],
                };
                frame.Content = imagen;

                var tapAction = new TapGestureRecognizer();

                //MainInstance.CurrentFoto = foto;

                //Accion relacionada a tomar foto y su eventual parametro (objeto)
                tapAction.SetBinding(TapGestureRecognizer.CommandProperty, "ICommandImageTap");
                tapAction.SetBinding(TapGestureRecognizer.CommandParameterProperty, $"ListaImagenes[{i}]");


                frame.GestureRecognizers.Add(tapAction);

                layout.Children.Add(frame);

                layout.Children.Add(new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontSize = 10,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontFamily = "{StaticResource RegularFontOpenSans}",
                    TextColor = (Color)Application.Current.Resources["BaseGrey"],
                    Text = foto.FOTO_DESCRIPCION
                }); ;

                ImageGrid.Children.Add(layout, gridCol, gridRow);

                gridCol++;
                if (gridCol > 2)
                {
                    gridCol = 0;
                    gridRow++;
                }


                i++;
            }
        }
    }
}