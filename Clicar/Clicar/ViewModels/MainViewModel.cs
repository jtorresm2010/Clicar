﻿using Clicar.Models;
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
        private List<ItemInspeccion> list;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }

        public void GetNewItemList()
        {
            list = (List<ItemInspeccion>)new ListaItemsInspeccion().GetListaItems();
            menuIndex = 1;
        }


        public void LoadItemList()
        {
            var listIteration = new List<ItemInspeccion>();

            var areasInspeccion = new ListaAreasInspeccion().GetListaAreas().Count;
            
            var ilistIteration = list.Select(ItemInspeccion => ItemInspeccion.Nombre);

            // Filtra la lista dependiendo de cual iteracion del menu acordion principal se esta mostrando
            var filteringQuery =
                from itemInspeccion in list
                where itemInspeccion.Area == menuIndex.ToString()
                select itemInspeccion;

            this.ItemsInspeccion = new ObservableCollection<ItemInspeccion>(filteringQuery);









            menuIndex++;
        }


        public void CommandNext(string parameter)
        {
            var inspeccionView = InspeccionView.GetInstance();

            var accordionMenu = (AccordionRepeaterView)inspeccionView.FindByName("AccordionMenu");

            var itemActual = (AccordionItemView)accordionMenu.Children[int.Parse(parameter)-1];
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
                //return new Command<string>((parameter) => CommandNext(parameter));
                return new RelayCommand<string>(parameter => CommandNext(parameter));
            }
        }




















    }
}
