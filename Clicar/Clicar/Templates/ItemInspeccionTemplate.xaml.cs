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
        private int index;
        MainViewModel mainInstance;
        public ItemInspeccionTemplate()
        {
            InitializeComponent();

            mainInstance = MainViewModel.GetInstance();

            index = mainInstance.MenuIndex;

            SetButtons();

            mainInstance.LoadItemList();

            var filteringQuery =
                from itemInspeccion in mainInstance.ItemsInspeccion
                where itemInspeccion.Tipo == "3"
                select itemInspeccion;

            if(filteringQuery.Count() != 0)
            {
                itemsInspeccionListView.IsVisible = false;
                CreateGrid();
            }
            else
            {
                ImageGrid.IsVisible = false;
                SetList();

            }



        }


        private void SetList()
        {
            itemsInspeccionListView.HeightRequest = mainInstance.ItemsInspeccion.Count() * itemsInspeccionListView.RowHeight;
        }

        private void CreateGrid()
        {

            var gridRow = 0;
            var gridCol = 0;

            foreach (ItemInspeccion item in mainInstance.ItemsInspeccion)
            {


                StackLayout layout = new StackLayout();

                Image imagen = new Image
                {
                    HeightRequest = 100,
                    Source = "camara_menu"
                };

                var tapAction = new TapGestureRecognizer();

                var mainInstance = MainViewModel.GetInstance();
                mainInstance.testObj = item;

                //Accion relacionada a tomar foto y su eventual parametro
                tapAction.SetBinding(TapGestureRecognizer.CommandProperty, "ICommandImageTap");
                //tapAction.SetBinding(TapGestureRecognizer.CommandParameterProperty, (object)item as Binding);
                tapAction.SetBinding(TapGestureRecognizer.CommandParameterProperty, "testObj");
                

                imagen.GestureRecognizers.Add(tapAction);

                layout.Children.Add(imagen);

                layout.Children.Add(new Label
                { HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontSize = 10,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontFamily = "{StaticResource RegularFontOpenSans}",
                    TextColor = (Color)Application.Current.Resources["BaseGrey"],
                    Text = item.Nombre }); ;

                ImageGrid.Children.Add(layout, gridCol, gridRow);

                gridCol++;
                if(gridCol > 2)
                {
                    gridCol = 0;
                    gridRow++;
                }




            }




        }

        private void SetButtons()
        {
            if(index == 1)
            {
                BackButton.IsVisible = false;
            }
            BackButton.CommandParameter = index.ToString();
            ForwardButton.CommandParameter = index.ToString();
        }




    }
}