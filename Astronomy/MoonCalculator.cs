using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    public class MoonCalculator : BaseCalculator
    {

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

        internal static readonly Func<DateTime, double, DateTime> HoursLater = (DateTime date, double h) => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(new DateTimeOffset(date).ToUnixTimeMilliseconds() + h* dayMs / 24).ToLocalTime();

        static private (double dec, double asc, double dist) MoonCoords(double d)
        {
            double M = MeanAnomaly(d),
                L = EclipticLongitude(d),
                F = MeanDistance(d);

            double l = Longitude(L, M),
                b = Latitude(F),
                dt = Distance(M);

            return (dec: Declination(L, 0), asc: RightAscension(L, 0), dist: dt);
        }

        static public MoonPosition GetMoonPosition(DateTime date, double lat, double lng)
        {
            double lw = rad * -lng,
                   phi = rad * lat,
                   d = ToDays(date);

            var c = MoonCoords(d);

            double H = SiderealTime(d, lw) - c.asc,
                h = Altitude(H, phi, c.dec),
                pa = Math.Atan2(Math.Sin(H), Math.Tan(phi) * Math.Cos(c.dec) - Math.Sin(c.dec) * Math.Cos(H));

            h = AstroRefraction(h);

            return new MoonPosition()
            {
                Altitude = h,
                Azimuth = Azimuth(H, phi, c.dec),
                Distance = c.dist,
                ParallacticAngle = pa
            };
        }

        static public Tuple<double, double, double> GetMoonIllumination(DateTime date)
        {
            double d = ToDays(date);
            var s = SunCalculator.SunCoords(d);
            var m = MoonCoords(d);

            int sdist = 149598000;

            double phi = Math.Acos(Math.Sin(s.dec) * Math.Sin(m.dec) + Math.Cos(s.dec) * Math.Cos(m.dec) * Math.Cos(s.asc - m.asc)),
                inc = Math.Atan2(sdist * Math.Sin(phi), m.dist - sdist * Math.Cos(phi)),
                angle = Math.Atan2(Math.Cos(s.dec) * Math.Sin(s.asc - m.asc), Math.Sin(s.dec) * Math.Cos(m.dec)
                - Math.Cos(s.dec) * Math.Sin(m.dec) * Math.Cos(s.asc - m.asc));

            return new Tuple<double, double, double>(Fraction(inc), Phase(inc, angle), angle);
        }

        // calculations for moon rise/set times are based on http://www.stargazing.net/kepler/moonrise.html article

        static public MoonTimes GetTimes(DateTime date, double lat, double lng)
        {
            var t = date.Date;

            double hc = 0.133 * rad,
                h0 = GetMoonPosition(t, lat, lng).Altitude - hc,
                h1, h2, rise = 0, set = 0, a, b, xe, ye = 0, d, roots, x1 = 0, x2 = 0, dx;

            for(int i = 1; i <= 24; i+=2)
            {
                h1 = GetMoonPosition(HoursLater(t, i), lat, lng).Altitude - hc;
                h2 = GetMoonPosition(HoursLater(t, i + 1), lat, lng).Altitude - hc;

                a = (h0 + h2) / 2 - h1;
                b = (h2 - h0) / 2;
                xe = -b / (2 * a);
                ye = (a * xe + b) * xe + h1;
                d = b * b - 4 * a * h1;
                roots = 0;

                if (d >= 0)
                {
                    dx = Math.Sqrt(d) / (Math.Abs(a) * 2);
                    x1 = xe - dx;
                    x2 = xe + dx;
                    if (Math.Abs(x1) <= 1) roots++;
                    if (Math.Abs(x2) <= 1) roots++;
                    if (x1 < -1) x1 = x2;
                }

                if (roots == 1)
                {
                    if (h0 < 0)
                    {
                        rise = (double)i + x1;
                    }
                    else set = i + x1;

                }
                else if (roots == 2)
                {
                    rise = i + (ye < 0 ? x2 : x1);
                    set = i + (ye < 0 ? x1 : x2);
                }

                if (rise != 0 && set != 0) break;

                h0 = h2;
            }

            return new MoonTimes()
            {
                Rise = rise != 0 ? HoursLater(t, rise) : new DateTime(),
                Set = set !=0 ? HoursLater(t,set) : new DateTime(),
                AlwaysUp = ye > 0,
                AlwaysDown = ye <= 0
            };
        }
    }
}
