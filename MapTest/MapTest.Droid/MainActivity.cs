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
            
            /*
            using (var stream = new FileInfo (@"mnt/shared/TMP/map_01.pbf").OpenRead ())
            {
                var geoJson = MyClass.Do (stream);

                var list = BuildIt (geoJson);
                map.AddPolyline (new PolylineOptions ()
                                     .SetColor(Color.ParseColor("#3bb2d0"))
                                     .SetWidth(2)
                                     .AddAll (list));
            }*/
        }

        private IIterable BuildIt(string geoJson)
        {

            List<LatLng> points = new List<LatLng>();

            try
            {
                // Parse JSON
                JSONObject json = new JSONObject(geoJson);
                JSONArray features = json.GetJSONArray("features");
                JSONObject feature = features.GetJSONObject(0);
                JSONObject geometry = feature.GetJSONObject("geometry");
                if (geometry != null)
                {
                    string type = geometry.GetString("type");

                    // Our GeoJSON only has one feature: a line string
                    if (!TextUtils.IsEmpty(type) && type.Equals("LineString",StringComparison.InvariantCultureIgnoreCase ))
                    {

                        // Get the Coordinates
                        JSONArray coords = geometry.GetJSONArray("coordinates");
                        for (int lc = 0; lc < coords.Length (); lc++)
                        {
                            JSONArray coord = coords.GetJSONArray(lc);
                            LatLng latLng = new LatLng(coord.GetDouble(1), coord.GetDouble(0));
                            points.Add(latLng);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("Exception Loading GeoJSON: " + exception.Message);
            }
            Java.Util.ArrayList al = new Java.Util.ArrayList(points); 
            return al;
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
            d.SetTitle ("Test");
            d.Show ();
          
            return true;
        }

    }
}


