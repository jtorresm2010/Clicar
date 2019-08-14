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
                gradientDrawable.SetColor(Android.Graphics.Color.Transparent);


                Control.SetBackground(gradientDrawable);
                Control.SetPadding(15, Control.PaddingTop, 15, Control.PaddingBottom);
            }
        }
    }
}