using Clicar.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Clicar.Models
{
    public class AccordionItem : BaseViewModel
    {
        private int aINSP_ID;
        public int AINSP_ID
        {
            get { return aINSP_ID; }
            set { SetValue(ref aINSP_ID, value); }
        }

        private string aINSP_DESCRIPCION;

        public string AINSP_DESCRIPCION
        {
            get { return aINSP_DESCRIPCION; }
            set { SetValue(ref aINSP_DESCRIPCION, value); }
        }
        private int aINSP_ORDEN_APP;

        public int AINSP_ORDEN_APP
        {
            get { return aINSP_ORDEN_APP; }
            set { SetValue(ref aINSP_ORDEN_APP, value); }
        }

        private int aINSP_ACTIVO;

        public int AINSP_ACTIVO
        {
            get { return aINSP_ACTIVO; }
            set { SetValue(ref aINSP_ACTIVO, value); }
        }

        private int aINSP_PAIS_ID;

        public int AINSP_PAIS_ID
        {
            get { return aINSP_PAIS_ID; }
            set { SetValue(ref aINSP_PAIS_ID, value); }
        }

        private bool isImageSet;

        public bool IsImageSet
        {
            get { return isImageSet; }
            set { SetValue(ref isImageSet, value); }
        }


        private List<Fotografia> listaFotos;

        public List<Fotografia> ListaFotos
        {
            get { return listaFotos; }
            set { SetValue(ref listaFotos, value); }
        }

        private List<ItemsAreasInspeccionACC> items;

        public List<ItemsAreasInspeccionACC> Items
        {
            get { return items; }
            set { SetValue(ref items, value); }
        }


        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    base.OnPropertyChanged(propertyName);

        //    //if (propertyName.Equals("AreasInspeccion"))
        //    Debug.WriteLine($"~(>'.')> Item cambiado {propertyName}");




        //}


    }
}
