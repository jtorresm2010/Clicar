using Clicar.Interface;
using Clicar.Models;
using Clicar.ViewModels;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraView : ContentPage
    {
        readonly SensorSpeed speed = SensorSpeed.Default;
        private bool ImageIsFlipping = false;

        public Fotografia CurrentImageInFrame { get; set; }

        MainViewModel MainInstance;


        public CameraView()
        {
            InitializeComponent();
            MainInstance = MainViewModel.GetInstance();
            CameraPreview.PictureFinished += OnPictureFinished;

            InitializeAccelerometer();

            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        void InitializeAccelerometer()
        {
            try
            {
                ToggleAccelerometer();
                Accelerometer.ReadingChanged += Reading_Changed;
            }
            catch (FeatureNotSupportedException)
            {
                Debug.WriteLine("Accelerometer Unavailable");
            }
        }

        private void Reading_Changed(object sender, AccelerometerChangedEventArgs e)
        {
            var xValue = Math.Abs(e.Reading.Acceleration.X);
            var yValue = Math.Abs(e.Reading.Acceleration.Y);


            if (xValue < yValue)
            {
                CameraPreview.Orientation = Customs.Orientation.Portrait;
                FlipUIElements();
            }
            else
            {
                CameraPreview.Orientation = Customs.Orientation.Landscape;
                FlipUIElements();
            }
        }

        private async void FlipUIElements()
        {
            if (ImageIsFlipping)
                return;

            ImageIsFlipping = true;
            uint intervalo = 300;

            if(CameraPreview.Orientation == Customs.Orientation.Portrait)
            {
                await Task.WhenAll(
                    OverlayImage.RotateTo(0, intervalo, Easing.CubicIn)
                    //GalleryButton.RotateTo(0, intervalo, Easing.CubicIn),
                    //CameraButton.RotateTo(0, intervalo, Easing.CubicIn)
                    );
            }
            else
            {
                await Task.WhenAll(
                    OverlayImage.RotateTo(90, intervalo, Easing.CubicIn)
                    //GalleryButton.RotateTo(90, intervalo, Easing.CubicIn),
                    //CameraButton.RotateTo(90, intervalo, Easing.CubicIn)
                    );
            }

            ImageIsFlipping = false;
        }

        protected async override void OnAppearing()
            {

            //NavigationPage.SetHasNavigationBar(this, false);
            
            

            bool hasCameraPermission = await GetCameraPermission();

            testLabel.Text = "Imagen: " + CurrentImageInFrame.FOTO_DESCRIPCION;

            if (hasCameraPermission)
            {
                Console.WriteLine("Camara tiene permisos");
            }
            base.OnAppearing();

        }

        private void OnPictureFinished()
        {
            DisplayAlert("Confirmar", "Foto guardada", "", "Ok");
        }

        private async void OnCameraClicked(object sender, EventArgs e)
        {
            try
            {
                CameraPreview.ObjectItem = CurrentImageInFrame;

            }
            catch (Exception ea)
            {
                Debug.WriteLine($"~(>o.o)> {ea.Message}"); ;
            }

            var resultsStor = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
            
            CameraPreview.CameraClick.Execute(this);

        }

        async Task<bool> GetCameraPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        var result = await DisplayAlert("Camera access needed", "App needs Camera access enabled to work.", "ENABLE", "CANCEL");

                        if (!result)
                            return false;
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Could not access Camera", "App needs Camera access to work. Go to Settings >> App to enable Camera access ", "GOT IT");
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        protected override void OnDisappearing()
        {
            ToggleAccelerometer();
            base.OnDisappearing();

        }

        private async void OnGalleryClicked(object sender, EventArgs e)
        {

            try
            {
                CameraPreview.ObjectItem = CurrentImageInFrame;

            }
            catch (Exception ea)
            {
                Debug.WriteLine($"~(>o.o)> {ea.Message}"); ;
            }

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

            CurrentImageInFrame.CurrentImageSmall = photo.Path;

            MainInstance.Inspeccion.AreasInspeccion[MainInstance.Inspeccion.AreasInspeccion.Count - 2].ListaFotos[MainInstance.Inspeccion.ListaImagenes.IndexOf(CurrentImageInFrame)] = CurrentImageInFrame;

            await Application.Current.MainPage.Navigation.PopAsync();
        }


        //private ImageSource ReduceImage(ImageSource image)
        //{
        //    ImageSource imageSmall = ImageSource.FromResource;
        //}



    }

}