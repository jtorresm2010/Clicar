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
using System.Diagnostics;

namespace Clicar.ViewModels
{
    public class InspeccionViewModel : BaseViewModel
    {
        #region Variables
        private List<AccordionItem> ListAccordionItems;
        private List<ItemInspeccion> ItemList;
        private Color baseGreyLight;
        private Color baseOrange;
        private Color baseGreen;
        private int menuIndex;
        private MainViewModel MainInstance;
        private Inspeccion currentInspeccion;
        private ObservableCollection<AccordionItem> areasInspeccion;
        public object currentItem { get; set; }
        #endregion

        #region Propiedades
        public int CurrentIteration { get; set; }
        public Inspeccion CurrentInspeccion
        {
            get { return currentInspeccion; }
            set { SetValue(ref currentInspeccion, value); }
        }
        public int MenuIndex { get { return this.menuIndex; } }
        public ObservableCollection<ItemInspeccion> ItemsInspeccion { get; set; }
        public ObservableCollection<AccordionItem> AreasInspeccion
        {
            get { return areasInspeccion; }
            set { SetValue(ref areasInspeccion, value); }
        }

        #endregion

        public InspeccionViewModel()
        {
            MainInstance = MainViewModel.GetInstance();
            CurrentIteration = 0;

            GetNewItemList();

            var areasInspeccion = MainInstance.Agenda.AreasInspeccion;

            //Ordenar areas segun el valor en "Orden"
            try
            {
                var areasOrdenadas =
                    from areaInspeccion in areasInspeccion
                    orderby areaInspeccion.AINSP_ORDEN_APP ascending
                    select areaInspeccion;

                CrearListaCompuesta(areasOrdenadas);


                AreasInspeccion = new ObservableCollection<AccordionItem>(ListAccordionItems);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"~(>'.')> ?? {ex.Message}");
            }




        }

        private void CrearListaCompuesta(IOrderedEnumerable<AreasInspeccion> areas)
        {
           ListAccordionItems = new List<AccordionItem>();


            foreach (AreasInspeccion area in areas)
            {

                var listaItems =
                    from itemInspeccion in ItemList
                    where itemInspeccion.Area == area.AINSP_ORDEN_APP.ToString()
                    select itemInspeccion;

                var filteringQuery =
                    from itemInspeccion in listaItems
                    where itemInspeccion.Tipo == "3"
                    select itemInspeccion;

                bool isImageSet = filteringQuery.Count() != 0;



                ListAccordionItems.AddRange(new[] {
                new AccordionItem
                        {
                            AINSP_ACTIVO = area.AINSP_ACTIVO,
                            AINSP_DESCRIPCION = area.AINSP_DESCRIPCION,
                            AINSP_ID = area.AINSP_ID,
                            AINSP_ORDEN_APP = area.AINSP_ORDEN_APP,
                            AINSP_PAIS_ID = area.AINSP_PAIS_ID,
                            Image = "MenuNum" + area.AINSP_ORDEN_APP,

                            Items = listaItems.ToList(),

                            IsImageSet = isImageSet
                        }

                });



            }




        }

        public void GetNewItemList()
        {
            ItemList = (List<ItemInspeccion>)new ListaItemsInspeccion().GetListaItems();
            menuIndex = 1;

            baseGreyLight = (Color)Application.Current.Resources["BaseGreyLight"];
            baseOrange = (Color)Application.Current.Resources["BaseOrange"];
            baseGreen = (Color)Application.Current.Resources["BaseGreen"];

        }

        public void AccordionCounter()
        {
            //var listIteration = new List<ItemInspeccion>();

            //var areasInspeccion = new ListaAreasInspeccion().GetListaAreas().Count;

            //var ilistIteration = ItemList.Select(ItemInspeccion => ItemInspeccion.Nombre);

            //// Filtra la lista dependiendo de cual iteracion del menu acordion principal se esta mostrando
            //var filteringQuery =
            //    from itemInspeccion in ItemList
            //    where itemInspeccion.Area == menuIndex.ToString()
            //    select itemInspeccion;

            //this.ItemsInspeccion = new ObservableCollection<ItemInspeccion>(filteringQuery);

            menuIndex++;
        }

        #region Icommands
        public ICommand ICommandNext
        {
            get
            {
                //return new Command<string>((parameter) => CommandNext(parameter));
                return new RelayCommand<int>(parameter => CommandNext(parameter));
            }
        }
        public ICommand ICommandBack
        {
            get
            {
                return new RelayCommand<int>(parameter => CommandBack(parameter));
            }

        }
        public ICommand ICommandImageTap
        {
            get
            {
                return new RelayCommand<object>(parameter => CommandImageTap(parameter));
            }

        }
        public ICommand EditarDetalleICommand
        {
            get
            {
                return new RelayCommand<string>(parameter => EditarDetalleCommand(parameter));
            }

        }
        #endregion

        public void CommandNext(int parameter)
        {
            var inspeccionView = InspeccionView.GetInstance();

            var accordionMenu = (AccordionRepeaterView)inspeccionView.FindByName("AccordionMenu");

            var itemActual = (AccordionItemView)accordionMenu.Children[parameter - 1];

            itemActual.ButtonBackgroundColor = baseGreen;
            itemActual.BorderColor = baseGreen;

            try
            {
                var itemSiguiente = (AccordionItemView)accordionMenu.Children[parameter];
                itemSiguiente.OpenPanel();

            }
            catch
            {
                MainInstance.Reporte = new ReporteViewModel();
                Application.Current.MainPage.Navigation.PushAsync(new EvaluacionFinalView());
            }

            itemActual.ClosePanel();

        }
        public void CommandBack(int parameter)
        {
            var inspeccionView = InspeccionView.GetInstance();

            var accordionMenu = (AccordionRepeaterView)inspeccionView.FindByName("AccordionMenu");

            var itemActual = (AccordionItemView)accordionMenu.Children[parameter - 1];

            itemActual.ClosePanel();

            try
            {
                var itemAnterior = (AccordionItemView)accordionMenu.Children[parameter - 2];
                itemAnterior.OpenPanel();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
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
        private async void EditarDetalleCommand(string parameter)
        {
            Console.WriteLine("(>'.')>-----------" + parameter);


            await Application.Current.MainPage.Navigation.PushAsync(new EditarDetalleView());

            //var instance = InspeccionView.GetInstance();
            //await instance.Navigation.PushAsync(new EditarDetalleView());
        }
    }
}
