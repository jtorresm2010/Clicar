using Clicar.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Customs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomGrid : Grid
    {

        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
            propertyName: "ItemSource",
            returnType: typeof(IEnumerable),
            declaringType: typeof(CustomGrid),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: GridItemsChanged);

        private static void GridItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var list = (List<ItemInspeccion>)newValue;

            foreach(ItemInspeccion item in list)
            {
                Debug.WriteLine($"~(>'.')> {item.Nombre}");
            }


            ListaItems = (List<ItemInspeccion>)newValue;

            Debug.WriteLine($"~(>'.')> ---------------------Grid{newValue.ToString()}");
        }

        private static List<ItemInspeccion> ListaItems;

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public CustomGrid()
        {
            InitializeComponent();




        }





        //private void CreateGrid()
        //{

        //    var gridRow = 0;
        //    var gridCol = 0;

        //    foreach (ItemInspeccion item in MainInstance.ItemsInspeccion)
        //    {


        //        StackLayout layout = new StackLayout();

        //        Image imagen = new Image
        //        {
        //            HeightRequest = 100,
        //            Source = "camara_menu"
        //        };

        //        var tapAction = new TapGestureRecognizer();

        //        MainInstance.currentItem = item;

        //        //Accion relacionada a tomar foto y su eventual parametro (objeto)
        //        tapAction.SetBinding(TapGestureRecognizer.CommandProperty, "ICommandImageTap");
        //        tapAction.SetBinding(TapGestureRecognizer.CommandParameterProperty, "currentItem");


        //        imagen.GestureRecognizers.Add(tapAction);

        //        layout.Children.Add(imagen);

        //        layout.Children.Add(new Label
        //        {
        //            HorizontalOptions = LayoutOptions.CenterAndExpand,
        //            FontSize = 10,
        //            HorizontalTextAlignment = TextAlignment.Center,
        //            FontFamily = "{StaticResource RegularFontOpenSans}",
        //            TextColor = (Color)Application.Current.Resources["BaseGrey"],
        //            Text = item.Nombre
        //        }); ;

        //        ImageGrid.Children.Add(layout, gridCol, gridRow);

        //        gridCol++;
        //        if (gridCol > 2)
        //        {
        //            gridCol = 0;
        //            gridRow++;
        //        }




        //    }




        //}

    }
}