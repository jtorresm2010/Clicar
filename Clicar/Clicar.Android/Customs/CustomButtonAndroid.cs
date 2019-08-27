using Clicar.Customs;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ClicarApp.Droid.Customs;

[assembly: ExportRenderer(typeof(Button), typeof(CustomButtonAndroid))]

namespace ClicarApp.Droid.Customs
{
    class CustomButtonAndroid : ButtonRenderer
    {
        public CustomButtonAndroid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;
            this.Control.SetAllCaps(false);
        }

    }
}