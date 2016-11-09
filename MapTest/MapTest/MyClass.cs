using System;
using System.Collections.Generic;
using System.IO;
using Itinero;
using Itinero.IO.Osm;
using Itinero.LocalGeo;
using Itinero.Osm.Vehicles;

namespace MapTest
{
    public class MyClass
    {
        public static List<GeoLocation> Locations { get; set; }= new List<GeoLocation>();
        public static void Do (Stream stream)
        {
            var routerDb = new RouterDb();
            var router = new Router(routerDb);
            
            routerDb.LoadOsmData(stream, Vehicle.Pedestrian);

            // calculate a route.
            var route = router.Calculate(Vehicle.Pedestrian.Fastest(),
                51.72070116f, 8.74880791f, 51.71584899f, 8.75824928f);
            //var geoJson = route.ToGeoJson();

            foreach (Coordinate coord in route.Shape)
            {
                Locations.Add(new GeoLocation(coord.Latitude, coord.Longitude));
            }
        }
    }
}

