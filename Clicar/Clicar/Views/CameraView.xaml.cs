using Clicar.Interface;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraView : ContentPage
    {

        private MediaFile file;

        private ImageSource ImageSource;

        public CameraView()
        {
            InitializeComponent();
            CameraPreview.PictureFinished += OnPictureFinished;
        }

        protected async override void OnAppearing()
        {

            bool hasCameraPermission = await GetCameraPermission();

            if (hasCameraPermission)
            {
                Console.WriteLine("Camara tiene permisos");
            }
            base.OnAppearing();

        }

        async void OpenCameraAsync()
        {
            Func<object> func = () =>
            {
                var obj = DependencyService.Get<IPhotoOverlay>().GetImageOverlay();
                return obj;
            };

            var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                OverlayViewProvider = func,
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
            });
        }


        private void OnPictureFinished()
        {
            DisplayAlert("Confirm", "Picture Taken", "", "Ok");
        }

        private async void OnCameraClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();  //



            this.file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Small,
                        }
                    );



            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();

                    //UpdateUserImage();
                    return stream;
                });
            }

        }

        private async void OnCameraClicked1(object sender, EventArgs e)
        {
            //var resultsStor = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
            //CameraPreview.CameraClick.Execute(this);

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

    }
}