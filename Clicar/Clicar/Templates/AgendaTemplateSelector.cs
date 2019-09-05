using Clicar.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Templates
{
    class AgendaTemplateSelector : DataTemplateSelector
    {


        public DataTemplate ItemTemplateA { get; set; }

        public DataTemplate ItemTemplateB { get; set; }

        public DataTemplate ItemTemplateC { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentItem = (Inspeccion)item;
            var template = new DataTemplate();

            switch (currentItem.Estado)
            {
                case "Pendiente":
                    template = ItemTemplateA;
                    break;
                case "Completado":
                    template = ItemTemplateB;
                    break;
                case "Rechazado":
                    template = ItemTemplateC;
                    break;
                default:
                    break;
            }


            return template;
        }
    }
}
