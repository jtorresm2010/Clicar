using System;
using System.Drawing;
using Clicar.iOS.Customs;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(CustomFrameIOS))]
namespace Clicar.iOS.Customs
{
    public class CustomFrameIOS: FrameRenderer
    {


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            base.OnElementChanged(e);
            var elem = this.Element;
            if (elem != null)
            {

                // Border
                this.Layer.CornerRadius = (float)elem.CornerRadius;

                // Shadow
                this.Layer.ShadowColor = UIColor.DarkGray.CGColor;
                this.Layer.ShadowOpacity = 0.6f;
                this.Layer.ShadowRadius = 2.0f;
                this.Layer.ShadowOffset = new SizeF(0, 0);
                //this.Layer.MasksToBounds = true;

            }
        }

    }
}