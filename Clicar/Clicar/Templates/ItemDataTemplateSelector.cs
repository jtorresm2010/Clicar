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
            var currentItem = (ItemInspeccion)item;
            var template = new DataTemplate();

            switch (currentItem.Tipo)
            {
                case "1":
                    template = ItemTemplateA;
                    break;
                case "2":
                    template = ItemTemplateB;
                    break;
                case "3":
                    template = ItemTemplateB;
                    break;
                default:
                    break;
            }

            return template;
        }
    }
}
