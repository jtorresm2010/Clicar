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

            foreach (ItemsAreasInspeccion item in MainInstance.AreasInspeccion[MainInstance.CurrentIteration].Items)
            {


                StackLayout layout = new StackLayout();

                Image imagen = new Image
                {
                    HeightRequest = 100,
                    Source = "camara_menu"
                };

                var tapAction = new TapGestureRecognizer();

                MainInstance.currentItem = item;

                //Accion relacionada a tomar foto y su eventual parametro (objeto)
                tapAction.SetBinding(TapGestureRecognizer.CommandProperty, "ICommandImageTap");
                tapAction.SetBinding(TapGestureRecognizer.CommandParameterProperty, "currentItem");


                imagen.GestureRecognizers.Add(tapAction);

                layout.Children.Add(imagen);

                layout.Children.Add(new Label
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontSize = 10,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontFamily = "{StaticResource RegularFontOpenSans}",
                    TextColor = (Color)Application.Current.Resources["BaseGrey"],
                    Text = item.ITINS_DESCRIPCION
                }); ;

                ImageGrid.Children.Add(layout, gridCol, gridRow);

                gridCol++;
                if (gridCol > 2)
                {
                    gridCol = 0;
                    gridRow++;
                }
            }
        }
    }
}