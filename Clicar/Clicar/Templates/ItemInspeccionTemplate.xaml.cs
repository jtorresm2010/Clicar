using Clicar.Models;
using Clicar.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemInspeccionTemplate : AccordionItemView
    {
        private int index;
        public ItemInspeccionTemplate()
        {
            InitializeComponent();

            //var accordMenuItem = this.Parent;
            // var index = accordMenuItem.Text;

            var mainInstance = MainViewModel.GetInstance();

            index = mainInstance.MenuIndex;
            SetButtons();

            mainInstance.LoadItemList();

            itemsInspeccionListView.HeightRequest = mainInstance.ItemsInspeccion.Count() * itemsInspeccionListView.RowHeight;

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