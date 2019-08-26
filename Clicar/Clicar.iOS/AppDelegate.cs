﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clicar.Interface;
using Clicar.iOS.Customs;
using Foundation;
using UIKit;

namespace Clicar.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(168, 168, 168);
            LoadApplication(new App());

            Xamarin.Forms.DependencyService.Register<IPhotoOverlay, PhotoOverlayIOS>();


            var fontList = new StringBuilder();
            var familyNames = UIFont.FamilyNames;
            foreach (var familyName in familyNames)
            {
                fontList.Append(String.Format("Family: {0}\n", familyName));
                Console.WriteLine("Family: {0}\n", familyName);
                var fontNames = UIFont.FontNamesForFamilyName(familyName);
                foreach (var fontName in fontNames)
                {
                    Console.WriteLine("\tFont: {0}\n", fontName);
                    fontList.Append(String.Format("\tFont: {0}\n", fontName));
                }
            };




            Plugin.InputKit.Platforms.iOS.Config.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}
