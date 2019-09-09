using Clicar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Templates
{
    class AccordionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ItemTemplateA { get; set; }

        public DataTemplate ItemTemplateB { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentItem = (AccordionItem)item;
            var template = new DataTemplate();

            if(!currentItem.IsImageSet)
            {
                template = ItemTemplateA;
            }
            else
            {
                template = ItemTemplateB;
            }

            return template;
        }

    }
}
