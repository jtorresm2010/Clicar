using System;
using System.ComponentModel;
using System.Drawing;
using Clicar.Customs;
using Clicar.iOS.Customs;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryIOS))]
namespace Clicar.iOS.Customs
{
    public class CustomEntryIOS : EntryRenderer
    {
        public CustomEntryIOS()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                
                UITextField textField = Control;
                textField.BorderStyle = UITextBorderStyle.None;
                textField.AttributedPlaceholder = new NSAttributedString(
                    textField.AttributedPlaceholder.Value,
                    UIFont.FromName(Element.FontFamily, (float)Element.FontSize),
                    foregroundColor: UIColor.FromRGBA(117, 171, 64, 128)
                    );
                textField.VerticalAlignment = UIControlContentVerticalAlignment.Center;

                
            }
        }
    }
}
