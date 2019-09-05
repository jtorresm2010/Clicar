using Clicar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.ViewModels
{
    public class DetalleInspeccionViewModel : BaseViewModel
    {
        #region Variables
        private Inspeccion currentInspeccion;
        #endregion

        #region Propiedades
        public Inspeccion CurrentInspeccion
        {
            get { return currentInspeccion; }
            set { SetValue(ref currentInspeccion, value); }
        }


        #endregion

        public DetalleInspeccionViewModel()
        {

        }

    }
}
