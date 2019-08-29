using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Clicar.Utils
{
    public class Funciones
    {
        public static Thickness SetTitleMargin(double anchoImagen, double anchoIconoDerecha = 0)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var screenwidth = mainDisplayInfo.Width / mainDisplayInfo.Density;
            var margin = (screenwidth / 2) - (anchoImagen / 2) - anchoIconoDerecha;

            Thickness thickness = new Thickness(0, 0, margin, 0);

            return thickness;
        }
    }
}
