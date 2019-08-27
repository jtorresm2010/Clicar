using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Behaviors
{
    public class RatingsBehavior : Behavior<View>
    {


        protected override void OnAttachedTo(View entry)
        {
            base.OnAttachedTo(entry);

            ((Editor)entry).TextChanged += Entry_TextChanged;


            // Perform setup
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            double result;
            bool isValid = double.TryParse(e.NewTextValue, out result);
            ((Editor)sender).TextColor = isValid ? Color.Default : Color.Red;

            ((Editor)sender).FontSize = ((Editor)sender).Text.Length;
        }

        protected override void OnDetachingFrom(View entry)
        {
            base.OnDetachingFrom(entry);
            ((Editor)entry).TextChanged -= Entry_TextChanged;
            // Perform clean up
        }



    }
}
