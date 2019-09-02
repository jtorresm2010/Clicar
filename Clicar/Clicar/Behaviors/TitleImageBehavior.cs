using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Clicar.Behaviors
{
    class TitleImageBehavior : Behavior<Image>
    {

        public static readonly BindableProperty ControlViewProperty = BindableProperty.Create(nameof(ControlView), typeof(View), typeof(TitleImageBehavior), null);
        public View ControlView
        {
            get { return (View)GetValue(ControlViewProperty); }
            set { SetValue(ControlViewProperty, value); }
        }

        Image currentImage;

        protected override void OnAttachedTo(Image image)
        {
            base.OnAttachedTo(image);

            currentImage = image;

            if(ControlView == null)
                currentImage.Margin = SetTitleMargin(currentImage.WidthRequest);
            else
                ControlView.SizeChanged += ControlViewSizeChanged;
        }
        private void ControlViewSizeChanged(object sender, EventArgs e)
        {
            currentImage.Margin = SetTitleMargin(currentImage.WidthRequest, ControlView.WidthRequest);
        }
        private Thickness SetTitleMargin(double anchoImagen, double anchoIconoDerecha = 0)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenwidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
            var margin = (screenwidth / 2) - (anchoImagen / 2) - anchoIconoDerecha;
            Thickness thickness = new Thickness(0, 0, margin, 0);
            return thickness;
        }
        protected override void OnDetachingFrom(Image image)
        {
            try
            {
                ControlView.SizeChanged -= ControlViewSizeChanged;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            base.OnDetachingFrom(image);
        }


    }
}
