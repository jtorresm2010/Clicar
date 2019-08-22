﻿using Clicar.Interface;
using Clicar.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraView : ContentPage
    {

        private MediaFile file;

        private ImageSource ImageSource;

        public ItemInspeccion iteminspeccion;

        public CameraView()
        {
            InitializeComponent();
            CameraPreview.PictureFinished += OnPictureFinished;
        }

        protected async override void OnAppearing()
        {

            bool hasCameraPermission = await GetCameraPermission();

            testLabel.Text = "Imagen: " + iteminspeccion.Nombre;

            if (hasCameraPermission)
            {
                Console.WriteLine("Camara tiene permisos");
            }
            base.OnAppearing();

        }

        private void OnPictureFinished()
        {
            DisplayAlert("Confirmar", "Foto guardada", "", "Ok");
        }

        private async void OnCameraClicked(object sender, EventArgs e)
        {
            var resultsStor = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
            CameraPreview.CameraClick.Execute(this);

        }

        async Task<bool> GetCameraPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        var result = await DisplayAlert("Camera access needed", "App needs Camera access enabled to work.", "ENABLE", "CANCEL");

                        if (!result)
                            return false;
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                    
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Could not access Camera", "App needs Camera access to work. Go to Settings >> App to enable Camera access ", "GOT IT");
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}