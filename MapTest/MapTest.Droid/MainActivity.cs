using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Mapsui;
using Mapsui.Geometries;
using Mapsui.UI.Android;

namespace MapTest.Droid
{
	[Activity (Label = "MapTest.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

	    protected override void OnCreate (Bundle bundle)
	    {
	        base.OnCreate (bundle);

	        // Set our view from the "main" layout resource
	        SetContentView (Resource.Layout.Main);

	        // Get our button from the layout resource,
	        // and attach an event to it
	        Button button = FindViewById<Button> (Resource.Id.myButton);

	        button.Click += delegate {
	            button.Text = string.Format ("{0} clicks!", count++);
	        };

            var mapControl = FindViewById<MapControl>(Resource.Id.mapcontrol);
            mapControl.Map = CommonMap.CreateMap();

            using (var stream = new FileInfo (@"mnt/shared/TMP/map_01.pbf").OpenRead ())
	        {
                MyClass.Do (stream);
	        }
            mapControl.Map.Viewport.Center = new Point(3, 3);
        }

	}
}


