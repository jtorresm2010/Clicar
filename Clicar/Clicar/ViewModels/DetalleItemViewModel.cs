using Clicar.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clicar.ViewModels
{
    public class DetalleItemViewModel : BaseViewModel
    {


        private MainViewModel MainInstance { get; set; }
        private DetallesItem currentItemDetails;
        private ItemsAreasInspeccionACC currentItem;
        private List<AreasInspeccion> areasInspeccionDB;
        private AreasInspeccion currentArea;
        private ObservableCollection<ItemsdetalleinspeccionDB> itemsDetalle;
        private bool needImage;
        private bool damageInfo;
        private ImageSource image;


        private string comment;

        public string Comment
        {
            get { return comment; }
            set { SetValue(ref comment, value); }
        }



        private string nivelDanio;

        public string NivelDanio
        {
            get { return nivelDanio; }
            set { SetValue(ref nivelDanio, value); }
        }




        private bool stateSustituir;

        public bool StateSustituir
        {
            get { return stateSustituir; }
            set { SetValue(ref stateSustituir, value); }
        }


        private string sustituirTxt;

        public string SustituirTxt
        {
            get { return sustituirTxt; }
            set { SetValue(ref sustituirTxt, value); }
        }

        private string repararTxt;

        public string RepararTxt
        {
            get { return repararTxt; }
            set { SetValue(ref repararTxt, value); }
        }




        private bool stateMalo;

        public bool StateMalo
        {
            get { return stateMalo; }
            set { SetValue(ref stateMalo, value); }
        }







        public ImageSource CurrentImage
        {
            get { return image; }
            set { SetValue(ref image, value); }
        }
     public bool NeedImage
        {
            get { return needImage; }
            set { SetValue(ref needImage, value); }
        }
        public bool DamageInfo
        {
            get { return damageInfo; }
            set { SetValue(ref damageInfo, value); }
        }
        public ObservableCollection<ItemsdetalleinspeccionDB> ItemsDetalle
        {
            get { return itemsDetalle; }
            set { SetValue(ref itemsDetalle, value); }
        }
        public List<AreasInspeccion> AreasInspeccionDB
        {
            get { return areasInspeccionDB; }
            set { SetValue(ref areasInspeccionDB, value); }
        }
        public AreasInspeccion CurrentArea
        {
            get { return currentArea; }
            set { SetValue(ref currentArea, value); }
        }
        public List<AreasInspeccion> ListaAreas { get; set; }
        public ItemsAreasInspeccionACC CurrentItem
        {
            get { return currentItem; }
            set { SetValue(ref currentItem, value); }
        }
        public DetallesItem CurrentItemDetails
        {
            get { return currentItemDetails; }
            set { SetValue(ref currentItemDetails, value); }
        }



        

        public DetalleItemViewModel()
        {
            MainInstance = MainViewModel.GetInstance();
            SustituirTxt = "Sustituir";
            RepararTxt = "Reparar";
            CurrentImage = ImageSource.FromFile("camara_select_foto");

        }


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals("CurrentItem"))
            {
                switch (CurrentItem.ITINS_CONDICION)
                {
                    case "Esta presente":
                        DamageInfo = false;
                        break;
                    case "Es Condición":
                        DamageInfo = true;
                        break;
                    case "Es daño":
                        DamageInfo = true;
                        break;
                    default:
                        break;
                }

            }


        }

        #region ICommands

        
        public ICommand ConfirmarICommand
        {
            get
            {
                return new RelayCommand(ConfirmarCommand);
            }
        }


        public ICommand TakePictureICommand
        {
            get
            {
                return new RelayCommand(TakePictureCommand);
            }
        }
        public ICommand OpenGalleryICommand
        {
            get
            {
                return new RelayCommand(OpenGalleryCommand);
            }
        }
        #endregion



        private void ConfirmarCommand()
        {
            //var a = MainInstance.Inspeccion.AreasInspeccion[CurrentItem.ITINS_ORDEN_APP - 1].Items.IndexOf(CurrentItem)

            //var lista = new List<ValorRepararItem>();
            //lista.Add(new ValorRepararItem { VAREP_VALOR_REPARACION = 2 });

            var currentAreaIndex = CurrentItem.CLCAR_AREA_INSPECCION.AINSP_ORDEN_APP - 1;
            var currentItemIndex = MainInstance.Inspeccion.AreasInspeccion[CurrentItem.CLCAR_AREA_INSPECCION.AINSP_ORDEN_APP - 1].Items.IndexOf(CurrentItem);

            MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].ITINS_STATE_ACTIVO = true;

            if(StateMalo)
                MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].Solucion = StateSustituir ? SustituirTxt: RepararTxt;

            MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].Condicion = NivelDanio;
            MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].Comentario = comment;
            MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].IsChanged = true;

            if (!CurrentImage.Equals(ImageSource.FromFile("camara_select_foto")))
                MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].Imagen = CurrentImage;
            

            Debug.WriteLine($"~(>'.')> {MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].ITINS_STATE_ACTIVO}" +
                $" {MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].Condicion}" +
                $" {MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].Comentario}" +
                $" {MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].IsChanged}" +
                $" {MainInstance.Inspeccion.AreasInspeccion[currentAreaIndex].Items[currentItemIndex].Imagen.Id}");

            Comment = string.Empty;

            Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void OpenGalleryCommand()
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

            CurrentImage = ImageSource.FromFile(photo.Path);
        }

        private async void TakePictureCommand()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Debug.WriteLine("Camara no Disponible Q(-.-Q)~");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Clicar",
                Name = $"{currentItem.ITINS_ID}test.jpg"
            });

            if (file == null)
                return;

            Debug.WriteLine($"~(>'.')> {file.Path}");
            CurrentImage = ImageSource.FromFile(file.Path);
        }
    }
}
