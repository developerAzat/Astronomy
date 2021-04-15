using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    public class BaseCalculator
    {
        internal static readonly double rad = Math.PI / 180;

        internal static readonly double dayMs = 1000 * 60 * 60 * 24,
               J1970 = 2440588,
               J2000 = 2451545;

        #region general calculations for position

        // general calculations for position

        internal static readonly double e = rad * 23.4397; // obliquity of the Earth

        internal static readonly Func<double, double, double> RightAscension = (double l, double b) => Math.Atan2(Math.Sin(l) * Math.Cos(e) - Math.Tan(b) * Math.Sin(e), Math.Cos(l));

        internal static readonly Func<double, double, double> Declination = (double l, double b) => Math.Asin(Math.Sin(b) * Math.Cos(e) + Math.Cos(b) * Math.Sin(e) * Math.Sin(l));

        internal static readonly Func<double, double, double, double> Azimuth = (double H, double phi, double dec) => Math.Atan2(Math.Sin(H), Math.Cos(H) * Math.Sin(phi) - Math.Tan(dec) * Math.Cos(phi));

        internal static readonly Func<double, double, double, double> Altitude = (double H, double phi, double dec) => Math.Asin(Math.Sin(phi) * Math.Sin(dec) + Math.Cos(phi) * Math.Cos(dec) * Math.Cos(H));

        internal static readonly Func<double, double, double> SiderealTime = (double d, double lw) => rad * (280.16 + 360.9856235 * d) - lw;

        internal static readonly Func<double, double> AstroRefraction = (double h) =>
        {
            if (h < 0) // the following formula works for positive altitudes only.
                h = 0; // if h = -0.08901179 a div/0 would occur.

            return 0.0002967 / Math.Tan(h + 0.00312536 / (h + 0.08901179));
        };

        #endregion

        internal static readonly Func<DateTime, double> ToJulian = (DateTime date) => new DateTimeOffset(date).ToUnixTimeMilliseconds() / dayMs - 0.5 + J1970;

        internal static readonly Func<double, DateTime> FromJulian = (double j) => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((j + 0.5 - J1970) * dayMs).ToLocalTime();

        internal static readonly Func<DateTime, double> ToDays = (DateTime date) => ToJulian(date) - J2000;
    }
}
