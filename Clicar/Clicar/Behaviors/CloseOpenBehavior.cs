using Clicar.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Behaviors
{
    class CloseOpenBehavior : Behavior<View>
    {

        public static readonly BindableProperty ListViewProperty = BindableProperty.Create(nameof(AttachedListView), typeof(ListView), typeof(CloseOpenBehavior), null);

        public ListView AttachedListView
        {
            get { return (ListView)GetValue(ListViewProperty); }
            set { SetValue(ListViewProperty, value); }
        }


        TapGestureRecognizer itemTapped;

        private double ListHeight;


        protected override void OnAttachedTo(View view)
        {
            base.OnAttachedTo(view);
            itemTapped = new TapGestureRecognizer();
            ((Frame)view).GestureRecognizers.Add(itemTapped);
            itemTapped.Tapped += RefreshCommand;

            if (AttachedListView == null)
                Debug.WriteLine("~(>'.')> Lista no especificada");
            else
                AttachedListView.SizeChanged += ListViewSizeChanged;


            

        }

        private void ListViewSizeChanged(object sender, EventArgs e)
        {
            ListHeight = AttachedListView.HeightRequest;
            AttachedListView.SizeChanged -= ListViewSizeChanged;
        }

        private void RefreshCommand(object sender, EventArgs e)
        {
            if(AttachedListView == null)
            {
                Debug.WriteLine("~(>'.')> Lista no seteada");
                return;
            }
            else
            {
                CloseOpenAnimation(AttachedListView, ListHeight, AttachedListView.IsVisible);
            }
        }


        protected override void OnDetachingFrom(View view)
        {
            base.OnDetachingFrom(view);
        }

        private void CloseOpenAnimation(ListView listView, double listHeight, bool IsVisible)
        {

            Action<double> callback = input =>
            {
                if (!listView.IsVisible)
                {
                    listView.IsVisible = true;
                }

                listView.HeightRequest = input;
            };
            Action<double, bool> endAction = (x, y) => { listView.IsVisible = IsVisible; };
            uint rate = 16;
            uint length = 400;
            double startingHeight = 0;
            double endingHeight = 0;
            Easing easing = Easing.Linear;

            
            if (IsVisible)
            {
                startingHeight = listHeight;
                endingHeight = 0;
                IsVisible = false;

            }
            else
            {
                startingHeight = 0;
                endingHeight = listHeight;
                IsVisible = true;
            }

            listView.Animate("ListSize", callback, startingHeight, endingHeight, rate, length, easing , endAction);
        }
    }
}
