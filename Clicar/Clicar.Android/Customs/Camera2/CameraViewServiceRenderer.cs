using Android.Content;
using Clicar.Customs;
using Clicar.Droid.Customs.Camera2;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System;
using Android.Runtime;
using Android.Views;
using Android.Graphics;
using Java.IO;
using Plugin.Media;
using Clicar.ViewModels;
using Clicar.Models;
using PCLStorage;
using Xamarin.Essentials;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(CameraViewServiceRenderer))]
namespace Clicar.Droid.Customs.Camera2
{
    public class CameraViewServiceRenderer : ViewRenderer<CameraPreview, CameraDroid>
    {

        MainViewModel MainInstance;
        private CameraDroid _camera;
        private CameraPreview _currentElement;
        private readonly Context _context;
        private bool hasCameraPermission;

        public CameraViewServiceRenderer(Context context) : base(context)
        {
            _context = context;
        }



        protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            base.OnElementChanged(e);
            MainInstance = MainViewModel.GetInstance();
            _camera = new CameraDroid(Context);

            SetNativeControl(_camera);

            if (e.NewElement != null && _camera != null)
            {
                e.NewElement.CameraClick = new Command(() => TakePicture());
                _currentElement = e.NewElement;
                _camera.SetCameraOption(_currentElement.Camera);
                _camera.Photo += OnPhoto;
            }
        }

        public void TakePicture()
        {
            _camera.LockFocus();
        }

        private async void OnPhoto(object sender, byte[] imgSource)
        {
            var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);

            Bitmap bmp = BitmapFactory.DecodeByteArray(imgSource, 0, imgSource.Length);

            if(this.Element.Orientation == Orientation.Portrait)
            {
                bmp = RotateImage(90, bmp);
            }

            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bmp.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }

            //Preferences.Set("ImageNumber", "0000");

            var numeroImagen = int.Parse(Preferences.Get("ImageNumber", "0000"));

            var fullpath = $"{path}/Clicar/CLCR{numeroImagen + 1}_{this.Element.Orientation.ToString()}.Jpeg";

            System.IO.File.WriteAllBytes(fullpath, bitmapData);

            var numeriINc = numeroImagen + 1;
            Preferences.Set("ImageNumber", numeriINc.ToString());

            System.Console.WriteLine(fullpath);

            if (this.Element.ObjectItem != null)
            {
                ((Fotografia)Element.ObjectItem).CurrentImageSmall = fullpath;

                //await MainInstance.DataService.Insert<FotografiaLocal>(new FotografiaLocal { LOCAL_IMAGERUTA = fullpath , ITEM_CORRESP = ((Fotografia)this.Element.ObjectItem).FOTO_ID });

            }

            Device.BeginInvokeOnMainThread(() =>
            {
                _currentElement?.PictureTaken();
            });
        }


        private Bitmap RotateImage(int angle, Bitmap bitmapSrc)
        {
            Matrix matrix = new Matrix();
            matrix.PostRotate(angle);
            return Bitmap.CreateBitmap(bitmapSrc, 0, 0,
                bitmapSrc.Width, bitmapSrc.Height, matrix, true);
        }


        protected override void Dispose(bool disposing)
        {
            _camera.Photo -= OnPhoto;

            base.Dispose(disposing);
        }
    }
}