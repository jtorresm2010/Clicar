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

namespace Clicar.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region Variables
        private bool isLoading;
        private string usuario;
        private string clave;
        LoginResponse loginResponse;
        private bool isBusy;
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

        private RestService restService;
        MainViewModel MainInstance;

        public LoginViewModel()
        {
            IsIdle = true;
            IsLoading = false;

            usuario = "palarcon";
            clave = "123456";


            MainInstance = MainViewModel.GetInstance();
            restService = new RestService();
        }

        public ICommand LoginICommand
        {
            get
            {
                return new RelayCommand(LoginCommand);
            }
        }

        private async void LoginCommand()
        {
            if (!IsIdle)
                return;


            IsIdle = false;
            IsLoading = true;

            var connection = restService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("", connection.Message, Languages.Accept);
                return;
            }

            var usuario = new Usuario
            {
                USU_USERNAME = Usuario,
                USU_CLAVE = Clave,
                ORIGEN = "mobile"
            };

            var response = await restService.PostAsync<LoginResponse>(MainInstance.Url, MainInstance.Prefix, MainInstance.ControllerLogin, usuario);

            try
            {
                loginResponse = (LoginResponse)response.Result;

                if (loginResponse.Resultado)
                {
                    InicializarDatos();
                }
                else
                {
                    Debug.WriteLine($"~(>'.')> Datos de login incorrectos");
                    IsIdle = true;
                    IsLoading = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error: " + ex.Message);


                IsIdle = true;
                IsLoading = false;
            }

            //IsLoading = false;
            //IsBusy = true;
        }

        private async void InicializarDatos()
        {
            IsLoading = true;

            MainInstance.Agenda = new AgendaViewModel();

            Preferences.Set("Token", loginResponse.Mensaje);
            Preferences.Set("Correo", Usuario);
            Preferences.Set("Clave", Clave);

            await MainInstance.DataService.Insert<Maestro>(loginResponse.Elemento);
            var maestroFromBD = await MainInstance.DataService.GetMaestro();

            MainInstance.Agenda.MaestroName(maestroFromBD);


            MainInstance.Config = new ConfigViewModel();

            GetListSucursales();

            GetClosestSucursal();

            ObtenerAreasInspeccion();

            IsLoading = false;

            Application.Current.MainPage = new ConfigView();
        }

        private async void GetListSucursales()
        {
            IsLoading = true;

            var connection = restService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("", connection.Message, Languages.Accept);
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
                    MainInstance.Config.IsReady = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("~(>-_-)> Error" + ex.Message);
            }

            //IsLoading = false;
            //IsBusy = true;
        }

        private async void GetClosestSucursal()
        {
            IsLoading = true;

            var connection = restService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("", connection.Message, Languages.Accept);
                return;
            }


            var loc = await GetLocation();

            dTOUbicacion posicion =  new dTOUbicacion();

            if (loc == null)
            {
                posicion.LATITUD = 0;
                posicion.LONGITUD = 0;
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

                //if (location != null)
                //{
                //    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                //}
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
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

                    //Se obtienen detalles de lo items (daño)
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
                            //FOTO_MARCO = 
                            FOTO_ACTIVO = p.FOTO_ACTIVO,
                            FOTO_OBLIGATORIA = p.FOTO_OBLIGATORIA
                            //CLCAR_TIPO_FOTOGRAFIA = imgType.Where(a => a.TIPOF_ID == p.CLCAR_TIPO_FOTOGRAFIA_ID).FirstOrDefault()

                        }).ToList();



                        foreach(TipoFotografia tipo in resp.Elemento.tipo_fotografias)
                        {
                            await MainInstance.DataService.Insert<TipoFotografia>(tipo);
                        }



                        foreach (FotografiaDB fotoN in list)
                        {

                            Debug.WriteLine($"~(>'.')> uwu");


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


    }
}
