using Clicar.Models;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CustomControls;
using Xamarin.Forms;

namespace Clicar.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private Color baseGreyLight;
        private Color baseOrange;
        private Color baseGreen;
        private int menuIndex;

        public int MenuIndex { get { return this.menuIndex; } }

        public ObservableCollection<ItemInspeccion> ItemsInspeccion { get; set; }


        public MainViewModel()
        {
            instance = this;




        }

        private static MainViewModel instance;
        private List<ItemInspeccion> itemList;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }

        public void GetNewItemList() // Inicializa la lista de items y los colores
        {
            itemList = (List<ItemInspeccion>)new ListaItemsInspeccion().GetListaItems();

            menuIndex = 1;

            baseGreyLight = (Color)Application.Current.Resources["BaseGreyLight"];
            baseOrange = (Color)Application.Current.Resources["BaseOrange"];
            baseGreen = (Color)Application.Current.Resources["BaseGreen"];

        }



        public void LoadItemList()
        {

            var filteringQuery =
                from itemInspeccion in itemList
                where itemInspeccion.Area == menuIndex.ToString()
                select itemInspeccion;

            this.ItemsInspeccion = new ObservableCollection<ItemInspeccion>(filteringQuery);

            menuIndex++;
        }


        public void CommandNext(string parameter)
        {
            var inspeccionView = InspeccionView.GetInstance();

            var accordionMenu = (AccordionRepeaterView)inspeccionView.FindByName("AccordionMenu");

            var itemActual = (AccordionItemView)accordionMenu.Children[int.Parse(parameter) - 1];
            
            itemActual.ButtonBackgroundColor = baseGreen;
            itemActual.BorderColor = baseGreen;

            itemActual.ClosePanel();

            try
            {
                var itemSiguiente = (AccordionItemView)accordionMenu.Children[int.Parse(parameter)];
                itemSiguiente.OpenPanel();

            }
            catch (Exception e )
            {

                Console.WriteLine(e.Message);
            }

            Console.WriteLine("---------------------" + parameter);
        }

        
        public ICommand ICommandNext
        {
            get
            {
                return new RelayCommand<string>(parameter => CommandNext(parameter));
            }
        }

        public void CommandBack(string parameter)
        {
            var inspeccionView = InspeccionView.GetInstance();

            var accordionMenu = (AccordionRepeaterView)inspeccionView.FindByName("AccordionMenu");

            var itemActual = (AccordionItemView)accordionMenu.Children[int.Parse(parameter) - 1];

            itemActual.ClosePanel();

            try
            {
                var itemAnterior = (AccordionItemView)accordionMenu.Children[int.Parse(parameter)-2];
                itemAnterior.OpenPanel();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            Console.WriteLine("---------------------" + parameter);
        }



        public ICommand ICommandBack
        {
            get
            {
                return new RelayCommand<string>(parameter => CommandBack(parameter));
            }
        }





















    }
}
