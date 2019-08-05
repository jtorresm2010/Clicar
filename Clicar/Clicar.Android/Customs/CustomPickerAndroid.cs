using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Clicar.Customs;
using Clicar.Droid.Customs;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerAndroid))]

namespace Clicar.Droid.Customs
{
    class CustomPickerAndroid : PickerRenderer
    {

        public CustomPickerAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {

                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                gradientDrawable.SetCornerRadius(20f);
                

                Control.SetBackground(gradientDrawable);

                Control.Elevation = 6f;
                Control.SetPadding(60, Control.PaddingTop, Control.PaddingRight, 30);
                //Control.Background.SetColorFilter(Android.Graphics.Color.Argb(255, 117, 171, 64), PorterDuff.Mode.SrcAtop);
                //Control.SetHintTextColor(Android.Graphics.Color.Argb(128, 117, 171, 64));
            }
        }



    }
}