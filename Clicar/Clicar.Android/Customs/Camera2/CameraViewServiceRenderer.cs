using Android.Content;
using Clicar.Customs;
using Clicar.Droid.Customs.Camera2;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System;
using Android.Graphics;
using Clicar.ViewModels;
using Clicar.Models;
using PCLStorage;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(CameraViewServiceRenderer))]
namespace Clicar.Droid.Customs.Camera2
{
    public class CameraViewServiceRenderer : ViewRenderer<CameraPreview, CameraDroid>
    {

        private CameraDroid _camera;
        private CameraPreview _currentElement;

        public CameraViewServiceRenderer(Context context) : base(context)
        {

        }



        protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            base.OnElementChanged(e);
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

            String folderName ="Clicar" ;
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

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

            IFile file = await folder.CreateFileAsync($"CLCR_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}.Jpeg", CreationCollisionOption.GenerateUniqueName);

            using (Stream stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                stream.Write(bitmapData, 0, bitmapData.Length);
            }


            if (this.Element.ObjectItem != null)
            {
                ((Fotografia)Element.ObjectItem).CurrentImageSmall = file.Path;
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