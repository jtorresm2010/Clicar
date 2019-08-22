using System;
using Clicar.Interface;
using Clicar.iOS.Customs;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(PhotoOverlayIOS))]
namespace Clicar.iOS.Customs
{
    public class PhotoOverlayIOS : IPhotoOverlay
    {
        public PhotoOverlayIOS()
        {
        }

        public object GetImageOverlay(string image)
        {
            var imageView = new UIImageView(UIImage.FromBundle(image));
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            var screen = UIScreen.MainScreen.Bounds;
            imageView.Frame = screen;

            return imageView;
        }

    }
}
