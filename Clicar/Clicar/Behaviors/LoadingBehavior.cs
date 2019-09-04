using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Clicar.ViewModels;

namespace Clicar.Behaviors
{
    class LoadingBehavior : Behavior<View>
    {

        public static readonly BindableProperty ControlViewProperty = BindableProperty.Create(nameof(ControlView), typeof(View), typeof(LoginBehavior), null);

        public View ControlView
        {
            get { return (View)GetValue(ControlViewProperty); }
            set { SetValue(ControlViewProperty, value); }
        }

        TapGestureRecognizer itemTapped;
        protected override void OnAttachedTo(View sender)
        {
            base.OnAttachedTo(sender);
            itemTapped = new TapGestureRecognizer();
            ((Grid)sender).GestureRecognizers.Add(itemTapped);
            itemTapped.Tapped += RefreshCommand;
        }

        //animacion para un ciclo de giro
        private async void RefreshCommand1(object sender, EventArgs e)
        {
            var parent = (Grid)sender;
            var imageF = (Image)parent.Children[0];
            var imageB = (Image)parent.Children[1];

            uint intervalo = 300;

            parent.IsEnabled = false;
            ((ScrollView)ControlView).IsEnabled = false;

            await Task.WhenAll(
                ((ScrollView)ControlView).FadeTo(0.5, intervalo),
                imageF.FadeTo(0, intervalo, Easing.CubicIn),
                imageB.FadeTo(1, intervalo, Easing.CubicIn),
                imageF.RotateTo(imageF.Rotation + 90, intervalo, Easing.CubicIn),
                imageB.RotateTo(imageB.Rotation + 90, intervalo, Easing.CubicIn)
                );

            await Task.WhenAll(
                ((ScrollView)ControlView).FadeTo(1, intervalo),
                imageF.FadeTo(1, intervalo, Easing.CubicIn),
                imageB.FadeTo(0, intervalo, Easing.CubicIn),
                imageF.RotateTo(imageF.Rotation + 90, intervalo, Easing.CubicOut),
                imageB.RotateTo(imageB.Rotation + 90, intervalo, Easing.CubicOut)
                );

            ((ScrollView)ControlView).IsEnabled = true;
            parent.IsEnabled = true;
        }

        //Test de animacion para carga continua
        private async void RefreshCommand(object sender, EventArgs e)
        {
            var parent = (Grid)sender;
            var imageF = (Image)parent.Children[0];
            var imageB = (Image)parent.Children[1];

            uint intervalo = 600;

            parent.IsEnabled = false;

            MainViewModel.GetInstance().Agenda.ListLoading = false;
            //((ScrollView)ControlView).IsEnabled = false;


            //comienza a girar
            await Task.WhenAll(
                ((ScrollView)ControlView).FadeTo(0.5, intervalo / 2),
                imageF.FadeTo(0, intervalo, Easing.SinIn),
                imageB.FadeTo(1, intervalo, Easing.SinIn),
                imageF.RotateTo(imageF.Rotation + 180, intervalo, Easing.Linear),
                imageB.RotateTo(imageB.Rotation + 180, intervalo, Easing.Linear)
                );

            //sigue girando por los ciclos necesarios
            for (int i = 0; i < 5; i++)
            {

                await Task.WhenAll(
                    imageF.RotateTo(imageF.Rotation + 180, intervalo, Easing.Linear),
                    imageB.RotateTo(imageB.Rotation + 180, intervalo, Easing.Linear)
                    );
            }

            //termina el giro
            await Task.WhenAll(
                ((ScrollView)ControlView).FadeTo(1, intervalo),
                imageF.FadeTo(1, intervalo),
                imageB.FadeTo(0, intervalo),
                imageF.RotateTo(imageF.Rotation + 180, intervalo, Easing.Linear),
                imageB.RotateTo(imageB.Rotation + 180, intervalo, Easing.Linear)
                );


            MainViewModel.GetInstance().Agenda.ListLoading = true;
            //((ScrollView)ControlView).IsEnabled = true;
            parent.IsEnabled = true;
        }

        protected override void OnDetachingFrom(View sender)
        {
            base.OnDetachingFrom(sender);
            itemTapped.Tapped -= RefreshCommand;
        }
    }
}
