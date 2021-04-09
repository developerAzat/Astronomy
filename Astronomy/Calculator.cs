using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    public class Calculator
    {
        public static readonly double rad = Math.PI / 180;

        static readonly double dayMs = 1000 * 60 * 60 * 24,
               J1970 = 2440588,
               J2000 = 2451545;

        #region general calculations for position

        // general calculations for position

        static readonly double e = rad * 23.4397; // obliquity of the Earth

        static readonly Func<double, double, double> RightAscension = (double l, double b) => Math.Atan2(Math.Asin(l) * Math.Cos(e) - Math.Tan(b) * Math.Asin(e), Math.Cos(l));

        static readonly Func<double, double, double> Declination = (double l, double b) => Math.Asin(Math.Sin(b) * Math.Cos(e) + Math.Cos(b) * Math.Sin(e) * Math.Sin(l));

        static readonly Func<double, double, double, double> Azimuth = (double H, double phi, double dec) => Math.Atan2(Math.Sin(H), Math.Cos(H) * Math.Sin(phi) - Math.Tan(dec) * Math.Cos(phi));

        static readonly Func<double, double, double, double> Altitude = (double H, double phi, double dec) => Math.Asin(Math.Sin(phi) * Math.Sin(dec) + Math.Cos(phi) * Math.Cos(dec) * Math.Cos(H));

        static readonly Func<double, double, double> SiderealTime = (double d, double lw) => rad * (280.16 + 360.9856235 * d) - lw;

        static readonly Func<double, double> AstroRefraction = (double h) =>
        {
            if (h < 0) // the following formula works for positive altitudes only.
                h = 0; // if h = -0.08901179 a div/0 would occur.

            // formula 16.4 of "Astronomical Algorithms" 2nd edition by Jean Meeus (Willmann-Bell, Richmond) 1998.
            // 1.02 / tan(h + 10.26 / (h + 5.10)) h in degrees, result in arc minutes -> converted to rad:
            return 0.0002967 / Math.Tan(h + 0.00312536 / (h + 0.08901179));
        };

        #endregion

        static readonly Func<DateTime, double> ToJulian = (DateTime date) => new DateTimeOffset(date).ToUnixTimeMilliseconds() / dayMs - 0.5 + J1970;

        //static readonly Func<double, DateTime> FromJulian = (double j) => new DateTime((long)(((j + 0.5 - J1970)* 10000) * dayMs),DateTimeKind.Utc);

        static readonly Func<double, DateTime> FromJulian = (double j) => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((j + 0.5 - J1970) * dayMs).ToLocalTime();

        public static readonly Func<DateTime, double> ToDays = (DateTime date) => ToJulian(date) - J2000;

        #region calculations for sun times

        static readonly double J0 = 0.0009;

        static readonly Func<double, double, double> JulianCycle = (double d, double lw) => Math.Round(d - J0 - lw / (2 * Math.PI));

        static readonly Func<double, double, double, double> ApproxTransit = (double ht, double lw, double n) => J0 + (ht + lw) / (2 * Math.PI) + n;

        static readonly Func<double, double, double, double> SolarTransitJ = (double ds, double M, double L) => J2000 + ds + 0.0053 * Math.Sin(M) - 0.0069 * Math.Sin(2 * L);

        static readonly Func<double, double, double, double> HourAngle = (double h, double phi, double d) => Math.Acos((Math.Sin(h) - Math.Sin(phi) * Math.Sin(d)) / (Math.Cos(phi) * Math.Cos(d)));

        static readonly Func<double, double> ObserverAngle = (double height) => -2.076 * Math.Sqrt(height) / 60;

        #endregion

        #region general sun calculations

        static readonly Func<double, double> SolarMeanAnomaly = (double d) => rad * (357.5291 + 0.98560028 * d);

        static readonly Func<double, double> EclipticLongitude = (double M) =>
        {
            double C = rad * (1.9148 * Math.Sin(M) + 0.02 * Math.Sin(2 * M) + 0.0003 * Math.Sin(3 * M)), // equation of center
                P = rad * 102.9372; // perihelion of the Earth

            return M + C + P + Math.PI;
        };

        #endregion

        // returns set time for the given sun altitude
        static double GetSetJ(double h, double lw, double phi, double dec, double n, double M, double L)
        {

            double w = HourAngle(h, phi, dec),
                a = ApproxTransit(w, lw, n);
            return SolarTransitJ(a, M, L);
        }

        static Tuple<DateTime, DateTime> GetTimesCalculate(double constant, double lw, double phi, double dec, double n, double M, double L, double dh, double Jnoon)
        {
            double h0 = (constant + dh) * rad;

            double Jset = GetSetJ(h0, lw, phi, dec, n, M, L);
            double Jrise = Jnoon - (Jset - Jnoon);

            return new Tuple<DateTime, DateTime>(FromJulian(Jrise), FromJulian(Jset));
        }

        static public Body GetTimes(DateTime date, double lat, double lng, double height = 0)
        {
            double lw = rad * -lng,
                   phi = rad * lat,
                   dh = ObserverAngle(height),

                   d = ToDays(date),
                   n = JulianCycle(d, lw),
                   ds = ApproxTransit(0, lw, n),

                   M = SolarMeanAnomaly(ds),
                   L = EclipticLongitude(M),
                   dec = Declination(L, 0),

                   Jnoon = SolarTransitJ(ds, M, L);

            Body result = new Body()
            {
                SolarNoon = FromJulian(Jnoon),
                Nadir = FromJulian(Jnoon - 0.5),
            };

            Tuple<DateTime, DateTime> temp = GetTimesCalculate(-0.833, lw, phi, dec, n, M, L, dh, Jnoon);

            result.Sunrise = temp.Item1;
            result.Sunset = temp.Item2;

            temp = GetTimesCalculate(-0.3, lw, phi, dec, n, M, L, dh, Jnoon);

            result.SunriseEnd = temp.Item1;
            result.SunsetStart = temp.Item2;

            temp = GetTimesCalculate(-6, lw, phi, dec, n, M, L, dh, Jnoon);

            result.Dawn = temp.Item1;
            result.Dusk = temp.Item2;

            temp = GetTimesCalculate(-12, lw, phi, dec, n, M, L, dh, Jnoon);

            result.NauticalDawn = temp.Item1;
            result.NauticalDusk = temp.Item2;

            temp = GetTimesCalculate(-18, lw, phi, dec, n, M, L, dh, Jnoon);

            result.NightEnd = temp.Item1;
            result.Night = temp.Item2;

            temp = GetTimesCalculate(6, lw, phi, dec, n, M, L, dh, Jnoon);

            result.GoldenHourEnd = temp.Item1;
            result.GoldenHour = temp.Item2;

            return result;
        }

        static private Tuple<double, double> SunCoords(double d)
        {
            double M = SolarMeanAnomaly(d),
                   L = EclipticLongitude(M);

            return new Tuple<double, double>(Declination(L, 0), RightAscension(L, 0));
        }

        static public SunPosition GetSunPosition(DateTime date, double lat, double lng)
        {
            double lw = rad * -lng,
                   phi = rad * lat,
                   d = ToDays(date);

            var c = SunCoords(d);

            double H = SiderealTime(d, lw) - c.Item2;

            return new SunPosition()
            {
                Altitude = Altitude(H, phi, c.Item1),
                Azimuth = Azimuth(H, phi, c.Item2)
            };
        }
    }
}
