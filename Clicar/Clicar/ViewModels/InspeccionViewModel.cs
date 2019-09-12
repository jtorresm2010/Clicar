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
using Clicar.Interface;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace Clicar.ViewModels
{
    public class InspeccionViewModel : BaseViewModel
    {
        #region Variables
        private List<AccordionItem> ListAccordionItems;
        //private List<ItemsAreasInspeccionDB> ListaItems;
        //private List<ItemInspeccion> ItemList;
        private Color baseGreyLight;
        private Color baseOrange;
        private Color baseGreen;
        private MainViewModel MainInstance;
        private SolicitudesPendiente currentInspeccion;
        private ObservableCollection<AccordionItem> areasInspeccion;
        public object currentItem { get; set; }
        private List<AreasInspeccion> areasInspeccionDB;
        private List<Fotografia> listaImagenes;
        private Fotografia currentFoto;
        #endregion






        #region Propiedades
        public List<Fotografia> ListaImagenes
        {
            get { return listaImagenes; }
            set { SetValue(ref listaImagenes, value); }
        }
        public Fotografia CurrentFoto
        {
            get { return currentFoto; }
            set { SetValue(ref currentFoto, value); }
        }
        public bool IsBusy { get; set; }
        public List<AreasInspeccion> AreasInspeccionDB
        {
            get { return areasInspeccionDB; }
            set { SetValue(ref areasInspeccionDB, value); }
        }
        public int CurrentIteration { get; set; }
        public SolicitudesPendiente CurrentInspeccion
        {
            get { return currentInspeccion; }
            set { SetValue(ref currentInspeccion, value); }
        }
        public ObservableCollection<ItemsAreasInspeccionDB> ItemsInspeccion { get; set; }
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

            IsBusy = false;

            InicializarColores();

            CrearListaCompuesta();

            MainInstance.DetalleItem = new DetalleItemViewModel();


        }

        private async void CrearListaCompuesta()
        {
            ListAccordionItems = new List<AccordionItem>();

            AreasInspeccionDB = await MainInstance.DataService.GetAreasInspeccion();

            var ListaItems = await MainInstance.DataService.GetItemsInspeccion();

            ListaImagenes = await MainInstance.DataService.GetImagenes();

            IOrderedEnumerable<AreasInspeccion> areasOrdenadas;

            if(AreasInspeccionDB.Count>0 && ListaItems.Count > 0)
            {
                //Ordenar areas segun el valor en "Orden"
                areasOrdenadas =
                    from areaInspeccion in AreasInspeccionDB
                    orderby areaInspeccion.AINSP_ORDEN_APP ascending
                    select areaInspeccion;
            

                foreach (AreasInspeccion area in areasOrdenadas)
                {

                    //asignar items a cada area

                    //items que corresponden al area
                    var listaItems =
                        from itemInspeccion in ListaItems
                        where itemInspeccion.ITINS_AINSP_ID == area.AINSP_ID && itemInspeccion.ITINS_ACTIVO == true
                        select itemInspeccion;

                    //se ordenan segun "orden app"
                    var itemsOrdenados =
                           from itemInspeccion in listaItems
                           orderby itemInspeccion.ITINS_ORDEN_APP ascending
                           select itemInspeccion;


                    foreach (ItemsAreasInspeccionACC items in itemsOrdenados)
                    {
                        //se agrega al item correspondiente
                        items.CLCAR_AREA_INSPECCION = area;
                    }



                    bool isImageSet = false;

                    var numeroAacc = area.AINSP_ORDEN_APP;

                    if (numeroAacc == areasOrdenadas.Count<AreasInspeccion>())
                    {
                        numeroAacc = numeroAacc + 1;
                    }


                    ListAccordionItems.AddRange(new[] {
                    new AccordionItem
                            {
                                AINSP_ACTIVO = area.AINSP_ACTIVO,
                                AINSP_DESCRIPCION = $"{numeroAacc}.\t\t{area.AINSP_DESCRIPCION}",
                                AINSP_ID = area.AINSP_ID,
                                AINSP_ORDEN_APP = numeroAacc,
                                AINSP_PAIS_ID = area.AINSP_PAIS_ID,
                                //Image = "MenuNum" + area.AINSP_ORDEN_APP,

                                Items = listaItems.ToList(),

                                IsImageSet = isImageSet
                            }
                    });
                }
            }
            var tiposImagen = await MainInstance.DataService.GetTipoImagenes();
            var numeroGrupoImagen = ListAccordionItems.Count;
            var tituloImagenBase = $"{numeroGrupoImagen}.\t\tFotografía";
            var tituloImagen = "";

            if(tiposImagen.Count <= 2)
            {
                tituloImagen = $"{tituloImagenBase} {tiposImagen[0].TIPOF_DESCRIPCION} y {tiposImagen[1].TIPOF_DESCRIPCION}";
            }
            else
            {
                tituloImagen = $"{tituloImagenBase}s";
            }


            var grupoImagenes = new AccordionItem
            {
                AINSP_DESCRIPCION = tituloImagen,
                AINSP_ORDEN_APP = numeroGrupoImagen,
                IsImageSet = true,
                ListaFotos = ListaImagenes
            };

            ListAccordionItems.Insert(numeroGrupoImagen-1, grupoImagenes);


            AreasInspeccion = new ObservableCollection<AccordionItem>(ListAccordionItems);

        }

        public void InicializarColores()
        {
            baseGreyLight = (Color)Application.Current.Resources["BaseGreyLight"];
            baseOrange = (Color)Application.Current.Resources["BaseOrange"];
            baseGreen = (Color)Application.Current.Resources["BaseGreen"];

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
                return new RelayCommand<object>(objeto => CommandImageTap(objeto));
            }
        }


        public ICommand EditarDetalleICommand
        {
            get
            {
                return new RelayCommand<object>(parameter => EditarDetalleCommand(parameter));
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

            if (Device.RuntimePlatform == Device.iOS)
            {
                //Func<object> func = () =>
                //{
                //    var obj = DependencyService.Get<IPhotoOverlay>().GetImageOverlay("front_example.png");
                //    return obj;
                //};

                //var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                //{
                //    Directory = "Clicar",
                //    Name = "front_example.jpg",
                //    PhotoSize = PhotoSize.Medium,
                //    OverlayViewProvider = func,
                //    DefaultCamera = CameraDevice.Rear,
                //});
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                var item = (Fotografia)parameter;
                CameraView cameraView = new CameraView();
                cameraView.iteminspeccion = item;

                CurrentFoto = item;

                var resultsStor = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);

                await Application.Current.MainPage.Navigation.PushAsync(cameraView);
            }

        }
        private async void EditarDetalleCommand(object parameter)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Console.WriteLine("(>'.')>-----------" + ((ItemsAreasInspeccionACC)parameter).ITINS_DESCRIPCION);


            var currentItem = (ItemsAreasInspeccionACC)parameter;
            AreasInspeccion currArea = null;

            foreach(AreasInspeccion  area in AreasInspeccionDB)
            {
                if (area.AINSP_ID == currentItem.ITINS_AINSP_ID)
                {
                    currArea = area;
                    break;
                }
            }

            MainInstance.DetalleItem.CurrentItem = currentItem;
            MainInstance.DetalleItem.CurrentArea = currArea;

            await Application.Current.MainPage.Navigation.PushAsync(new EditarDetalleView());

            //var instance = InspeccionView.GetInstance();
            //await instance.Navigation.PushAsync(new EditarDetalleView());

            IsBusy = false;
        }
    }
}
