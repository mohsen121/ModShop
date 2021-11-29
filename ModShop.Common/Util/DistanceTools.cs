using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Common.Util
{
    public static class DistanceTools
    {
        public static double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
        {
            double R = 6371; // km

            double sLat1 = Math.Sin(lat1.ToRadian());
            double sLat2 = Math.Sin(lat2.ToRadian());
            double cLat1 = Math.Cos(lat1.ToRadian());
            double cLat2 = Math.Cos(lat2.ToRadian());
            double cLon = Math.Cos(lon1.ToRadian() - lon2.ToRadian());

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

            double d = Math.Acos(cosD);

            double dist = R * d;

            return Math.Round(dist, 2);
        }

        public static double ToRadian(this double x)
        {
            return x * Math.PI / 180;
        }
    }
}
