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
        AgendaView agendaInstance;
        public AgendaViewCellTemplate()
        {
            agendaInstance = AgendaView.GetInstance();
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (agendaInstance.IsLast)
            {
                SeparatorFrame.IsVisible = false;
                agendaInstance.IsLast = false;
            }

            base.OnAppearing();
        }
    }
}