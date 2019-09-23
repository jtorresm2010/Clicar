using Clicar.Helpers;
using Clicar.Models;
using Clicar.Templates;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private string clave;

        #endregion




        #region Propiedades
        public string Clave
        {
            get { return clave; }
            set { SetValue(ref clave, value); }
        }

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
        public int IdRespuesta { get; set; }
        #endregion


        public ReporteViewModel()
        {
            MainInstance = MainViewModel.GetInstance();
            CurrentStarRating = 0;
            CorreoUsuario = Preferences.Get("Correo","");
        }

        #region ICommands
        public ICommand CancelarICommand
        {
            get
            {
                return new RelayCommand(CancelarCommand);
            }
        }
        public ICommand VerResumenICommand
        {
            get
            {
                return new RelayCommand(VerResumenCommand);
            }
        }
        public ICommand FinalizarICommand
        {
            get
            {
                return new RelayCommand(FinalizarCommand);
            }
        }
        public ICommand FinishICommand
        {
            get
            {
                return new RelayCommand(FinishCommand);
            }
        }
        public ICommand PassSignICommand
        {
            get
            {
                return new RelayCommand(PassSignCommand);
            }
        }
        public ICommand FingerprintICommand
        {
            get
            {
                return new RelayCommand(FingerprintCommand);
            }
        }
        public ICommand EnviarReporteICommand
        {
            get
            {
                return new RelayCommand(EnviarReporteCommand);
            }
        }

        #endregion


        private async void EnviarReporteCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            var popup = PopupNavigation.Instance;

            var currentclave = Preferences.Get("Clave", "");


            if (Clave == null ||  !Clave.Equals(currentclave))
            {
                await popup.PushAsync(new AlertPopup("Error de autenticación", "Contraseña incorrecta", "Continuar"));
                IsBusy = false;
                return;
            }


            Clave = "";


            EnviarEncabezado();

            //try
            //{
            //    var cuerpo = CrearCuerpoMensaje();
            //    Debug.WriteLine($"~(>'.')> test");
            //    var cuerpoJson = JsonConvert.SerializeObject(cuerpo);
            //    Debug.WriteLine($"~(>'.')> {cuerpoJson}");
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"~(>'n')> {ex.Message}");
            //}






            //Debug.WriteLine($"~(>'.')> Enviando encabezado... confirmando pass {Clave}");

            await popup.PopAsync();

            IsBusy = false;
        }



        private async void PassSignCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new ConfirmarClavePopup());

            IsBusy = false;
        }


        private async void FinalizarCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;




            await Application.Current.MainPage.Navigation.PushAsync(new ReportePreliminarView());

            IsBusy = false;
        }

        private async void CancelarCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            await Application.Current.MainPage.Navigation.PopAsync();
            IsBusy = false;
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

        private async void FinishCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopAsync();

            IsBusy = false;
        }

        private Encabezado CrearEncabezado()
        {
            Encabezado encabezado = new Encabezado
            {
                INSP_ID = null,
                INSP_SOINS_ID = MainInstance.Inspeccion.CurrentInspeccion.SOINS_ID,
                INPS_FECHA_INICIO = MainInstance.Inspeccion.HoraInicio,
                INSP_USU_ID = MainInstance.Agenda.Maestro.USU_ID,
                INSP_FECHA_FIN = MainInstance.Inspeccion.HoraTermino,
                INSP_COMENTARIOS = EvalGeneral,
                INSP_NRO_ESTRELLAS = (int)CurrentStarRating,
                INSP_ESTDO_ID = 1,
            };

            var cuerpoJson = JsonConvert.SerializeObject(encabezado);
            Debug.WriteLine($"~(>'.')> 1: {cuerpoJson}");

            return encabezado;
        }

        private async void EnviarEncabezado()
        {
            var connection = MainInstance.RestService.CheckConnection();
            if (!connection.IsSuccess)
            {
                var popup = PopupNavigation.Instance;
                await popup.PushAsync(new AlertPopup("", connection.Message, Languages.Accept));
                return;
            }

            var enc = CrearEncabezado();

            var response = await MainInstance.RestService.PostAsync<EncabezadoResponse>(
                MainInstance.Url,
                MainInstance.Prefix,
                MainInstance.EnvioInspeccionEncabezado,
                enc);

            var testobj = JsonConvert.SerializeObject(response.Result);
            Debug.WriteLine($"~(>'v')> 2: {testobj}");

            try
            {
                EncabezadoResponse resp = (EncabezadoResponse)response.Result;

                if (resp.Resultado)
                {
                    EnviarCuerpo(resp.Elemento.INSP_ID);


                   // Debug.WriteLine($"~(>^.^)> Encabezado OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error encabezado " + ex.Message);
            }
        }

        private EnvioInspeccion CrearCuerpoMensaje(int insp_id)
        {
            EnvioInspeccion cuerpo = new EnvioInspeccion();

            var listaItems = new List<ItemsInspeccionado>();

            foreach(AccordionItem area in MainInstance.Inspeccion.AreasInspeccion)
            {
                if(area.Items != null)
                {
                    if (area.Items.Count > 0)
                    {
                        foreach(ItemsAreasInspeccionACC item in area.Items)
                        {

                            var INSPECCION_REPARAR = new ClcarInspeccionReparar();
                            var INSPECCION_SUSTITUIR = new ClcarInspeccionSustituir();

                            if (item.ITINS_STATE_ACTIVO)
                            {

                                if (item.Reparar)
                                {
                                    INSPECCION_REPARAR.INSPE_ID = null;
                                    INSPECCION_REPARAR.IREPA_COMENTARIO = item.Condicion;

                                    INSPECCION_SUSTITUIR = null;
                                }
                                else
                                {
                                    INSPECCION_SUSTITUIR.INSPE_ID = null;
                                    INSPECCION_SUSTITUIR.ISUST_COMENTARIO = "";

                                    INSPECCION_REPARAR = null;
                                }
                            }
                            else
                            {
                                INSPECCION_REPARAR = null;
                                INSPECCION_SUSTITUIR = null;
                            }


                            var itemInspeccionados = new ItemsInspeccionado
                            {
                                INSPE_ID = null,
                                INSPE_INSP_ID = insp_id,
                                INSPE_ITINS_ID = item.ITINS_ID,
                                INSPE_OBSERVACION = item.Comentario ?? "",
                                INSPE_DESHABILITADO = item.ITINS_IS_LOCKED ? 1 : 0,
                                CLCAR_INSPECCION_REPARAR = INSPECCION_REPARAR,
                                CLCAR_INSPECCION_SUSTITUIR = INSPECCION_SUSTITUIR
                            };


                            listaItems.Add(itemInspeccionados);
                        }
                    }
                }
            }

            cuerpo.itemsInspeccionados = listaItems;


            var cuerpoJson = JsonConvert.SerializeObject(cuerpo);
            Debug.WriteLine($"~(>^.^)> 3: {cuerpoJson}");


            return cuerpo;
        }

        private async void EnviarCuerpo(int Ins_ID)
        {
            var connection = MainInstance.RestService.CheckConnection();
            if (!connection.IsSuccess)
            {
                var popup = PopupNavigation.Instance;
                await popup.PushAsync(new AlertPopup("", connection.Message, Languages.Accept));
                return;
            }

            var enc = CrearCuerpoMensaje(Ins_ID);


            var response = await MainInstance.RestService.PostAsync<DTOGenerico>(
                MainInstance.Url,
                MainInstance.Prefix,
                MainInstance.EnvioInspeccionCuerpo,
                enc);

            var testobj2 = JsonConvert.SerializeObject(response.Result);
            Debug.WriteLine($"~(>'o')> 4: {testobj2}");


            try
            {
                object resp = (object)response.Result;

                //if (resp)
                //{
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error envio de cuerpo " + ex.Message);
            }
        }

        private async void FingerprintCommand()
        {
            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new FingerPrintPopupView());

            var result = await CrossFingerprint.Current.AuthenticateAsync("Valide su huella para continuar");
            if (result.Authenticated)
            {
                await popup.PopAsync();

                Debug.WriteLine($"~(>'.')> Autenticando a usuario {Preferences.Get("Correo", "---")}");
            }
            else
            {
                // not allowed to do secret stuff :(
            }
        }

    }
}
