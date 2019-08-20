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
                /*
                var view = (Element as CustomEntry);
                if (view != null)
                {
                    DrawBorder(view);
                }



            */












                
                UITextField textField = (UITextField)Control;
                textField.BorderStyle = UITextBorderStyle.None;
                //textField.BackgroundColor = UIColor.Black;


                textField.LeftView = new UIView(new CGRect(0, 0, 40, 0));
                textField.LeftViewMode = UITextFieldViewMode.Always;


                textField.AttributedPlaceholder = new NSAttributedString(
                    textField.AttributedPlaceholder.Value,
                    foregroundColor:UIColor.FromRGBA(117,171,64,128)

                    );
                textField.VerticalAlignment = UIControlContentVerticalAlignment.Center;
                // Create borders (bottom only)
                var borderLayer = new CALayer
                {
                    MasksToBounds = true,
                    Frame = new CGRect(0f, Frame.Height * 0.95, Frame.Width * 1.2, 5f),
                    BorderColor = new CGColor(117, 171, 64), //new CGColor(117, 171, 64),
                    BorderWidth = 1.0f
                };


                textField.Layer.AddSublayer(borderLayer);

                textField.Layer.MasksToBounds = true;
              



                //underline effect Control.Background.SetColorFilter(Android.Graphics.Color.Argb(255, 117, 171, 64), PorterDuff.Mode.SrcAtop);
                //Control.SetHintTextColor(Android.Graphics.Color.Argb(128, 117, 171, 64));
                //Control.SetPadding(100, Control.PaddingTop, 115, 30);
            }
        }


        /*
        void DrawBorder(CustomEntry view)
        {
            
        }


    */





    }
}
