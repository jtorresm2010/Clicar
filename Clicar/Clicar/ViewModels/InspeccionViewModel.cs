using Clicar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Plugin.Permissions;
using Plugin.DeviceOrientation;
using Clicar.Views;
using Xamarin.CustomControls;
using Plugin.Permissions.Abstractions;

namespace Clicar.ViewModels
{
    public class InspeccionViewModel : BaseViewModel
    {
        #region Variables
        private List<ItemInspeccion> list;
        private Color baseGreyLight;
        private Color baseOrange;
        private Color baseGreen;
        private int menuIndex;
        public object currentItem { get; set; }
        private ObservableCollection<AreasInspeccion> areasInspeccion;
        #endregion


        #region Propiedades
        public int MenuIndex { get { return this.menuIndex; } }
        public ObservableCollection<ItemInspeccion> ItemsInspeccion { get; set; }
        public ObservableCollection<AreasInspeccion> AreasInspeccion
        {
            get { return areasInspeccion; }
            set { SetValue(ref areasInspeccion, value); }
        }

        #endregion

        public InspeccionViewModel()
        {

            GetNewItemList();

            var areasInspeccion = MainViewModel.GetInstance().Agenda.AreasInspeccion;
            //var areasInspeccion = new ListaAreasInspeccion().GetListaAreas();

            //Ordenar areas segun el valor en Orden
            var areasOrdenadas =
                from areaInspeccion in areasInspeccion
                orderby areaInspeccion.AINSP_ORDEN_APP ascending
                select areaInspeccion;

            //Setea el nombre de las imagenes
            //foreach (AreasInspeccion area in areasOrdenadas)
            //{
            //    area.Image = "MenuNum" + area.Orden;
            //}

            AreasInspeccion = new ObservableCollection<AreasInspeccion>(areasOrdenadas);




        }

        public void GetNewItemList()
        {
            list = (List<ItemInspeccion>)new ListaItemsInspeccion().GetListaItems();
            menuIndex = 1;

            baseGreyLight = (Color)Application.Current.Resources["BaseGreyLight"];
            baseOrange = (Color)Application.Current.Resources["BaseOrange"];
            baseGreen = (Color)Application.Current.Resources["BaseGreen"];

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

            var itemActual = (AccordionItemView)accordionMenu.Children[int.Parse(parameter) - 1];

            itemActual.ButtonBackgroundColor = baseGreen;
            itemActual.BorderColor = baseGreen;

            try
            {
                var itemSiguiente = (AccordionItemView)accordionMenu.Children[int.Parse(parameter)];
                itemSiguiente.OpenPanel();

            }
            catch
            {
                Application.Current.MainPage.Navigation.PushAsync(new EvaluacionFinalView());
                //Console.WriteLine(e.Message);
            }

            itemActual.ClosePanel();
        }

        public ICommand ICommandNext
        {
            get
            {
                //return new Command<string>((parameter) => CommandNext(parameter));
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
                var itemAnterior = (AccordionItemView)accordionMenu.Children[int.Parse(parameter) - 2];
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

        public ICommand ICommandImageTap
        {
            get
            {
                return new RelayCommand<object>(parameter => CommandImageTap(parameter));
            }

        }
        private async void CommandImageTap(object parameter)
        {

            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    Func<object> func = () =>
            //    {
            //        var obj = DependencyService.Get<IPhotoOverlay>().GetImageOverlay("front_example.png");
            //        return obj;
            //    };

            //    var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            //    {
            //        Directory = "Clicar",
            //        Name = "front_example.jpg",
            //        PhotoSize = PhotoSize.Medium,
            //        OverlayViewProvider = func,
            //        DefaultCamera = CameraDevice.Rear,
            //    });
            //}
            //else if (Device.RuntimePlatform == Device.Android)
            //{
            //    var item = (ItemInspeccion)parameter;
            //    CameraView cameraView = new CameraView();
            //    cameraView.iteminspeccion = item;


            //    var test = CrossDeviceOrientation.Current;
            //    Console.WriteLine(test.CurrentOrientation.ToString());

            //    var resultsStor = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);

            //    await Application.Current.MainPage.Navigation.PushAsync(cameraView);





            //}


            var item = (ItemInspeccion)parameter;
            CameraView cameraView = new CameraView();
            cameraView.iteminspeccion = item;


            var test = CrossDeviceOrientation.Current;
            Console.WriteLine(test.CurrentOrientation.ToString());

            var resultsStor = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);

            await Application.Current.MainPage.Navigation.PushAsync(cameraView);


        }
        public ICommand EditarDetalleICommand
        {
            get
            {
                return new RelayCommand<string>(parameter => EditarDetalleCommand(parameter));
            }

        }
        private async void EditarDetalleCommand(string parameter)
        {
            Console.WriteLine("(>'.')>-----------" + parameter);


            await Application.Current.MainPage.Navigation.PushAsync(new EditarDetalleView());

            //var instance = InspeccionView.GetInstance();
            //await instance.Navigation.PushAsync(new EditarDetalleView());
        }






    }
}
