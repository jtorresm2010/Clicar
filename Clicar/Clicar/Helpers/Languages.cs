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


        public static string Error => Resource.Error;
        public static string Welcome => Resource.Welcome;
        public static string UsernamePlaceholder => Resource.UsernamePlaceholder;
        public static string PasswordPlaceholder => Resource.PasswordPlaceholder;
        public static string PasswordRecovery => Resource.PasswordRecovery;
        public static string LoginButton => Resource.LoginButton;
        public static string TouchIDButton => Resource.TouchIDButton;
        public static string PasswordRecoveryInfo => Resource.PasswordRecoveryInfo;
        public static string EmailPlaceholder => Resource.EmailPlaceholder;
        public static string Cancel => Resource.Cancel;
        public static string Send => Resource.Send;
        public static string ConfigInfoText => Resource.ConfigInfoText;
        public static string ConfigCurrentBranch => Resource.ConfigCurrentBranch;
        public static string InspectionAccessButton => Resource.InspectionAccessButton;
        public static string ConfigPicker => Resource.ConfigPicker;
        public static string Inspector => Resource.Inspector;
        public static string Pending => Resource.Pending;
        public static string Completed => Resource.Completed;
        public static string StartTime => Resource.StartTime;
        public static string Logout => Resource.Logout;
        public static string LogoutConfirmText => Resource.LogoutConfirmText;
        public static string Exit => Resource.Exit;
        public static string Accept => Resource.Accept;
        public static string ConnectError => Resource.ConnectError;
        public static string InspectionNo => Resource.InspectionNo;
        public static string CarBrand => Resource.CarBrand;
        public static string CarModel => Resource.CarModel;
        public static string CarColor => Resource.CarColor;
        public static string CarYear => Resource.CarYear;
        public static string CarVersion => Resource.CarVersion;
        public static string CarMileage => Resource.CarMileage;
        public static string CarType => Resource.CarType;
        public static string ClientName => Resource.ClientName;
        public static string ClientID => Resource.ClientID;
        public static string Reject => Resource.Reject;
        public static string StartInspection => Resource.StartInspection;
        public static string RejectInfoText => Resource.RejectInfoText;
        public static string Confirm => Resource.Confirm;
        public static string Authentication => Resource.Authentication;
        public static string ConfirmInfoText => Resource.ConfirmInfoText;
        public static string VinEntryPlaceholder => Resource.VinEntryPlaceholder;
        public static string Transmission => Resource.Transmission;
        public static string Automatic => Resource.Automatic;
        public static string Mechanic => Resource.Mechanic;
        public static string Access => Resource.Access;
        public static string LockText => Resource.LockText;
        public static string LockedText => Resource.LockedText;
        public static string State => Resource.State;
        public static string Edit => Resource.Edit;
        public static string Back => Resource.Back;
        public static string Continue => Resource.Continue;
        public static string FinalAppraisal => Resource.FinalAppraisal;
        public static string FinalAppraisalInfoText => Resource.FinalAppraisalInfoText;
        public static string StarRatingInfoText => Resource.StarRatingInfoText;
        public static string Score => Resource.Score;
        public static string Conclude => Resource.Conclude;
        public static string PrelimReport => Resource.PrelimReport;
        public static string EndTime => Resource.EndTime;
        public static string InspectionTime => Resource.InspectionTime;
        public static string ExpectedTime => Resource.ExpectedTime;
        public static string CurrentRecord => Resource.CurrentRecord;
        public static string CurrentMedian => Resource.CurrentMedian;
        public static string InspectionSummary => Resource.InspectionSummaryBtn;
        public static string PasswordSign => Resource.PasswordSign;
        public static string TouchIDSign => Resource.TouchIDSign;
        public static string InspectionSummaryTitle => Resource.InspectionSummaryTitle;
        public static string CarRating => Resource.CarRating;
        public static string InspectorComment => Resource.InspectorComment;
        public static string CompletedBy => Resource.CompletedBy;
        public static string Close => Resource.Close;
        public static string OneStar => Resource.OneStar;
        public static string TwoStars => Resource.TwoStars;
        public static string ThreeStars => Resource.ThreeStars;
        public static string FourStars => Resource.FourStars;
        public static string FiveStars => Resource.FiveStars;
    }
}
