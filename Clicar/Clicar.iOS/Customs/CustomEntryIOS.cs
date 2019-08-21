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


                //Agrega linea inferior
                /*
                CALayer _line = new CALayer
                {
                    BorderColor = UIColor.FromRGB(117, 171, 64).CGColor,
                    BackgroundColor = UIColor.FromRGB(117, 171, 64).CGColor,
                    Frame = new CGRect(0, Frame.Height * 0.95, Frame.Width, 1f)
                };
                textField.Layer.AddSublayer(_line);


                //Agrega margenes
                textField.LeftView = new UIView(new CGRect(0, 0, 40, 0));
                textField.LeftViewMode = UITextFieldViewMode.Always;

                Control.RightView = new UIView(new CGRect(0, 0, 40, 0));
                Control.RightViewMode = UITextFieldViewMode.Always;
                */

                //Cambia el color del texto Hint
                textField.AttributedPlaceholder = new NSAttributedString(
                    textField.AttributedPlaceholder.Value,
                    foregroundColor:UIColor.FromRGBA(117,171,64,128)
                    );
                textField.VerticalAlignment = UIControlContentVerticalAlignment.Center;

                
            }
        }
    }
}
