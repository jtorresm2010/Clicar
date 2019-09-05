using Clicar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Templates
{
    class AgendaTemplateSelector : DataTemplateSelector
    {


        public DataTemplate ItemTemplateA { get; set; }

        public DataTemplate ItemTemplateB { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentItem = (Inspeccion)item;
            var template = new DataTemplate();

            if(currentItem.Estado.Equals("Anulado"))
                template = ItemTemplateB;
            else
                template = ItemTemplateA;

            return template;
        }
    }
}
