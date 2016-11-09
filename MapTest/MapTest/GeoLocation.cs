namespace MapTest
{
    public class GeoLocation
    {

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public GeoLocation(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;

        }

    }
}