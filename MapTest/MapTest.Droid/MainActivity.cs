using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Text;
using Android.Util;
using Java.IO;
using Java.Lang;
using Java.Nio.Charset;
using Mapbox.Annotations;
using Mapbox.Camera;
using Mapbox.Constants;
using Mapbox.Geometry;
using Mapbox.Maps;
using Org.Json;
using Exception = System.Exception;

namespace MapTest.Droid
{
    [Activity(Label = "MapTest.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, MapboxMap.IOnInfoWindowClickListener {
        int count = 1;

        private MapView mapView;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
            };

            mapView = FindViewById<MapView>(Resource.Id.mapView);
            mapView.OnCreate(bundle);

            var map = await mapView.GetMapAsync();

            var position = new CameraPosition.Builder()
                .Target(new LatLng(51.7189205, 8.7575093)) // Sets the new camera position
                .Zoom(11) // Sets the zoom
                .Build(); // Creates a CameraPosition from the builder

            map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(position), 3000);

            map.AddMarker(new MarkerOptions()
                .SetTitle("Test Marker")
                .SetSnippet ("This is another test")
                .SetPosition(new LatLng(51.7189205, 8.7575093)));
            map.OnInfoWindowClickListener = this;

            var fileInfo = new FileInfo (@"mnt/shared/TMP/map_01.pbf");
            if (fileInfo.Exists)
            {
                AddRoute (fileInfo, map);
            }
            

        }

        private void AddRoute (FileInfo fileInfo, MapboxMap map)
        {
            using (var stream = fileInfo.OpenRead())
            {
                MyClass.Do(stream);
            }

            var list = new List<LatLng>();
            foreach (var w in MyClass.Locations)
            {
                var point = new LatLng(w.Latitude, w.Longitude);
                list.Add(point);
            }

            map.AddPolyline(new PolylineOptions()
                                 .SetColor(Color.Blue)
                                 .SetWidth(5f)
                                 .Add(list.ToArray()));
        }

        protected override void OnPause()
        {
            base.OnPause();
            mapView.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (mapView != null)
                mapView.OnResume();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView.OnLowMemory();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mapView.OnDestroy();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            mapView.OnSaveInstanceState(outState);
        }

        public bool OnInfoWindowClick(Marker marker)
        {
            Dialog d = new Dialog (this);
            
            d.SetTitle ("Click on: " + marker.Title);
            d.Show ();
          
            return true;
        }

    }
}


