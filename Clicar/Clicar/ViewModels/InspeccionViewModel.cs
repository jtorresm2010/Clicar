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
        private Color baseGreyLight;
        private Color baseOrange;
        private Color baseGreen;
        private MainViewModel MainInstance;
        private SolicitudesInspeccionPendiente currentInspeccion;
        private ObservableCollection<AccordionItem> areasInspeccion;
        public object currentItem { get; set; }
        private List<AreasInspeccion> areasInspeccionDB;
        private List<Fotografia> listaImagenes;
        private List<Fotografia> listaImagenes2;
        private Fotografia currentFoto;
        private bool isLoading;
        private DateTime horaTermino;
        private DateTime tiempoInspeccion;
        private DateTime horaInicio;
        private List<AccordionItem> areasInspeccionBase;
        #endregion

        #region Propiedades
        public DateTime HoraTermino
        {
            get { return horaTermino; }
            set { SetValue(ref horaTermino, value); }
        }
        public DateTime TiempoInspeccion
        {
            get { return tiempoInspeccion; }
            set { SetValue(ref tiempoInspeccion, value); }
        }
        public DateTime HoraInicio
        {
            get { return horaInicio; }
            set { SetValue(ref horaInicio, value); }
        }
        public bool PageLoaded { get; set; }
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetValue(ref isLoading, value); }
        }

        private List<Fotografia> listaImagenesMain;

        public List<Fotografia> ListaImagenesMain
        {
            get { return listaImagenesMain; }
            set { SetValue(ref listaImagenesMain, value); }
        }

        public List<Fotografia> ListaImagenes
        {
            get { return listaImagenes; }
            set { SetValue(ref listaImagenes, value); }
        }
        public List<Fotografia> ListaImagenes2
        {
            get { return listaImagenes2; }
            set { SetValue(ref listaImagenes2, value); }
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
        public SolicitudesInspeccionPendiente CurrentInspeccion
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
        public List<AccordionItem> AreasInspeccionBase
        {
            get { return areasInspeccionBase; }
            set { SetValue(ref areasInspeccionBase, value); }
        }
        public int CurrentImageSet { get; set; }

        #endregion



        public InspeccionViewModel()
        {
            MainInstance = MainViewModel.GetInstance();
            CurrentIteration = 0;
            IsLoading = true; 
            IsBusy = false;

            CurrentImageSet = 0;
            MainInstance.DetalleItem = new DetalleItemViewModel();

            InicializarColores();
            //CrearListaCompuesta();

        }

        public async void CrearListaCompuesta()
        {
            if (PageLoaded)
                return;

            IsLoading = true;
            ListAccordionItems = new List<AccordionItem>();

            AreasInspeccionDB = await MainInstance.DataService.GetAreasInspeccion();

            var ListaItems = await MainInstance.DataService.GetItemsInspeccion();

            foreach(ItemsAreasInspeccionACC item in ListaItems)
            {
                item.Imagen = ImageSource.FromFile("camara_select_foto.png");
                item.SwitchActive = true;
            }

            ListaImagenesMain = await MainInstance.DataService.GetImagenes();

            IOrderedEnumerable<AreasInspeccion> areasOrdenadas;

            if(AreasInspeccionDB.Count>0 && ListaItems.Count > 0)
            {
                //Ordenar areas segun el valor en "Orden"
                areasOrdenadas =
                    from areaInspeccion in AreasInspeccionDB
                    where areaInspeccion.AINSP_ACTIVO == 1
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


                    int numeroAacc = area.AINSP_ORDEN_APP == areasOrdenadas.Count<AreasInspeccion>() ? area.AINSP_ORDEN_APP + 2 : area.AINSP_ORDEN_APP;


                    ListAccordionItems.AddRange(new[] {
                    new AccordionItem
                            {
                                AINSP_ACTIVO = area.AINSP_ACTIVO,
                                AINSP_DESCRIPCION = $"{numeroAacc}.\t\t{area.AINSP_DESCRIPCION}",
                                AINSP_ID = area.AINSP_ID,
                                AINSP_ORDEN_APP = numeroAacc,
                                AINSP_PAIS_ID = area.AINSP_PAIS_ID,
                                Items = listaItems.ToList(),
                                IsImageSet = isImageSet
                            }
                    });
                }
            }

            if(ListaImagenesMain.Count > 0)
            {
                var tiposImagen = await MainInstance.DataService.GetTipoImagenes();
                var numeroGrupoImagen = ListAccordionItems.Count;
                var tituloImagenBase = $"{numeroGrupoImagen}.\t\tFotografía";
                var tituloImagen = "";

                tituloImagen = tiposImagen.Count <= 2 ? 
                    $"{tituloImagenBase} {tiposImagen[0].TIPOF_DESCRIPCION} y {tiposImagen[1].TIPOF_DESCRIPCION}" : 
                    $"{tituloImagenBase}s";

                foreach (Fotografia foto in ListaImagenesMain)
                {
                    foto.CurrentImageSmall = "camara_select_foto.png";
                }

                var lista1 =
                    from img1 in ListaImagenesMain
                    where img1.FOTO_TIPOF_ID == 1
                    select img1;

                ListaImagenes = lista1.ToList();

                var grupoImagenes = new AccordionItem
                {
                    AINSP_DESCRIPCION = $"{numeroGrupoImagen}.\t\tFotografía {tiposImagen[0].TIPOF_DESCRIPCION}",
                    AINSP_ORDEN_APP = numeroGrupoImagen,
                    IsImageSet = true,
                    ListaFotos = ListaImagenes
                };

                ListAccordionItems.Insert(numeroGrupoImagen-1, grupoImagenes);

                var lista2 =
                    from img2 in ListaImagenesMain
                    where img2.FOTO_TIPOF_ID == 2
                    select img2;

                ListaImagenes2 = lista2.ToList();

                var grupoImagenes2 = new AccordionItem
                {
                    AINSP_DESCRIPCION = $"{numeroGrupoImagen+1}.\t\tFotografía {tiposImagen[1].TIPOF_DESCRIPCION}",
                    AINSP_ORDEN_APP = numeroGrupoImagen+1,
                    IsImageSet = true,
                    ListaFotos = ListaImagenes2
                };

                ListAccordionItems.Insert(ListAccordionItems.Count - 1, grupoImagenes2);




            }

            AreasInspeccion = new ObservableCollection<AccordionItem>(ListAccordionItems);
            AreasInspeccionBase = ListAccordionItems; //lista estatica que se usara para comparar


            IsLoading = false;
            PageLoaded = true;
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

        public async void CommandNext(int parameter)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new EvaluacionFinalView());

            var inspeccionView = InspeccionView.GetInstance();

            var accordionMenu = (AccordionRepeaterView)inspeccionView.FindByName("AccordionMenu");

            var itemActual = (AccordionItemView)accordionMenu.Children[parameter - 1];

            itemActual.ButtonBackgroundColor = baseGreen;
            itemActual.BorderColor = baseGreen;

            if (parameter < AreasInspeccion[AreasInspeccion.Count - 1].AINSP_ORDEN_APP)
            {
                var itemSiguiente = (AccordionItemView)accordionMenu.Children[parameter];
                itemSiguiente.OpenPanel();

            }
            else
            {
                HoraTermino = DateTime.Now;

                TimeSpan timeDiff = HoraTermino - HoraInicio;


                TiempoInspeccion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timeDiff.Hours, timeDiff.Minutes, timeDiff.Seconds);//timeDiff;

                MainInstance.Reporte = new ReporteViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new EvaluacionFinalView());

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
            if (IsBusy)
                return;

            IsBusy = true;



            var source = await Application.Current.MainPage.DisplayActionSheet(
                "Seleccione el origen de la imagen",
                "Cancelar",
                null,
                "Camara",
                "Galeria"
                );

            switch (source)
            {
                case "Galeria":

                    SelectFromGallery(parameter);
                    break;

                case "Camara":
                    TakePicture(parameter);
                    break;
                default:
                    break;
            }

            IsBusy = false;

        }
        private async void TakePicture(object parameter)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                Func<object> func = () =>
                {
                    var obj = DependencyService.Get<IPhotoOverlay>().GetImageOverlay("front_example.png");
                    return obj;
                };

                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    Directory = "Clicar",
                    Name = "front_example.jpg",
                    PhotoSize = PhotoSize.Medium,
                    OverlayViewProvider = func,
                    DefaultCamera = CameraDevice.Rear,
                });

                if (photo == null)
                    return;


                Debug.WriteLine("Ruta de la imagen: " + photo.Path);

                ((Fotografia)parameter).CurrentImageSmall = photo.Path;

                var currentIndex = AreasInspeccion.Count;

                if (((Fotografia)parameter).FOTO_TIPOF_ID == 1)
                {
                    AreasInspeccion[currentIndex - 3].ListaFotos[ListaImagenes.IndexOf((Fotografia)parameter)] = (Fotografia)parameter;

                }
                else
                {
                    AreasInspeccion[currentIndex - 2].ListaFotos[ListaImagenes2.IndexOf((Fotografia)parameter)] = (Fotografia)parameter;
                }
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                var item = (Fotografia)parameter;
                CameraView cameraView = new CameraView();
                cameraView.CurrentImageInFrame = item;

                //CurrentFoto = item;

                var resultsStor = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);

                await Application.Current.MainPage.Navigation.PushAsync(cameraView);
            }
        }
        private async void SelectFromGallery(object CurrentImage)
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Debug.WriteLine("Galeria no Disponible Q(-.-Q)~");
                return;
            }

            var photo = await CrossMedia.Current.PickPhotoAsync();

            if (photo == null)
                return;


            Debug.WriteLine("Ruta de la imagen: " + photo.Path);

            ((Fotografia)CurrentImage).CurrentImageSmall = photo.Path;

            var currentIndex = AreasInspeccion.Count;

            if (((Fotografia)CurrentImage).FOTO_TIPOF_ID == 1)
            {
                AreasInspeccion[currentIndex - 3].ListaFotos[ListaImagenes.IndexOf((Fotografia)CurrentImage)] = (Fotografia)CurrentImage;

            }
            else
            {
                AreasInspeccion[currentIndex - 2].ListaFotos[ListaImagenes2.IndexOf((Fotografia)CurrentImage)] = (Fotografia)CurrentImage;
            }
        }
        private async void EditarDetalleCommand(object parameter)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var currentItem = (ItemsAreasInspeccionACC)parameter;
                AreasInspeccion currArea = null;

                foreach (AreasInspeccion area in AreasInspeccionDB)
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

            }
            catch (Exception ee)
            {
                Debug.WriteLine($"~(>'.')> {ee.Message}");
            }


            //Console.WriteLine("(>'.')>-----------" + ((ItemsAreasInspeccionACC)parameter).ITINS_DESCRIPCION);



            //var instance = InspeccionView.GetInstance();
            //await instance.Navigation.PushAsync(new EditarDetalleView());

            IsBusy = false;
        }
    }
}
