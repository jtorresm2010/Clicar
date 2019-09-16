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
                        //if(item.ITINS_DESCRIPCION.(MainInstance.Inspeccion.AreasInspeccionBase.Find(x => x.AINSP_ID == item.ITINS_ID)))
                        // {
                        //     Debug.WriteLine($"~(>'.')> Item no cambiado {item.ITINS_DESCRIPCION}");
                        // }
                        // else
                        // {
                        //     Debug.WriteLine($"~(>'.')> Item cambiado {item.ITINS_DESCRIPCION}");
                        // }



                       //Debug.WriteLine($"{item.ITINS_DESCRIPCION}");
                        //if(item.ITINS_STATE_ACTIVO.Equals(MainInstance.Inspeccion.AreasInspeccionBase[indice].Items[])
                        var comentario = item.Comentario ?? "Sin Observaciones";

                        var Estado = "";

                        if(!item.ITINS_CONDICION.Equals("Esta presente"))
                            Estado = item.ITINS_STATE_ACTIVO ? "Estado: Malo\n\t" : "Estado: Bueno\n\t";

                        var Activo = "";
                        if(item.ITINS_CONDICION.Equals("Esta presente"))
                            Activo = item.SwitchActive ? "Estado: Activado\n\t" : "Estado: Desactivado\n\t";

                        var Locked = "";
                        Locked = item.ITINS_IS_LOCKED ? "Bloqueado\n\t" : "Estado: Desbloqueado\n\t";

                        var Solucion = "";
                        if (item.Reparar || item.Solucion)
                            Solucion = item.Reparar ? "Solucion: Reparar\n\t" : "Solucion: Sustituir\n\t";

                        var Condicion = "";
                        if (item.Reparar)
                            Condicion = $"Condicion: {item.Condicion}\n\t";

                        var Imagen = "";
                        if (item.ITINS_REQUIERE_FOTO)
                            Imagen = item.Imagen.Equals("File: camara_select_foto.png") ? "Imagen: No," : "Imagen: Si";

                        texto = $"{texto}{item.ITINS_DESCRIPCION}: \n\t{comentario}\n\t{Estado}{Activo}{Solucion}{Condicion}\n";
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
