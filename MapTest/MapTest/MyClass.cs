using System;
using System.IO;
using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;

namespace MapTest
{
    public class MyClass
    {

        public static string Do (Stream stream)
        {
            var routerDb = new RouterDb();
            var router = new Router(routerDb);
            
            routerDb.LoadOsmData(stream, Vehicle.Car);

            // calculate a route.
            var route = router.Calculate(Vehicle.Car.Fastest(),
                51.72070116f, 8.74880791f, 51.71584899f, 8.75824928f);
            var geoJson = route.ToGeoJson();

            return geoJson;
        }
    }
}

