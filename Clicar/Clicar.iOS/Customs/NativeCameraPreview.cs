using System;
using System.Linq;
using AVFoundation;
using Clicar.Customs;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Clicar.iOS.Customs
{
    public class NativeCameraPreview : UIView
    {
        AVCaptureVideoPreviewLayer previewLayer;
        CameraOptions cameraOptions;

        public event EventHandler<EventArgs> Tapped;

        public AVCaptureSession CaptureSession { get; private set; }
        AVCaptureStillImageOutput stillImageOutput;

        public bool IsPreviewing { get; set; }

        public NativeCameraPreview(CameraOptions options)
        {
            cameraOptions = options;
            IsPreviewing = false;
            Initialize();
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            previewLayer.Frame = rect;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            OnTapped();
        }


        async void TakePhotoButtonTapped(UIButton sender)
        {
            var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
            var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);

            var jpegImageAsNsData = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);
            var jpegAsByteArray = jpegImageAsNsData.ToArray();

            // TODO: Send this to local storage or cloud storage such as Azure Storage.
        }

        protected virtual void OnTapped()
        {

            var eventHandler = Tapped;
            if (eventHandler != null)
            {
                eventHandler(this, new EventArgs());
            }
        }

        void Initialize()
        {
            CaptureSession = new AVCaptureSession();
            previewLayer = new AVCaptureVideoPreviewLayer(CaptureSession)
            {
                Frame = Bounds,
                VideoGravity = AVLayerVideoGravity.ResizeAspectFill
            };

            var videoDevices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);
            var cameraPosition = (cameraOptions == CameraOptions.Front) ? AVCaptureDevicePosition.Back : AVCaptureDevicePosition.Front;
            var device = videoDevices.FirstOrDefault(d => d.Position == cameraPosition);

            if (device == null)
            {
                return;
            }

            stillImageOutput = new AVCaptureStillImageOutput()
            {
                OutputSettings = new NSDictionary()
            };


            NSError error;
            var input = new AVCaptureDeviceInput(device, out error);
            CaptureSession.AddInput(input);
            Layer.AddSublayer(previewLayer);
            CaptureSession.StartRunning();
            IsPreviewing = true;
        }
    }
}
