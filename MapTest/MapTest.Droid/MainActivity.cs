using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Org.Osmdroid.Bonuspack.Overlays;
using Org.Osmdroid.Tileprovider.Tilesource;
using Org.Osmdroid.Util;
using Org.Osmdroid.Views;

namespace MapTest.Droid {
    [Activity (Label = "MapTest.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity {

        int count = 1;

        private MapView mapView;
        private MapController mapController;
        private GeoPoint position;
        private IList<GeoPoint> routePoints;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);
            routePoints = new List<GeoPoint> ();
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            position = new GeoPoint (51.7189205, 8.7575093);

            mapView = FindViewById<MapView> (Resource.Id.mapview);
            mapView.SetTileSource (TileSourceFactory.DefaultTileSource);

            mapView.SetMultiTouchControls (true);
            mapView.TilesScaledToDpi = true;

            //mapView.SetTileSource (new XYTileSource ("OSM",null, 0, 18,256, ".png",
            //new[] {"http://c.tile.stamen.com/watercolor/"}));


            mapController = (MapController) mapView.Controller;
            mapController.SetZoom (13);

            // var centreOfMap = new GeoPoint(51496994, -134733);
            var centreOfMap = new GeoPoint (position.Latitude, position.Longitude);
            mapController.SetCenter (centreOfMap);

            mapView.Invalidate ();


            MyClass.GetRoute ();


            foreach (GeoLocation w in MyClass.locations)
            {
                var point = new GeoPoint (w.latitude, w.longitude);
                routePoints.Add (point);
            }

            Polyline line = new Polyline (this);
            line.Title = ("Test Line");
            line.Width = (5f);
            line.Color = Color.Blue;
            line.Points = (routePoints);
            line.Geodesic = (true);


            mapView.OverlayManager.Add (line);
        }

    }
}