using Clicar.Interface;
using Clicar.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }


        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string Welcome
        {
            get { return Resource.Welcome; }
        }
        public static string UsernamePlaceholder
        {
            get { return Resource.UsernamePlaceholder; }
        }
        public static string PasswordPlaceholder
        {
            get { return Resource.PasswordPlaceholder; }
        }
        public static string PasswordRecovery
        {
            get { return Resource.PasswordRecovery; }
        }
        public static string LoginButton
        {
            get { return Resource.LoginButton; }
        }
        public static string TouchIDButton
        {
            get { return Resource.TouchIDButton; }
        }

    }
}
