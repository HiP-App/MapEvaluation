using System;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;

namespace MapTest.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += delegate {
				var title = string.Format ("{0} clicks!", count++);
				Button.SetTitle (title, UIControlState.Normal);
			};
            MyMapView.StyleURL = NSUrl.FromString ("mapbox://styles/babri/civ8a51l6009e2ilcz7e3zrr2");
            MyMapView.SetCenterCoordinate(new CLLocationCoordinate2D(51.7189205, 8.7575093), false);
            MyMapView.SetZoomLevel(11, false);
            
            MyMapView.AddAnnotation(new PointAnnotation
            {
                Coordinate = new CLLocationCoordinate2D(51.7189205, 8.7575093),
                Title = "Sample Marker",
                Subtitle = "This is the subtitle"
            });
        }

        public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

