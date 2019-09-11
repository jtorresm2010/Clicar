using Clicar.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Clicar.Behaviors
{
    class LockButtonBehavior : Behavior<View>
    {
        MainViewModel MainInstance;

        public static readonly BindableProperty LockedProperty = BindableProperty.Create(
            propertyName: "Locked",
            declaringType: typeof(LockButtonBehavior),
            returnType: typeof(bool),
            defaultValue: false
            );

        public bool Locked
        {
            get { return (bool)GetValue(LockedProperty); }
            set { SetValue(LockedProperty, value); }
        }

        protected override void OnAttachedTo(View view)
        {
            base.OnAttachedTo(view);
            ((Button)view).Clicked += OnButtonClick;

            MainInstance = MainViewModel.GetInstance();

        }

        private async void OnButtonClick(object sender, EventArgs e)
        {
            Locked = false;
            ((Button)sender).IsEnabled = false;

            await Task.Delay(500);

            Locked = MainInstance.Login.IsIdle;
            ((Button)sender).IsEnabled = Locked;
            //Locked = false;
        }

        protected override void OnDetachingFrom(View view)
        {
            base.OnDetachingFrom(view);
            ((Button)view).Clicked -= OnButtonClick;
        }


    }
}
