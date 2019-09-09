using Clicar.Helpers;
using Clicar.ViewModels;
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
    public partial class StarTemplate : StackLayout
    {
        private static MainViewModel MainInstance;


        public readonly BindableProperty IsEditableProperty = BindableProperty.Create(nameof(IsEditable), typeof(bool), typeof(StarTemplate), true);

        public static readonly BindableProperty StarValueProperty = BindableProperty.Create(
        propertyName: "StarValue",
        returnType: typeof(float),
        declaringType: typeof(StarTemplate),
        defaultValue: 0f,
        defaultBindingMode: BindingMode.TwoWay);

        private static void CurrentStarChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var starView = (StarTemplate)bindable;
            var value = (float)newValue;

            MainInstance.Reporte.CurrentStarRating = value;
            
            starView.StarValue = value;
        }



        public static readonly BindableProperty TextVisibleProperty = BindableProperty.Create(nameof(TextVisible), typeof(bool), typeof(StarTemplate), true);



        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }
        public float StarValue
        {
            get { return (float)GetValue(StarValueProperty); }
            set { SetValue(StarValueProperty, value); }
        }

        public bool TextVisible
        {
            get { return (bool)GetValue(TextVisibleProperty); }
            set { SetValue(TextVisibleProperty, value); }
        }
        private Color       baseOrange;
        private ImageSource StarFill, StarBorder;
        public StarTemplate()
        {
            InitializeComponent();

            MainInstance = MainViewModel.GetInstance();

            StarFill =  ImageSource.FromFile("star_black.png");
            StarBorder = ImageSource.FromFile("star_border.png");

            baseOrange = (Color)Application.Current.Resources["BaseOrange"];

            StarValue = MainInstance.Reporte.CurrentStarRating;

        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);


            switch (propertyName)
            {
                case "TextVisible":
                    if(!TextVisible)
                        StarStringValue.IsVisible = false;
                    break;

                case "IsEditable":
                     if (!IsEditable)
                        StarStringValue.TextColor = baseOrange;
                    break;

                case "StarValue":
                    SetStarRating();
                    SetStarStrings();
                    break;
                
                default:
                    break;
            }

        }
        private void SetStar(object sender, EventArgs e)
        {
            if (IsEditable)
            {
                StackLayout layout = (StackLayout)sender;
                Grid grid = (Grid)layout.Parent;
                switch (grid.Children.IndexOf(layout))
            {
                case 5:
                    StarValue = StarValue >= 1f ? 0f : 1f;
                    break;

                case 6:
                    StarValue = StarValue >= 2f ? 1f : 2f;
                    break;

                case 7:
                    StarValue = StarValue >= 3f ? 2f : 3f;
                    break;

                case 8:
                    StarValue = StarValue >= 4f ? 3f : 4f;
                    break;

                case 9:
                    StarValue = StarValue >= 5f ? 4f : 5f;
                    break;

                default:
                    break;
            }
            }
        }
        private void SetStarStrings()
        {
            switch (StarValue)
            {
                case 0:
                    StarStringValue.Text = Languages.Score;
                    break;
                case 1:
                    StarStringValue.Text = Languages.OneStar;
                    break;

                case 2:
                    StarStringValue.Text = Languages.TwoStars;
                    break;

                case 3:
                    StarStringValue.Text = Languages.ThreeStars;
                    break;

                case 4:
                    StarStringValue.Text = Languages.FourStars;
                    break;

                case 5:
                    StarStringValue.Text = Languages.FiveStars;
                    break;

                default:
                    break;
            }
        }
        private void SetStarRating()
        {
            Star1.Source = StarValue >= 1f ? StarFill : StarBorder;
            Star2.Source = StarValue >= 2f ? StarFill : StarBorder;
            Star3.Source = StarValue >= 3f ? StarFill : StarBorder;
            Star4.Source = StarValue >= 4f ? StarFill : StarBorder;
            Star5.Source = StarValue >= 5f ? StarFill : StarBorder;
        }
    }
}