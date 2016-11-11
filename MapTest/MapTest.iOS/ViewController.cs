using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MapKit;
using CoreLocation;
using Foundation;
using UIKit;

namespace MapTest.iOS {
    public partial class ViewController : UIViewController {

        int count = 1;
        MKMapView map;
        private CLLocationManager locationManager;
        private IList<CLLocationCoordinate2D> routePoints;

        public ViewController (IntPtr handle) : base (handle)
        {
            routePoints = new List<CLLocationCoordinate2D> ();
        }


        public override void LoadView ()
        {
            map = new MKMapView (UIScreen.MainScreen.Bounds);
            locationManager = new CLLocationManager ();
            View = map;
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            map.OverlayRenderer = OverlayRenderer;
            string template = "http://tile.openstreetmap.org/{z}/{x}/{y}.png";
            MKTileOverlay overlay = new MKTileOverlay (template);
            overlay.CanReplaceMapContent = true;
            map.AddOverlay (overlay);

            // Center the map, for development purposes
            MKCoordinateRegion region = map.Region;
            region.Span.LatitudeDelta = 0.05;
            region.Span.LongitudeDelta = 0.05;
            region.Center = new CLLocationCoordinate2D (51.7166700, 8.7666700);
            map.Region = region;

            // Disable rotation programatically because value of designer is somehow ignored
            map.RotateEnabled = false;

            Assembly assembly = Assembly.GetExecutingAssembly ();
            Stream file = assembly.GetManifestResourceStream ("MapTest.iOS.Resources.map_01.pbf");

            MyClass.Do (file);


            foreach (GeoLocation w in MyClass.locations)
            {
                var point = new CLLocationCoordinate2D (w.latitude, w.longitude);
                routePoints.Add (point);
            }

            var line = MKPolyline.FromCoordinates (routePoints.ToArray ());
            map.AddOverlay (line);
            //map.SetVisibleMapRect(line.BoundingMapRect, false);

            if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0))
            {
                locationManager.RequestWhenInUseAuthorization ();
            }
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        public MKOverlayRenderer OverlayRenderer (MKMapView mapView, IMKOverlay overlay)
        {
            if (overlay is MKTileOverlay)
            {
                var renderer = new MKTileOverlayRenderer ((MKTileOverlay) overlay);
                return renderer;
            }
            else if (overlay is MKPolyline)
            {
                MKPolylineRenderer polylineRenderer = new MKPolylineRenderer((MKPolyline)overlay);
                polylineRenderer.FillColor = UIColor.Blue;
                polylineRenderer.StrokeColor = UIColor.Blue;
                polylineRenderer.LineWidth = 5f;
                return polylineRenderer;
            }
            return null;
        }

    }

}