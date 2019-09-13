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


    }
}