using Clicar.Models;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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


        public ICommand RechazarICommand
        {
            get
            {
                return new RelayCommand<Inspeccion>(inspeccion => RechazarCommand(inspeccion));
            }

        }

        private async void RechazarCommand(Inspeccion inspeccion)
        {

            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new RechazarPopupView());

        }

        public ICommand InspeccionarICommand
        {
            get
            {
                return new RelayCommand<Inspeccion>(inspeccion => InspeccionarCommand(inspeccion));
            }

        }

        private async void InspeccionarCommand(Inspeccion inspeccion)
        {
            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new InspeccionPopupView());
        }

    }
}
