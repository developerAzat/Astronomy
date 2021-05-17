using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{

    public class SunCalculator : BaseCalculator
    {
        #region calculations for sun times

        internal static readonly double J0 = 0.0009;

        internal static readonly Func<double, double, double> JulianCycle = (double d, double lw) => Math.Round(d - J0 - lw / (2 * Math.PI));

        internal static readonly Func<double, double, double, double> ApproxTransit = (double ht, double lw, double n) => J0 + (ht + lw) / (2 * Math.PI) + n;

        internal static readonly Func<double, double, double, double> SolarTransitJ = (double ds, double M, double L) => J2000 + ds + 0.0053 * Math.Sin(M) - 0.0069 * Math.Sin(2 * L);

        internal static readonly Func<double, double, double, double> HourAngle = (double h, double phi, double d) => Math.Acos((Math.Sin(h) - Math.Sin(phi) * Math.Sin(d)) / (Math.Cos(phi) * Math.Cos(d)));

        internal static readonly Func<double, double> ObserverAngle = (double height) => -2.076 * Math.Sqrt(height) / 60;

        #endregion

        #region general sun calculations

        internal static readonly Func<double, double> SolarMeanAnomaly = (double d) => rad * (357.5291 + 0.98560028 * d);

        internal static readonly Func<double, double> EclipticLongitude = (double M) =>
        {
            double C = rad * (1.9148 * Math.Sin(M) + 0.02 * Math.Sin(2 * M) + 0.0003 * Math.Sin(3 * M)), // equation of center
                P = rad * 102.9372; // perihelion of the Earth

            return M + C + P + Math.PI;
        };

        #endregion

        // returns set time for the given sun altitude
        internal static double GetSetJ(double h, double lw, double phi, double dec, double n, double M, double L)
        {

            double w = HourAngle(h, phi, dec),
                a = ApproxTransit(w, lw, n);
            return SolarTransitJ(a, M, L);
        }

        internal static Tuple<DateTime, DateTime> GetTimesCalculate(double constant, double lw, double phi, double dec, double n, double M, double L, double dh, double Jnoon)
        {
            double h0 = (constant + dh) * rad;

            double Jset = GetSetJ(h0, lw, phi, dec, n, M, L);
            double Jrise = Jnoon - (Jset - Jnoon);
            
            if (Jset is Double.NaN || Jrise is Double.NaN)
            {
                return null;
            }

            return new Tuple<DateTime, DateTime>(FromJulian(Jrise), FromJulian(Jset));
        }

        ///<summary>
        ///Get Sun Times
        ///</summary>
        ///<param name="date">Date and time for calculation</param>
        ///<param name="lat">Latitude</param>
        ///<param name="lng">Longtitude</param>
        ///<param name="height">Optional height</param>
        ///<returns>A Body class with sun times</returns>
        static public SunTimes GetTimes(DateTime date, double lat, double lng, double height = 0)
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

            SunTimes result = new SunTimes()
            {
                SolarNoon = FromJulian(Jnoon),
                Nadir = FromJulian(Jnoon - 0.5),
            };

            Tuple<DateTime, DateTime> temp = GetTimesCalculate(-0.833, lw, phi, dec, n, M, L, dh, Jnoon);

            if (temp != null)
            {
                result.Sunrise = temp.Item1;
                result.Sunset = temp.Item2;
            }

            temp = GetTimesCalculate(-0.3, lw, phi, dec, n, M, L, dh, Jnoon);

            if (temp != null)
            {
                result.SunriseEnd = temp.Item1;
                result.SunsetStart = temp.Item2;
            }

            temp = GetTimesCalculate(-6, lw, phi, dec, n, M, L, dh, Jnoon);

            if (temp != null)
            {
                result.Dawn = temp.Item1;
                result.Dusk = temp.Item2;
            }

            temp = GetTimesCalculate(-12, lw, phi, dec, n, M, L, dh, Jnoon);

            if (temp != null)
            {
                result.NauticalDawn = temp.Item1;
                result.NauticalDusk = temp.Item2;
            }

            temp = GetTimesCalculate(-18, lw, phi, dec, n, M, L, dh, Jnoon);

            if (temp != null)
            {
                result.NightEnd = temp.Item1;
                result.Night = temp.Item2;
            }

            temp = GetTimesCalculate(6, lw, phi, dec, n, M, L, dh, Jnoon);

            if (temp != null)
            {
                result.GoldenHourEnd = temp.Item1;
                result.GoldenHour = temp.Item2;
            }

            return result;
        }

        static public (double dec, double asc) SunCoords(double d)
        {
            double M = SolarMeanAnomaly(d),
                   L = EclipticLongitude(M);

            return (dec: Declination(L, 0), asc: RightAscension(L, 0));
        }

        static public SunPosition GetSunPosition(DateTime date, double lat, double lng)
        {
            double lw = rad * -lng,
                   phi = rad * lat,
                   
                   d = ToDays(date);

            var c = SunCoords(d);

            double H = SiderealTime(d, lw) - c.asc;

            return new SunPosition()
            {
                Altitude = Altitude(H, phi, c.dec),
                Azimuth = Azimuth(H, phi, c.dec)
            };
        }
    }
}
