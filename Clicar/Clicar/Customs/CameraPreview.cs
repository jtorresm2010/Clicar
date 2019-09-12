using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Clicar.Customs
{
    public enum CameraOptions
    {
        Rear,
        Front
    }

    public enum Orientation
    {
        Portrait,
        Landscape
    }

    public class CameraPreview : View
    {

        Command cameraClick;

        public static readonly BindableProperty CameraProperty = BindableProperty.Create(
            propertyName: "Camera",
            returnType: typeof(CameraOptions),
            declaringType: typeof(CameraPreview),
            defaultValue: CameraOptions.Rear);

        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
            propertyName: "Orientation",
            returnType: typeof(Orientation),
            declaringType: typeof(CameraPreview),
            defaultValue: Orientation.Portrait);

        public static readonly BindableProperty ObjectItemProperty = BindableProperty.Create(
            propertyName: "ObjectItem",
            returnType: typeof(object),
            declaringType: typeof(CameraPreview),
            defaultValue: null);

        public object ObjectItem
        {
            get { return (object)GetValue(ObjectItemProperty); }
            set { SetValue(ObjectItemProperty, value); }
        }


        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public CameraOptions Camera
        {
            get { return (CameraOptions)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }

        public Command CameraClick
        {
            get { return cameraClick; }
            set { cameraClick = value; }
        }

        public void PictureTaken()
        {
            PictureFinished?.Invoke();
        }

        public event Action PictureFinished;


    }
}
