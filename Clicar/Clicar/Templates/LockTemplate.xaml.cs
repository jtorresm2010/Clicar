using Clicar.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LockTemplate : StackLayout
    {
        public static readonly BindableProperty LockStateProperty = BindableProperty.Create(nameof(LockState), typeof(bool), typeof(LockTemplate), false, BindingMode.TwoWay);
        public static readonly BindableProperty LockableProperty = BindableProperty.Create(nameof(Lockable), typeof(bool), typeof(LockTemplate), true);

        public bool LockState {
            get { return (bool)GetValue(LockStateProperty); }
            set { SetValue(LockStateProperty, value); }
        }
        public bool Lockable
        {
            get { return (bool)GetValue(LockableProperty); }
            set { SetValue(LockableProperty, value); }
        }

        private Color baseGreyLight;
        private Color baseOrange;
        private Color baseGreen;

        ImageSource LockOrange;
        ImageSource LockGrey;
        ImageSource LockGreen;

        public LockTemplate()
        {
            InitializeComponent();

            baseGreyLight = (Color)Application.Current.Resources["BaseGreyLight"];
            baseOrange = (Color)Application.Current.Resources["BaseOrange"];
            baseGreen = (Color)Application.Current.Resources["BaseGreen"];

            LockOrange = ImageSource.FromFile("lock_solid_orange.png");
            LockGreen = ImageSource.FromFile("lock_solid.png");
            LockGrey = ImageSource.FromFile("lock_solid_grey.png");
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals("Lockable"))
            {
                if (!Lockable)
                {
                    //LockState = true;
                    LockText.TextColor = baseGreyLight;
                    LockIcon.Source = LockGrey;
                }
            }

            if (propertyName.Equals("LockState"))
            {
                SwitchState(LockState);
            }
        }

        private void SwitchState(bool state)
        {
            LockIcon.Source = state ? LockOrange : LockGreen;
            LockText.Text = state ? Languages.LockedText : Languages.LockText;
            LockText.TextColor = state ? baseOrange : baseGreen;
        }

        private void SwitchStateCommand(object sender, EventArgs e)
        {
            if(Lockable)
                LockState = LockState ? false : true ;
        }
    }
}