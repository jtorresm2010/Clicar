using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Clicar.ViewModels
{
    public class ReporteViewModel : BaseViewModel
    {
        #region Variables
        private bool IsBusy = false;
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

        public ICommand FinalizarICommand
        {
            get
            {
                return new RelayCommand(FinalizarCommand);
            }
        }

        private async void FinalizarCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PushAsync(new ReportePreliminarView());
            IsBusy = false;
        }


        public ICommand CancelarICommand
        {
            get
            {
                return new RelayCommand(CancelarCommand);
            }
        }

        private async void CancelarCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PopAsync();
            IsBusy = false;
        }

        public ICommand VerResumenICommand
        {
            get
            {
                return new RelayCommand(VerResumenCommand);
            }
        }

        private async void VerResumenCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PushAsync(new ResumenInspeccion());
            IsBusy = false;
        }


        public ICommand FinishICommand
        {
            get
            {
                return new RelayCommand(FinishCommand);
            }
        }
        private async void FinishCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopToRootAsync();

            IsBusy = false;
        }





    }
}
