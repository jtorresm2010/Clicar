using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Behaviors
{
    public class LoginBehavior : Behavior<Entry>
    {


        public static readonly BindableProperty ControlViewProperty =
            BindableProperty.Create(nameof(ControlView), typeof(View), typeof(LoginBehavior), null);

        public View ControlView
        {
            get { return (Button)GetValue(ControlViewProperty); }
            set { SetValue(ControlViewProperty, value); }
        }


        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);

            entry.TextChanged += Entry_TextChanged;
            ControlView.IsEnabled = false;
            entry.TextColor = Color.Red;
            // Perform setup
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isEnabled = ((Entry)sender).Text.Length > 0;


            ((Entry)sender).TextColor = isEnabled ? Color.Default : Color.Red;
            ControlView.IsEnabled = isEnabled ? true : false;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.TextChanged -= Entry_TextChanged;
            // Perform clean up
        }



    }
}
