using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Clicar.Utils
{
    public class Funciones
    {

        static Regex ValidEmailRegex = CreateValidEmailRegex();

        public static Thickness SetTitleMargin(double anchoImagen, double anchoIconoDerecha = 0)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenwidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
            var margin = (screenwidth / 2) - (anchoImagen / 2) - anchoIconoDerecha;
            Thickness thickness = new Thickness(0, 0, margin, 0);
            return thickness;
        }

        public static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

        public static bool IsValidEmail(string email)
        {
            var expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            return Regex.IsMatch(email, expresion) && Regex.Replace(email, expresion, String.Empty).Length == 0;
        }


        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }


    }
}
