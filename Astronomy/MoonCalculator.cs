using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    public class MoonCalculator
    {
        internal static readonly double rad = Math.PI / 180;

        #region general moon calculations

        internal static readonly Func<double, double> EclipticLongitude = (double d) => rad * (218.316 + 13.176396 * d);

        internal static readonly Func<double, double> MeanAnomaly = (double d) => rad * (134.963 + 13.064993 * d);

        internal static readonly Func<double, double> MeanDistance = (double d) => rad * (93.272 + 13.229350 * d);

        internal static readonly Func<double, double, double> Longitude = (double L, double M) => L + rad * 6.289 * Math.Sin(M);

        internal static readonly Func<double, double> Latitude = (double F) => rad * 5.128 * Math.Sin(F);

        internal static readonly Func<double, double> Distance = (double M) => 385001 - 20905 * Math.Cos(M);

        internal static readonly Func<double, double> Fraction = (double inc) => (1 + Math.Cos(inc)) / 2;

        internal static readonly Func<double, double, double> Phase = (double inc, double angle) => 0.5 + 0.5 * inc * (angle < 0 ? -1 : 1) / Math.PI;

        #endregion

        internal static readonly Func<DateTime, double, double> Hours = (DateTime date, double h) => new DateTimeOffset(date).ToUnixTimeMilliseconds() + h * General.dayMs / 24;

        static private (double dec, double asc, double dist) MoonCoords(double d)
        {
            double M = MeanAnomaly(d),
                L = EclipticLongitude(d),
                F = MeanDistance(d);

            double l = Longitude(L, M),
                b = Latitude(F),
                dt = Distance(M);

            return (dec: General.Declination(L, 0), asc: General.RightAscension(L, 0), dist: dt);
        }

        static public MoonPosition GetMoonPosition(DateTime date, double lat, double lng)
        {
            double lw = rad * -lng,
                   phi = rad * lat,
                   d = General.ToDays(date);

            var c = MoonCoords(d);

            double H = General.SiderealTime(d, lw) - c.asc,
                h = General.Altitude(H, phi, c.dec),
                pa = Math.Atan2(Math.Sin(H), Math.Tan(phi) * Math.Cos(c.dec) - Math.Sin(c.dec) * Math.Cos(H));

            h = General.AstroRefraction(h);

            return new MoonPosition()
            {
                Altitude = h,
                Azimuth = General.Azimuth(H, phi, c.dec),
                Distance = c.dist,
                ParallacticAngle = pa
            };
        }

        static public Tuple<double, double, double> GetMoonIllumination(DateTime date)
        {
            double d = General.ToDays(date);
            var s = SunCalculator.SunCoords(d);
            var m = MoonCoords(d);

            int sdist = 149598000;

            double phi = Math.Acos(Math.Sin(s.dec) * Math.Sin(m.dec) + Math.Cos(s.dec) * Math.Cos(m.dec) * Math.Cos(s.asc - m.asc)),
                inc = Math.Atan2(sdist * Math.Sin(phi), m.dist - sdist * Math.Cos(phi)),
                angle = Math.Atan2(Math.Cos(s.dec) * Math.Sin(s.asc - m.asc), Math.Sin(s.dec) * Math.Cos(m.dec)
                - Math.Cos(s.dec) * Math.Sin(m.dec) * Math.Cos(s.asc - m.asc));

            return new Tuple<double, double, double>(Fraction(inc), Phase(inc, angle), angle);
        }
    }
}
