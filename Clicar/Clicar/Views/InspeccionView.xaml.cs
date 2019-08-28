using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CustomControls;
using Xamarin.Forms;
using Clicar.Models;
using Xamarin.Forms.Xaml;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Clicar.ViewModels;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspeccionView : ContentPage
    {


        public InspeccionView()
        {
            InitializeComponent();

            instance = this;

            var mainInstance = MainViewModel.GetInstance();

            mainInstance.GetNewItemList();

            var areasInspeccion = new ListaAreasInspeccion().GetListaAreas();

            //Ordenar areas segun el valor en Orden
            var areasOrdenadas =
                from areaInspeccion in areasInspeccion
                orderby areaInspeccion.Orden ascending
                select areaInspeccion;







            //Setea el nombre de las imagenes
            foreach(AreaInspeccion area in areasOrdenadas)
            {
                area.Image = "MenuNum" + area.Orden;
            }

            //Setea el dataset delacordion
            AccordionMenu.ItemsSource = areasOrdenadas.ToList<AreaInspeccion>();


            //Abre  el primer panel
            var primerItem = (AccordionItemView)AccordionMenu.Children[0];
            primerItem.OpenPanel();
            

            //Aqui se implementaria Skias...
            //primerItem.ActiveLeftImage = 



        }


        private static InspeccionView instance;

        public static InspeccionView GetInstance()
        {
            if (instance == null)
            {
                instance = new InspeccionView();
            }
            return instance;
        }



    }
}