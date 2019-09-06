using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Clicar.ViewModels
{
    public class ReporteViewModel : BaseViewModel
    {
        #region Variables
        private float currentStarRating;
        private string evalGeneral;
        private string correoUsuario;
        #endregion

        #region Propiedades
        public float CurrentStarRating
        {
            get { return currentStarRating; }
            set { SetValue(ref currentStarRating, value); }
        }
        public string EvalGeneral
        {
            get { return evalGeneral; }
            set { SetValue(ref evalGeneral, value); }
        }
        public string CorreoUsuario
        {
            get { return correoUsuario; }
            set { SetValue(ref correoUsuario, value); }
        }

        #endregion


        public ReporteViewModel()
        {
            CurrentStarRating = 0;
            CorreoUsuario = Preferences.Get("Correo","");



        }

    }
}
