using Clicar.Customs;
using Clicar.iOS.Customs;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerIOS))]
namespace Clicar.iOS.Customs
{
    public class CustomPickerIOS : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var element = (CustomPicker)this.Element;

            if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.BackgroundColor = UIKit.UIColor.Cyan;
               
                   
            }
        }
    }
}
