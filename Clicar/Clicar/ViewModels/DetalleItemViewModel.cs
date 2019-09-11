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

            CurrentImage = ImageSource.FromFile("camara_select_foto");


            //GetPickerItems();
        }

        //private async void GetPickerItems()
        //{
        //    ItemsDetalle = new ObservableCollection<ItemsdetalleinspeccionDB>(await MainInstance.DataService.GetAllItemsDetalle());
        //}

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

            var lista = new List<ValorRepararItem>();
            lista.Add(new ValorRepararItem { VAREP_VALOR_REPARACION = 2 });


            try
            {
                MainInstance.Inspeccion.AreasInspeccion[CurrentItem.CLCAR_AREA_INSPECCION.AINSP_ORDEN_APP - 1]
                    .Items[MainInstance.Inspeccion.AreasInspeccion[CurrentItem.CLCAR_AREA_INSPECCION.AINSP_ORDEN_APP - 1].Items.IndexOf(CurrentItem)]
                    .ITINS_STATE_ACTIVO = true;

            }
            catch (Exception e)
            {
                Debug.WriteLine($"~(>'o')> {e.Message}");
            }


            Debug.WriteLine($"~(>'.')> {CurrentItem.CLCAR_AREA_INSPECCION.AINSP_ORDEN_APP}");
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
