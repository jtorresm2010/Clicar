using Clicar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Templates
{

    class AccordionItemTemplateSelector : DataTemplateSelector
    {

        public DataTemplate ItemTemplateA { get; set; }

        public DataTemplate ItemTemplateB { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentArea = (AreaInspeccion)item;
            var template = new DataTemplate();

            if (currentArea.IsImage)
            {
                template = ItemTemplateB;
            }
            else
            {
                template = ItemTemplateA;
            }

            return template;
        }

    }
}
