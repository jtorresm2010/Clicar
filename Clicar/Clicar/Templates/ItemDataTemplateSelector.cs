using Clicar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Templates
{
    class ItemDataTemplateSelector : DataTemplateSelector
    {


        public DataTemplate ItemTemplateA { get; set; }

        public DataTemplate ItemTemplateB { get; set; }

        public DataTemplate ItemTemplateC { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentItem = (ItemsAreasInspeccion)item;
            var template = new DataTemplate();

            switch (currentItem.ITINS_CONDICION)
            {
                case "Esta presente":
                    template = ItemTemplateA;
                    break;
                case "Es Condición":
                    template = ItemTemplateB;
                    break;
                case "Es daño":
                    template = ItemTemplateC;
                    break;
                default:
                    break;
            }

            return template;
        }
    }
}
