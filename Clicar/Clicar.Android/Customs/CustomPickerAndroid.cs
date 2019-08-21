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
using Android.Support.V4.Content;
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


        CustomPicker element;

        public CustomPickerAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (CustomPicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                Control.Background = AddPickerStyles();
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                //Control.Elevation = 6f;
                //Control.SetPadding(60, 30, 60, 30);

        }

        public ShapeDrawable AddPickerStyles()
        {
            ShapeDrawable background = new ShapeDrawable();
            background.Paint.Color = Android.Graphics.Color.Transparent;

            return background;
        }
        public LayerDrawable AddPickerStyles(string imagePath)
        {
            ShapeDrawable border = new ShapeDrawable();
            border.Paint.Color = Android.Graphics.Color.Transparent;

            var gradientDrawable = new GradientDrawable();
            gradientDrawable.SetColor(Android.Graphics.Color.White);
            
            gradientDrawable.SetCornerRadius(20f);



            Drawable[] layers = { gradientDrawable, border, GetDrawable(imagePath) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }


        private BitmapDrawable GetDrawable(string imagePath)
        {

            //int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            //var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var drawable = Resources.GetDrawable(imagePath);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 70, 70, true));
            result.Gravity = Android.Views.GravityFlags.Right;
            

            return result;
        }


        //protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        //{
        //    base.OnElementChanged(e);
        //    if (e.OldElement == null)
        //    {

        //        var gradientDrawable = new GradientDrawable();
        //        gradientDrawable.SetColor(Android.Graphics.Color.White);
        //        gradientDrawable.SetCornerRadius(20f);


        //        Control.SetBackground(gradientDrawable);

        //        Control.Elevation = 6f;
        //        Control.SetPadding(60, Control.PaddingTop, Control.PaddingRight, 30);
        //        //Control.Background.SetColorFilter(Android.Graphics.Color.Argb(255, 117, 171, 64), PorterDuff.Mode.SrcAtop);
        //        //Control.SetHintTextColor(Android.Graphics.Color.Argb(128, 117, 171, 64));
        //    }
        //}



    }
}