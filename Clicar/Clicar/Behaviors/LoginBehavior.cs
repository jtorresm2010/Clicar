using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Behaviors
{
    public class LoginBehavior : Behavior<View>
    {


        public static readonly BindableProperty ControlViewProperty =
            BindableProperty.Create(nameof(ControlView), typeof(Button), typeof(LoginBehavior), null);

        public Button ControlView
        {
            get { return (Button)GetValue(ControlViewProperty); }
            set { SetValue(ControlViewProperty, value); }
        }


        protected override void OnAttachedTo(View entry)
        {
            base.OnAttachedTo(entry);

            ((Entry)entry).TextChanged += Entry_TextChanged;
            ControlView.IsEnabled = false;
            ((Entry)entry).TextColor = Color.Red;
            // Perform setup
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isEnabled = ((Entry)sender).Text.Length > 0;


            ((Entry)sender).TextColor = isEnabled ? Color.Default : Color.Red;
            ControlView.IsEnabled = isEnabled ? true : false;

            //((Editor)sender).FontSize = ((Editor)sender).Text.Length;
        }

        protected override void OnDetachingFrom(View entry)
        {
            base.OnDetachingFrom(entry);
            ((Entry)entry).TextChanged -= Entry_TextChanged;
            // Perform clean up
        }



    }
}
