using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Clicar.Customs;
using Clicar.Droid.Customs;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorAndroid))]

namespace Clicar.Droid.Customs
{
    class CustomEditorAndroid : EditorRenderer
    {
        public CustomEditorAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {

                var gradientDrawable = new GradientDrawable();
                //gradientDrawable.SetBounds(10, 0, 0, 2);
                ////gradientDrawable.SetStroke(3, Android.Graphics.Color.Argb(128, 117, 171, 64));
                gradientDrawable.SetColor(Android.Graphics.Color.Transparent);


                Control.SetBackground(gradientDrawable);

                //Control.Background.SetColorFilter(Android.Graphics.Color.Argb(255, 117, 171, 64), PorterDuff.Mode.SrcAtop);
                //Control.SetHintTextColor(Android.Graphics.Color.Argb(128, 117, 171, 64));
                //Control.SetPadding(100, Control.PaddingTop, 115, 30);
            }
        }
    }
}