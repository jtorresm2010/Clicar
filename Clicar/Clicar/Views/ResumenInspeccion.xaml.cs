using Clicar.Utils;
using Plugin.DeviceOrientation;
using Plugin.DeviceOrientation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResumenInspeccion : ContentPage
    {
        public ResumenInspeccion()
        {
            CrossDeviceOrientation.Current.LockOrientation(DeviceOrientations.Portrait);
            InitializeComponent();
        }


    }
}