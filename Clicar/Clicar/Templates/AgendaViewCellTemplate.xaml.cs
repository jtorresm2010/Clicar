using Clicar.ViewModels;
using Clicar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaViewCellTemplate : ViewCell
    {
        AgendaView mainInstance;
        protected override void OnAppearing()
        {
            if (mainInstance.IsLast)
            {
                SeparatorFrame.IsVisible = false;
                mainInstance.IsLast = false;
            }

            base.OnAppearing();
        }


        
        public AgendaViewCellTemplate()
        {
            mainInstance = AgendaView.GetInstance();
            InitializeComponent();
        }
    }
}