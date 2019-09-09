using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemInspeccionGridTemplate : AccordionItemView
    {
        public ItemInspeccionGridTemplate()
        {
            InitializeComponent();
        }
    }
}