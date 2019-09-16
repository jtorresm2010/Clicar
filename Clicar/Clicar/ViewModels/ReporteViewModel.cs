using Clicar.Models;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Clicar.ViewModels
{

    public class ResumenFinal
    {
        public string Area { get; set; }
        public string Texto { get; set; }


    }


    public class ReporteViewModel : BaseViewModel
    {
        #region Variables
        private MainViewModel MainInstance;
        private bool IsBusy = false;
        private float currentStarRating;
        private string evalGeneral;
        private string correoUsuario;
        private List<ResumenFinal> resumenFinal;

        #endregion

        #region Propiedades

        public List<ResumenFinal> ResumenFinal
        {
            get { return resumenFinal; }
            set { SetValue(ref resumenFinal, value); }
        }


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
            MainInstance = MainViewModel.GetInstance();
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

            ResumenFinal = new List<ResumenFinal>();

            foreach(AccordionItem area in MainInstance.Inspeccion.AreasInspeccion)
            {
                Debug.WriteLine($"{area.AINSP_DESCRIPCION}");
                string texto = "";

                if (area.Items != null && area.Items.Count > 0)
                {
                    foreach (ItemsAreasInspeccionACC item in area.Items)
                    {
                        Debug.WriteLine($"{item.ITINS_DESCRIPCION}");
                        var comentario = item.Comentario ?? "Sin Observaciones";
                        texto = $"{texto}{item.ITINS_DESCRIPCION}: {comentario}\n";
                    }

                    ResumenFinal.Add(new ResumenFinal { Area = area.AINSP_DESCRIPCION, Texto = texto });

                }


            }















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

            await Application.Current.MainPage.Navigation.PopAsync();
            //await Application.Current.MainPage.Navigation.PopToRootAsync();

            IsBusy = false;
        }





    }
}
