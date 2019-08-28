using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CustomControls;
using Xamarin.Forms;
using Clicar.Models;
using Xamarin.Forms.Xaml;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Clicar.ViewModels;
using SkiaSharp;
using System.IO;

namespace Clicar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InspeccionView : ContentPage
    {


        public InspeccionView()
        {
            InitializeComponent();

            instance = this;

            var mainInstance = MainViewModel.GetInstance();

            mainInstance.GetNewItemList();

            var areasInspeccion = new ListaAreasInspeccion().GetListaAreas();

            //Ordenar areas segun el valor en Orden
            var areasOrdenadas =
                from areaInspeccion in areasInspeccion
                orderby areaInspeccion.Orden ascending
                select areaInspeccion;







            //Setea el nombre de las imagenes
            foreach(AreaInspeccion area in areasOrdenadas)
            {
                area.Image = "MenuNum" + area.Orden;
            }

            //Setea el dataset delacordion
            AccordionMenu.ItemsSource = areasOrdenadas.ToList<AreaInspeccion>();


            //Abre  el primer panel
            IList<View> ItemsAcordion = AccordionMenu.Children;

            ImageSource image = CreateImage(0);
            image.Id.ToString();

            foreach (View item in ItemsAcordion)
            {
                ((AccordionItemView)item).ActiveLeftImage = new FileImageSource {File = image.Id.ToString()} ;
            }




            var primerItem = (AccordionItemView)AccordionMenu.Children[0];

            primerItem.OpenPanel();
            



        }

        private ImageSource CreateImage(int numero)
        {
            SKSurface surface;
            SKImage image;


            var info = new SKImageInfo(40, 40);
            using (surface = SKSurface.Create(info))
            {
                SKCanvas canvas = surface.Canvas;

                SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.White
                };
                canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);

                image = surface.Snapshot();
            }
            SKData encoded = image.Encode();
            Stream stream = encoded.AsStream();


            return ImageSource.FromStream(() => stream);
        }



        private static InspeccionView instance;

        public static InspeccionView GetInstance()
        {
            if (instance == null)
            {
                instance = new InspeccionView();
            }
            return instance;
        }



    }
}