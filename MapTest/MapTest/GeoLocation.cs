using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapTest
{
    public class GeoLocation {

        public double longitude;
        public double latitude;

        public GeoLocation (double lat, double lon)
        {
            this.latitude = lat;
            this.longitude = lon;
           
        }

    }
}
