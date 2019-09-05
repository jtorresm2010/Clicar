using Clicar.Helpers;
using Clicar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Clicar.Services
{
    class RestService
    {
        public readonly HttpClient client;
        //private readonly DataService dataService;

        public RestService()
        {
            //this.dataService = new DataService();

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    this.client = new HttpClient();
                    this.client.DefaultRequestHeaders.Accept.Clear();
                    this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ERROR Creando Conexion ", ex.Message);
                }
            }

        }
        public Response CheckConnection()
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.ConnectError,
                };
            }

            return new Response
            {
                IsSuccess = true,
            };
        }

        public async Task<Response> PostAsync<T>(string urlBase, string prefix, string controller, object data = null)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                var answer = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var loginResponse = JsonConvert.DeserializeObject<T>(answer);

                return new Response
                {
                    IsSuccess = true,
                    Result = loginResponse,
                    Message = answer
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }

        public async Task<Response> GetAsync<T>(string urlBase, string prefix, string controller, string data = "", string token = "")
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";

                var response = await client.GetAsync($"{url}{data}");

                var answer = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);

                return new Response
                {
                    IsSuccess = true,
                    Result = obj
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
