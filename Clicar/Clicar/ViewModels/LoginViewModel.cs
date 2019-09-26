using Clicar.Helpers;
using Clicar.Models;
using Clicar.Services;
using Clicar.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Fingerprint;
using Rg.Plugins.Popup.Services;
using Clicar.Utils;
using Clicar.Templates;

namespace Clicar.ViewModels
{
    public class PassRecovery
    {
        public string USU_USERNAME { get; set; }
    }
    public class LoginViewModel : BaseViewModel
    {

        #region Variables
        private RestService restService;
        MainViewModel MainInstance;
        private bool isLoading;
        private string usuario;
        private string clave;
        LoginResponse loginResponse;
        private bool isBusy;
        private string recoveryEmail;
        private bool figerprintAvailable;
        #endregion

        #region Propiedades
        public string Usuario
        {
            get { return usuario; }
            set { SetValue(ref usuario, value); }
        }
        public string Clave
        {
            get { return clave; }
            set { SetValue(ref clave, value); }
        }
        public string RecoveryEmail
        {
            get { return recoveryEmail; }
            set { SetValue(ref recoveryEmail, value); }
        }
        public bool FigerprintAvailable
        {
            get { return figerprintAvailable; }
            set { SetValue(ref figerprintAvailable, value); }
        }
        public bool IsIdle
        {
            get { return isBusy; }
            set { SetValue(ref isBusy, value); }
        }
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetValue(ref isLoading, value); }
        }
        #endregion

        public LoginViewModel()
        {
            IsIdle = true;
            IsLoading = false;

            //Usuario = "palarcon@a.cl";
            //clave = "123456";


            FeatureAvailable();

            MainInstance = MainViewModel.GetInstance();
            restService = new RestService();
        }

        private async void FeatureAvailable()
        {
            FigerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync();
        }

        #region ICommands
        public ICommand LoginICommand
        {
            get
            {
                return new RelayCommand(LoginCommand);
            }
        }

        public ICommand FingerprintICommand
        {
            get
            {
                return new RelayCommand(FingerprintCommand, () => FigerprintAvailable);
            }
        }
        public ICommand RecuperarClaveIcommand
        {
            get
            {
                return new RelayCommand(RecuperarClavecommand);
            }
        }
        #endregion

        private void LoginCommand()
        {
            LoginCommand(Usuario, Clave);
        }

        private async void LoginCommand(string correo, string clave)
        {
            if (!IsIdle)
                return;


            IsIdle = false;
            IsLoading = true;

            var popup = PopupNavigation.Instance;

            if (correo == null)
            {
                await popup.PushAsync(new AlertPopup("Error", "Ingrese un correo", Languages.Accept));
                Usuario = "";
                IsIdle = true;
                IsLoading = false;
                return;
            }

            if (clave == null)
            {
                await popup.PushAsync(new AlertPopup("Error", "Ingrese una contraseña", Languages.Accept));
                Clave = "";
                IsIdle = true;
                IsLoading = false;
                return;
            }

            if (correo.Length > 100)
            {
                await popup.PushAsync(new AlertPopup("Error", "Correo ingresado excede el máximo de caractéres", Languages.Accept));
                IsIdle = true;
                IsLoading = false;
                return;
            }

            if (clave.Length > 10 || clave.Length < 6)
            {
                await popup.PushAsync(new AlertPopup("Error", "Contraseña debe ser de 6 a 10 caractéres", Languages.Accept));
                IsIdle = true;
                IsLoading = false;
                return;
            }

            if (!Funciones.IsValidEmail(correo))
            {
                await popup.PushAsync(new AlertPopup("Error", "Ingrese un correo válido", Languages.Accept));
                IsIdle = true;
                IsLoading = false;
                return;
            }

            if (!Funciones.IsValidPassword(clave))
            {
                await popup.PushAsync(new AlertPopup("Error", "Ingrese una contraseña válida", Languages.Accept));
                IsIdle = true;
                IsLoading = false;
                return;
            }

            var connection = restService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await popup.PushAsync(new AlertPopup("Error", connection.Message, Languages.Accept));
                IsIdle = true;
                IsLoading = false;
                return;
            }

            var usuario = new Usuario
            {
                USU_USERNAME = correo,
                USU_CLAVE = clave,
                ORIGEN = "mobile"
            };

            var response = await restService.PostAsync<LoginResponse>(MainInstance.Url, MainInstance.Prefix, MainInstance.ControllerLogin, usuario);

            try
            {
                loginResponse = (LoginResponse)response.Result;

                if (loginResponse.Resultado)
                {
                    Preferences.Set("Token", loginResponse.Mensaje);
                    Preferences.Set("Correo", correo);
                    Preferences.Set("Clave", clave);

                    InicializarDatos();
                }
            }
            catch (Exception ex)
            {
                await popup.PushAsync(new AlertPopup("Error", "Credenciales de inicio de sesión no válidas", Languages.Accept));
                IsIdle = true;
                IsLoading = false;
                return;
            }

            //IsLoading = false;
            //IsIdle = true;
        }

        private async void InicializarDatos()
        {
            IsLoading = true;

            MainInstance.Agenda = new AgendaViewModel();


            await MainInstance.DataService.Insert<Maestro>(loginResponse.Elemento);
            var maestroFromBD = await MainInstance.DataService.GetMaestro();

            MainInstance.Agenda.MaestroName(maestroFromBD);


            MainInstance.Config = new ConfigViewModel();

            await GetListSucursales();

            await GetClosestSucursal();

            ObtenerAreasInspeccion();

            IsLoading = false;

            Application.Current.MainPage = new ConfigView();
        }

        private async Task GetListSucursales()
        {
            IsLoading = true;

            var connection = restService.CheckConnection();
            if (!connection.IsSuccess)
            {
                var popup = PopupNavigation.Instance;
                await popup.PushAsync(new AlertPopup("", connection.Message, Languages.Accept));
                return;
            }

            var response = await restService.GetAsync<SucursalesResponse>(
                MainInstance.Url,
                MainInstance.Prefix,
                MainInstance.ControllerSucursal);

            try
            {
                SucursalesResponse resp = (SucursalesResponse)response.Result;

                if (resp.Resultado)
                {

                    foreach(Sucursal succ in resp.Elemento)
                    {
                        var succDB = new SucursalDB
                        {
                            MAESU_ID = succ.MAESU_ID,
                            MAESU_NOMBRE_SUCURSAL = succ.MAESU_NOMBRE_SUCURSAL,
                            MAESU_DIRECCION = succ.MAESU_DIRECCION,
                            MAESU_MAE_CIUDAD = succ.MAESU_MAE_CIUDAD,
                            MAESU_HABILITAR_FESTIVOS = succ.MAESU_HABILITAR_FESTIVOS,
                            MAESU_LATITUD = succ.MAESU_LATITUD,
                            MAESU_LONGITUD = succ.MAESU_LONGITUD,
                            MAESU_EMAIL = succ.MAESU_EMAIL,
                            MAESU_ACTIVO = succ.MAESU_ACTIVO,
                            MAESU_DISTANCE_USU = succ.MAESU_DISTANCE_USU,
                        };

                        await MainInstance.DataService.Insert<SucursalDB>(succDB);
                    }

                    var sucursalesFromDB = await MainInstance.DataService.GetAllSucursales();
                    
                    MainInstance.Config.Sucursales = new ObservableCollection<SucursalDB>(sucursalesFromDB);
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error" + ex.Message);
            }

            //IsLoading = false;
            //IsBusy = true;
        }

        private async Task GetClosestSucursal()
        {
            IsLoading = true;

            var connection = restService.CheckConnection();
            if (!connection.IsSuccess)
            {
                var popup = PopupNavigation.Instance;
                await popup.PushAsync(new AlertPopup("", connection.Message, Languages.Accept));
                return;
            }


            var loc = await GetLocation();

            dTOUbicacion posicion =  new dTOUbicacion();

            if (loc == null)
            {
                posicion.LATITUD = -33.364778;
                posicion.LONGITUD = -70.679101;
            }
            else
            {
                posicion.LATITUD = loc.Latitude;
                posicion.LONGITUD = loc.Longitude;
            }

            var response = await restService.PostAsync<SucursalesResponse>(
                MainInstance.Url,
                MainInstance.Prefix,
                MainInstance.ControllerCercanaSucursal,
                posicion);

            try
            {
                SucursalesResponse resp = (SucursalesResponse)response.Result;

                if (resp.Resultado)
                {
                    var sucursalesFromDB = await MainInstance.DataService.GetAllSucursales();

                    var closestSucursal =
                        from sucursal in sucursalesFromDB
                        where sucursal.MAESU_ID == resp.Elemento[0].MAESU_ID
                        select sucursal;

                    MainInstance.Config.ClosestSucursal = closestSucursal.FirstOrDefault();
                    MainInstance.Config.IsReady = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error closest succ " + ex.Message);
            }

            //IsBusy = true;
            //IsLoading = false;
        }

        private async Task<Location> GetLocation()
        {
            Location location = null;

            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return location;
        }

        private async void ObtenerAreasInspeccion()
        {
            IsLoading = true;

            string data = $"?UsuID={loginResponse.Elemento.USU_ID}";

            var response = await restService.GetAsync<AreasResponse>(MainInstance.Url, MainInstance.Prefix, MainInstance.ControllerMaestros, data);
            
            try
            {
                AreasResponse resp = (AreasResponse)response.Result;

                if (resp.Resultado)
                {
                    //Se obtienen las areas
                    if (resp.Elemento.areas_inspeccion.Count > 0)
                    {
                        var currentareas = await MainInstance.DataService.GetAreasInspeccion();
                        try
                        {
                            foreach(AreasInspeccion area in resp.Elemento.areas_inspeccion)
                            {
                                await MainInstance.DataService.Delete<AreasInspeccion>(area);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine($"~(>'.')> {e.Message}");
                        }
                        
                        foreach(AreasInspeccion area in resp.Elemento.areas_inspeccion)
                        {
                            try
                            {
                                await MainInstance.DataService.Insert<AreasInspeccion>(area);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine($"~(>.-.)> Error de Insert {e.Message}");
                            }
                        }
                    }

                    //se obtieen los items
                    if(resp.Elemento.items_areas_inspeccion.Count > 0)
                    {
                        var currentItems = await MainInstance.DataService.GetItemsInspeccionDB();

                        foreach(ItemsAreasInspeccionDB itemDel in currentItems)
                        {
                            await MainInstance.DataService.Delete<ItemsAreasInspeccionDB>(itemDel);
                        }


                        foreach (ItemsAreasInspeccion item in resp.Elemento.items_areas_inspeccion)
                        {
                            var itemDB = new ItemsAreasInspeccionDB
                            {
                                ITINS_ID = item.ITINS_ID,
                                ITINS_AINSP_ID = item.ITINS_AINSP_ID,
                                ITINS_DESCRIPCION = item.ITINS_DESCRIPCION,
                                ITINS_CONDICION = item.ITINS_CONDICION,
                                ITINS_DESHABILITAR = item.ITINS_DESHABILITAR,
                                ITINS_REQUIERE_FOTO = item.ITINS_REQUIERE_FOTO,
                                ITINS_ORDEN_APP = item.ITINS_ORDEN_APP,
                                ITINS_ACTIVO = item.ITINS_ACTIVO
                            };

                            try
                            {
                                await MainInstance.DataService.Insert<ItemsAreasInspeccionDB>(itemDB);
                            }
                            catch (Exception e)
                            {

                                Debug.WriteLine($"~(>'.')> {e.Message}");
                            }
                        }
                    }

                    //Se obtienen detalles de los items (daño)
                    if (resp.Elemento.itemsdetalleinspeccion.Count > 0)
                    {

                        var currentdetalle = await MainInstance.DataService.GetDetallesInspeccion();

                        foreach (ItemsdetalleinspeccionDB detalle in currentdetalle)
                        {
                            await MainInstance.DataService.Delete<ItemsdetalleinspeccionDB>(detalle);
                        }

                        foreach (Itemsdetalleinspeccion newDetalle in resp.Elemento.itemsdetalleinspeccion)
                        {

                            var detalleDB = new ItemsdetalleinspeccionDB
                            {
                                IDINSP_ID = newDetalle.IDINSP_ID,
                                IDINSP_DEINSP_ID = newDetalle.IDINSP_DEINSP_ID,
                                IDINSP_DESCRIPCION = newDetalle.IDINSP_DESCRIPCION,
                                IDINSP_FECHA_CREACION = newDetalle.IDINSP_FECHA_CREACION
                            };



                            try
                            {
                                await MainInstance.DataService.Insert<ItemsdetalleinspeccionDB>(detalleDB);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine($"~(>.-.)> Error de Insert {e.Message}");
                            }
                        }
                    }


                    //Obtener fotografias
                    if (resp.Elemento.fotografias.Count > 0)
                    {

                        var currentImages = await MainInstance.DataService.GetImagenesDB();

                        foreach (FotografiaDB foto in currentImages)
                        {
                            await MainInstance.DataService.Delete<FotografiaDB>(foto);
                        }



                        var array = resp.Elemento.fotografias.ToArray();
                        var list = array.Select(p => new FotografiaDB
                        {
                            FOTO_ID = p.FOTO_ID,
                            FOTO_TIPOF_ID = p.FOTO_TIPOF_ID,
                            FOTO_DESCRIPCION = p.FOTO_DESCRIPCION,
                            FOTO_REQUIERE_MARCO = p.FOTO_REQUIERE_MARCO,
                            FOTO_MARCO = p.FOTO_MARCO,
                            FOTO_ACTIVO = p.FOTO_ACTIVO,
                            FOTO_OBLIGATORIA = p.FOTO_OBLIGATORIA,
                            FOTO_BASE64 = p.FOTO_BASE64
                            //CLCAR_TIPO_FOTOGRAFIA = imgType.Where(a => a.TIPOF_ID == p.CLCAR_TIPO_FOTOGRAFIA_ID).FirstOrDefault()

                        }).ToList();



                        foreach(TipoFotografia tipo in resp.Elemento.tipo_fotografias)
                        {
                            await MainInstance.DataService.Insert<TipoFotografia>(tipo);
                        }



                        foreach (FotografiaDB fotoN in list)
                        {


                            //var newfotoDB = new FotografiaDB
                            //{
                            //    IDINSP_ID = newDetalle.IDINSP_ID,
                            //    IDINSP_DEINSP_ID = newDetalle.IDINSP_DEINSP_ID,
                            //    IDINSP_DESCRIPCION = newDetalle.IDINSP_DESCRIPCION,
                            //    IDINSP_FECHA_CREACION = newDetalle.IDINSP_FECHA_CREACION
                            //};



                            try
                            {
                                await MainInstance.DataService.Insert<FotografiaDB>(fotoN);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine($"~(>.-.)> Error de Insert {e.Message}");
                            }
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error" + ex.Message);
            }

            IsIdle = true;
            IsLoading = false;
        }

        private async void FingerprintCommand()
        {
            if (!IsIdle)
                return;

            IsIdle = false;

            var popup = PopupNavigation.Instance;
            await popup.PushAsync(new FingerPrintPopupView());

            var result = await CrossFingerprint.Current.AuthenticateAsync("Valide su huella para continuar");
            if (result.Authenticated)
            {
                await popup.PopAsync();
                IsIdle = true;

                

                var usuarioGuardado = Preferences.Get("Correo", "");
                var claveGuardada = Preferences.Get("Clave", "");


                if (!usuarioGuardado.Equals("") && !claveGuardada.Equals(""))
                {
                    LoginCommand(usuarioGuardado, claveGuardada);

                    Preferences.Set("Correo", usuarioGuardado);
                    Preferences.Set("Clave", claveGuardada);

                }
                else
                    await popup.PushAsync(new AlertPopup("Error de autenticación", "No hay datos de sesión disponibles", "Continuar"));


            }
            else
            {
                // not allowed to do secret stuff :(
            }

            IsIdle = true;
        }

        private async void RecuperarClavecommand()
        {
            if (!IsIdle)
                return;
            IsIdle = false;
            var popup = PopupNavigation.Instance;

            if (RecoveryEmail == null)
            {
                await popup.PushAsync(new AlertPopup("Error de autenticación", "Ingrese un correo", "Continuar"));
                //await Application.Current.MainPage.DisplayAlert("Error de autenticación", "Ingrese un correo", "Continuar");
                IsIdle = true;
                return;
            }

            if (!Funciones.EmailIsValid(RecoveryEmail))
            {
                await popup.PushAsync(new AlertPopup("Error de autenticación", "Ingrese un correo válido", "Continuar"));
                //await Application.Current.MainPage.DisplayAlert("Error de autenticación", "Ingrese un correo ", "Continuar");
                IsIdle = true;
                return;
            }

            var passrec = new PassRecovery
            {
                USU_USERNAME = RecoveryEmail
            };

            var reponse = await MainInstance.RestService.PostAsync<object>(MainInstance.Url, MainInstance.Prefix, MainInstance.RecuperaPass, passrec);

            //await Application.Current.MainPage.DisplayAlert("Error de autenticación", "Ingrese un correo válido", "Continuar");
            //Debug.WriteLine($"~(>'.')> Passrec command send");

            await popup.PopAsync();

            IsIdle = true;
        }
    }
}
