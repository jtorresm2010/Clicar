using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clicar.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarTemplate : StackLayout
    {

        ImageSource StarFill, StarHalf, StarBorder;

        public float StarValue { get; private set; }

        public StarTemplate()
        {
            InitializeComponent();
            StarFill =  ImageSource.FromFile("star_black.png");
            StarHalf = ImageSource.FromFile("star_half.png");
            StarBorder = ImageSource.FromFile("star_border.png");
            StarValue = 0f;
        }

        private void SetStar(object sender, EventArgs e)
        {


            Frame frame = (Frame)sender;
            Grid grid = (Grid)frame.Parent;

           
            //Debug.WriteLine(grid.Children.IndexOf((Frame)sender));

            switch (grid.Children.IndexOf(frame))
            {
                case 5:
                    Star1.Source = StarHalf;
                    Star2.Source = StarBorder;
                    Star3.Source = StarBorder;
                    Star4.Source = StarBorder;
                    Star5.Source = StarBorder;
                    StarValue = 0.5f;
                    break;
                case 6:
                    Star1.Source = StarFill;
                    Star2.Source = StarBorder;
                    Star3.Source = StarBorder;
                    Star4.Source = StarBorder;
                    Star5.Source = StarBorder;
                    StarValue = 1f;
                    break;

                case 7:
                    Star1.Source = StarFill;
                    Star2.Source = StarHalf;
                    Star3.Source = StarBorder;
                    Star4.Source = StarBorder;
                    Star5.Source = StarBorder;
                    StarValue = 1.5f;
                    break;
                case 8:
                    Star1.Source = StarFill;
                    Star2.Source = StarFill;
                    Star3.Source = StarBorder;
                    Star4.Source = StarBorder;
                    Star5.Source = StarBorder;
                    StarValue = 2f;
                    break;

                case 9:
                    Star1.Source = StarFill;
                    Star2.Source = StarFill;
                    Star3.Source = StarHalf;
                    Star4.Source = StarBorder;
                    Star5.Source = StarBorder;
                    StarValue = 2.5f;
                    break;
                case 10:
                    Star1.Source = StarFill;
                    Star2.Source = StarFill;
                    Star3.Source = StarFill;
                    Star4.Source = StarBorder;
                    Star5.Source = StarBorder;
                    StarValue = 3f;
                    break;

                case 11:
                    Star1.Source = StarFill;
                    Star2.Source = StarFill;
                    Star3.Source = StarFill;
                    Star4.Source = StarHalf;
                    Star5.Source = StarBorder;
                    StarValue = 3.5f;
                    break;
                case 12:
                    Star1.Source = StarFill;
                    Star2.Source = StarFill;
                    Star3.Source = StarFill;
                    Star4.Source = StarFill;
                    Star5.Source = StarBorder;
                    StarValue = 4f;
                    break;

                case 13:
                    Star1.Source = StarFill;
                    Star2.Source = StarFill;
                    Star3.Source = StarFill;
                    Star4.Source = StarFill;
                    Star5.Source = StarHalf;
                    StarValue = 4.5f;
                    break;
                case 14:
                    Star1.Source = StarFill;
                    Star2.Source = StarFill;
                    Star3.Source = StarFill;
                    Star4.Source = StarFill;
                    Star5.Source = StarFill;
                    StarValue = 5f;
                    break;

                default:
                    break;
            }


        }
    }
}