using Android.Content;
using Clicar.Droid.Customs;
using Clicar.Customs;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryAndroid))]

namespace Clicar.Droid.Customs
{
    class CustomEntryAndroid : EntryRenderer
    {
        public CustomEntryAndroid(Context context) : base(context)
        {
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {

                var gradientDrawable = new GradientDrawable();
                //gradientDrawable.SetBounds(10, 0, 0, 2);
                //gradientDrawable.SetStroke(3, Android.Graphics.Color.Argb(128, 117, 171, 64));
                gradientDrawable.SetColor(Android.Graphics.Color.Transparent);

                
                Control.SetBackground(gradientDrawable);

                //underline Control.Background.SetColorFilter(Android.Graphics.Color.Argb(255, 117, 171, 64), PorterDuff.Mode.SrcAtop);
                Control.SetHintTextColor(Android.Graphics.Color.Argb(128, 117, 171, 64));
                //padding Control.SetPadding(100, Control.PaddingTop, 115, 30);
            }
        }

    }
}