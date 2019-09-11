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
    public partial class StateTemplate : StackLayout
    {

        public static readonly BindableProperty IsDamageProperty = BindableProperty.Create(nameof(IsDamage), typeof(string), typeof(StateTemplate), string.Empty/*, propertyChanged: accion*/ );

        //private static void accion(BindableObject bindable, object oldValue, object newValue)
        //{

        //}

        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(StateTemplate), false);


        public string IsDamage
        {
            get { return (string)GetValue(IsDamageProperty); }
            set { SetValue(IsDamageProperty, value); }
        }
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private string StateActive;
        private string StateInactive;


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals("IsDamage"))
            {
                switch (IsDamage)
                {
                    case "Es daño":
                        StateActive = "Daño";
                        StateInactive = "Sin Daño";
                        StateText.Text = StateInactive;
                        break;

                    case "Es Condición":
                        StateActive = "Malo";
                        StateInactive = "Bueno";
                        StateText.Text = StateInactive;
                        break;
                    default:
                        break;
                }
            }

            if (propertyName.Equals("IsActive"))
            {
                SwitchState();
            }
        }


        private async void SwitchState()
        {
            await Task.Delay(100);
            StateText.Text = IsActive ?StateActive  : StateInactive;
        }

        public StateTemplate()
        {
            InitializeComponent();
        }
    }
}