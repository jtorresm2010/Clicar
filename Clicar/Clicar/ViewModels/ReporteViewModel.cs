﻿using Clicar.Customs;
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


        private bool subiendoDatos;

        public bool SubiendoDatos
        {
            get { return subiendoDatos; }
            set { SetValue(ref subiendoDatos, value); }
        }



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
        public ICommand ContinuarReporteICommand
        {
            get
            {
                return new RelayCommand(ContinuarReporteCommand);
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

        private async void ContinuarReporteCommand()
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

            //var cuerpoJson = JsonConvert.SerializeObject(encabezado);
            //Debug.WriteLine($"~(>'.')> 1: {cuerpoJson}");

            return encabezado;
        }

        private async void EnviarEncabezado()
        {
            //SubiendoDatos = true;
            var popup = PopupNavigation.Instance;
            //await popup.PushAsync(new CargandoPopup());




            var connection = MainInstance.RestService.CheckConnection();
            if (!connection.IsSuccess)
            {
                //var popup = PopupNavigation.Instance;
                await popup.PushAsync(new AlertPopup("", connection.Message, Languages.Accept));
                return;
            }

            var enc = CrearEncabezado();

            var response = await MainInstance.RestService.PostAsync<EncabezadoResponse>(
                MainInstance.Url,
                MainInstance.Prefix,
                MainInstance.EnvioInspeccionEncabezado,
                enc);

            //var testobj = JsonConvert.SerializeObject(response.Result);
            //Debug.WriteLine($"~(>'v')> 2: {testobj}");

            try
            {
                EncabezadoResponse resp = (EncabezadoResponse)response.Result;

                if (resp.Resultado)
                {
                    EnviarCuerpo(resp.Elemento.INSP_ID);
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


            //var cuerpoJson = JsonConvert.SerializeObject(cuerpo);
            //Debug.WriteLine($"~(>^.^)> 3: {cuerpoJson}");


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


            var response = await MainInstance.RestService.PostAsync<RespuestaEnvioInspeccion>(
                MainInstance.Url,
                MainInstance.Prefix,
                MainInstance.EnvioInspeccionCuerpo,
                enc);

            //var testobj2 = JsonConvert.SerializeObject(response.Result);
            //Debug.WriteLine($"~(>'o')> 4: {testobj2}");

            try
            {
                var resp = (RespuestaEnvioInspeccion)response.Result;

                SubidaDeImagenes(Ins_ID, resp.Elemento);
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
                EnviarEncabezado();

                await popup.PopAsync();

                //Debug.WriteLine($"~(>'.')> Autenticando a usuario {Preferences.Get("Correo", "---")}");
            }
            else
            {
                // not allowed to do secret stuff :(
            }
        }


        private EnvioFotos CrearListaImagenes(int inspeccionID, List<ItemsInspeccionado> respuesta)
        {
            var envioFotos = new EnvioFotos();

            var fotosInspeccion = new List<FotosInspeccion>();
            var fotosItemInspeccion = new List<FotositemsInspeccion>();

            envioFotos.fotosInspeccion = fotosInspeccion;

            envioFotos.fotositemsInspeccion = fotosItemInspeccion;


            foreach (AccordionItem area in MainInstance.Inspeccion.AreasInspeccion)
            {
                if (area.ListaFotos != null && area.ListaFotos.Count > 0)
                {
                    foreach (Fotografia foto in area.ListaFotos)
                    {
                        if (!foto.CurrentImageSmall.Equals("camara_select_foto.png"))
                        {
                            byte[] bytes = File.ReadAllBytes(foto.CurrentImageSmall);

                            var fotoItem = new FotosInspeccion
                            {
                                FINSP_FECHA_CREACION = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} {Math.Floor(DateTime.Now.TimeOfDay.TotalHours)}:{DateTime.Now.TimeOfDay.Minutes}",
                                FINSP_FOTO_ID = foto.FOTO_ID,
                                FINSP_NOMBRE_ARCHIVO = Path.GetFileName(foto.CurrentImageSmall),
                                FINSP_INSP_ID = inspeccionID,
                                FINSP_ARCHIVO_BASE64 = Convert.ToBase64String(bytes)
                            };

                            envioFotos.fotosInspeccion.Add(fotoItem);
                        }
                    }

                }

                if (area.Items != null && area.Items.Count > 0)
                {
                    foreach (ItemsAreasInspeccionACC item in area.Items)
                    {
                        if (item.ITINS_REQUIERE_FOTO && !item.Imagen.Equals("camara_select_foto.png"))
                        {
                            ItemsInspeccionado result = respuesta.Find(x => x.INSPE_ITINS_ID == item.ITINS_ID);

                            byte[] bytes = File.ReadAllBytes(item.Imagen);

                            var fotoitem = new FotositemsInspeccion
                            {
                                FIINS_FECHA_CREACION = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} {Math.Floor(DateTime.Now.TimeOfDay.TotalHours)}:{DateTime.Now.TimeOfDay.Minutes}",
                                FIINS_NOMBRE_ARCHIVO = Path.GetFileName(item.Imagen),
                                INSPE_ID = (int)result.INSPE_ID,
                                FIINS_ARCHIVO_BASE64 = Convert.ToBase64String(bytes)
                            };

                            envioFotos.fotositemsInspeccion.Add(fotoitem);
                        }
                    }
                }
            }

            if (envioFotos.fotosInspeccion.Count == 0)
                envioFotos.fotosInspeccion = null;

            if (envioFotos.fotositemsInspeccion.Count == 0)
                envioFotos.fotositemsInspeccion = null;

            return envioFotos;
        }

        private async void SubirFotos(EnvioFotos envioFotos)
        {
            var connection = MainInstance.RestService.CheckConnection();
            if (!connection.IsSuccess)
            {
                var popup = PopupNavigation.Instance;
                await popup.PushAsync(new AlertPopup("", connection.Message, Languages.Accept));
                return;
            }

            //Debug.WriteLine($"~(>'.')> Carga iniciada");

            var response = await MainInstance.RestService.PostAsync<DTOGenerico>(
                MainInstance.Url,
                MainInstance.Prefix,
                MainInstance.EnvioFotoInspeccion,
                envioFotos);

            try
            {
                if (response.IsSuccess)
                {
                    //SubiendoDatos = false;

                    //var popup = PopupNavigation.Instance;
                    //await popup.PopAllAsync();
                    //Debug.WriteLine($"~(>'.')> imagen subida");


                    //if (resp)
                    //{
                    //}
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error envio de cuerpo " + ex.Message);
            }
        }

        private async void SubidaDeImagenes(int inspeccionID, List<ItemsInspeccionado> respuesta)
        {
            //Debug.WriteLine($"~(>'.')> Iniciando Carga");


            MainInstance.Agenda.LoadMainList();
            await Application.Current.MainPage.Navigation.PopToRootAsync();




            /*Crear lista de imagenes*/
            var envioFotos = CrearListaImagenes(inspeccionID, respuesta);

            var conteoFotos = envioFotos.fotosInspeccion == null ? 0 : envioFotos.fotosInspeccion.Count;
            var conteofotosItems = envioFotos.fotositemsInspeccion == null ? 0 : envioFotos.fotositemsInspeccion.Count;




            var totalItems = conteoFotos + conteofotosItems;
            int currentItem = 0;

            if(envioFotos.fotosInspeccion != null && envioFotos.fotosInspeccion.Count > 0)
            {
                foreach (var foto in envioFotos.fotosInspeccion)
                {
                    var envioFotoIndividual = new EnvioFotos
                    {
                        fotosInspeccion = new List<FotosInspeccion>(),
                        fotositemsInspeccion = null
                    };

                    envioFotoIndividual.fotosInspeccion.Add(foto);

                    /*subir usando api*/
                    SubirFotos(envioFotoIndividual);
                    currentItem++;


                    if (currentItem == totalItems)
                        DependencyService.Get<IMessage>().LongAlert("Carga de imagenes Finalizada");
                }
            }

            if (envioFotos.fotositemsInspeccion != null && envioFotos.fotositemsInspeccion.Count > 0)
            {
                foreach (var foto in envioFotos.fotositemsInspeccion)
                {
                    var envioFotoIndividual = new EnvioFotos
                    {
                        fotosInspeccion = null,
                        fotositemsInspeccion = new List<FotositemsInspeccion>()
                    };

                    envioFotoIndividual.fotositemsInspeccion.Add(foto);

                    /*subir usando api*/
                    SubirFotos(envioFotoIndividual);

                    currentItem++;

                    if(currentItem == totalItems)
                        DependencyService.Get<IMessage>().LongAlert("Carga de imagenes Finalizada");
                }
            }

            ///*subir usando api*/
            //SubirFotos(envioFotos);

        }



    }
}
