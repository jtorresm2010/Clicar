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
        public static string PasswordRecoveryInfo
        {
            get { return Resource.PasswordRecoveryInfo; }
        }
        public static string EmailPlaceholder
        {
            get { return Resource.EmailPlaceholder; }
        }
        public static string Cancel
        {
            get { return Resource.Cancel; }
        }
        public static string Send
        {
            get { return Resource.Send; }
        }
        public static string ConfigInfoText
        {
            get { return Resource.ConfigInfoText; }
        }
        public static string ConfigCurrentBranch
        {
            get { return Resource.ConfigCurrentBranch; }
        }
        public static string InspectionAccessButton
        {
            get { return Resource.InspectionAccessButton; }
        }
        public static string ConfigPicker
        {
            get { return Resource.ConfigPicker; }
        }
        public static string Inspector
        {
            get { return Resource.Inspector; }
        }
        public static string Pending
        {
            get { return Resource.Pending; }
        }
        public static string Completed
        {
            get { return Resource.Completed; }
        }
        public static string StartTime
        {
            get { return Resource.StartTime; }
        }
        public static string Logout
        {
            get { return Resource.Logout; }
        }
        public static string LogoutConfirmText
        {
            get { return Resource.LogoutConfirmText; }
        }
        public static string Exit
        {
            get { return Resource.Exit; }
        }
    }
}
