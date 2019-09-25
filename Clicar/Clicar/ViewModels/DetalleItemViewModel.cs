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
        private bool stateSustituir;
        private string nivelDanio;
        private string sustituirTxt;
        private string repararTxt;
        private bool stateMalo;




        public List<AreasInspeccion> ListaAreas { get; set; }
        public string NivelDanio
        {
            get { return nivelDanio; }
            set { SetValue(ref nivelDanio, value); }
        }
        public string Comment
        {
            get { return comment; }
            set { SetValue(ref comment, value); }
        }
        public bool StateSustituir
        {
            get { return stateSustituir; }
            set { SetValue(ref stateSustituir, value); }
        }
        public string SustituirTxt
        {
            get { return sustituirTxt; }
            set { SetValue(ref sustituirTxt, value); }
        }
        public string RepararTxt
        {
            get { return repararTxt; }
            set { SetValue(ref repararTxt, value); }
        }
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
        public bool EstadoBase { get; set; }




        public DetalleItemViewModel()
        {
            MainInstance = MainViewModel.GetInstance();
            SustituirTxt = "Sustituir";
            RepararTxt = "Reparar";
            CurrentImage = ImageSource.FromFile("camara_select_foto");
           // CurrentItem.Imagen = ImageSource.FromFile("camara_select_foto");
        }


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            try
            {
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
            catch (Exception)
            {

                throw;
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
            CurrentItem.IsChanged = true;

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


            CurrentItem.Imagen = photo.Path;
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

            CurrentItem.Imagen = file.Path;
        }
    }
}
