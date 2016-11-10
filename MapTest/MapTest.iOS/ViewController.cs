using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MapKit;
using CoreLocation;
using UIKit;
using CoreGraphics;
using MapDemo;

namespace MapTest.iOS {
    public partial class ViewController : UIViewController {

        int count = 1;
        MKMapView map;
        MapDelegate mapDelegate;
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

            //map - should be switched with separate reusable map view /OSMMapView/
            MapViewDelegate mapDelegate = new MapViewDelegate();
            map.Delegate = mapDelegate;

            string template = "http://tile.openstreetmap.org/{z}/{x}/{y}.png";
            MKTileOverlay overlay = new MKTileOverlay(template);
            overlay.CanReplaceMapContent = true;
            map.AddOverlay(overlay, MKOverlayLevel.AboveLabels);

            // Center the map, for development purposes
            MKCoordinateRegion region = map.Region;
            region.Span.LatitudeDelta = 0.05;
            region.Span.LongitudeDelta = 0.05;
            region.Center = new CLLocationCoordinate2D(51.7166700, 8.7666700);
            map.Region = region;

            // Disable rotation programatically because value of designer is somehow ignored
            map.RotateEnabled = false;

            /*map.MapType = MKMapType.Standard;
            map.ShowsUserLocation = true;
            map.ZoomEnabled = true;
            map.ScrollEnabled = true;
            double lat = 51.72070116;
            double lon = 8.74880791;
            CLLocationCoordinate2D mapCenter = new CLLocationCoordinate2D (lat, lon);
            MKCoordinateRegion mapRegion = MKCoordinateRegion.FromDistance (mapCenter, 100, 100);
            map.CenterCoordinate = mapCenter;
            map.Region = mapRegion;
            mapDelegate = new MapViewDelegate ();
            map.Delegate = mapDelegate;
            map.AddAnnotations (new ConferenceAnnotation ("Evolve Conference", mapCenter));*/

          /*  using (var stream = new FileInfo(@"mnt/shared/TMP/map_01.pbf").OpenRead())
            {
                MyClass.Do(stream);
            }

            foreach (GeoLocation w in MyClass.locations)
            {
                var point = new CLLocationCoordinate2D(w.latitude, w.longitude);
                routePoints.Add(point);
            }



            MKPolygon hotelOverlay = MKPolygon.FromCoordinates (routePoints.ToArray ());

            map.AddOverlay (hotelOverlay);*/

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

    }


    class MapViewDelegate : MKMapViewDelegate
    {


        public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
        {
            if (overlay is MKTileOverlay)
            {
                var renderer = new MKTileOverlayRenderer((MKTileOverlay)overlay);
                return renderer;
            }
            else
            {
                return null;
            }
        }
    }
}