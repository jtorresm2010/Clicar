﻿using Clicar.Customs;
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

            if (this.Control != null && this.Element != null)
            {

                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;

                Control.BackgroundColor = UIKit.UIColor.FromRGBA(0,0,0,0);
                //Control.Frame = new CGRect(x: 0, y: 0, width: Frame.Width, height: 60);


            }
        }
    }
}
