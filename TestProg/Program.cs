using System;
using Astronomy;

namespace TestProg
{
    class Program
    {
        static void Main(string[] args)
        {
            //Moscow
            var res = SunCalculator.GetTimes(DateTime.Now, 55.75700, 37.61500);

            Console.WriteLine("Sunrise time in Moscow:  " + res.Sunrise.Hour.ToString() + ":" + res.Sunrise.Minute.ToString() + ":" + res.Sunrise.Second.ToString());
            Console.WriteLine("Sunset time in Moscow:  " + res.Sunset.Hour.ToString() + ":" + res.Sunset.Minute.ToString() + ":" + res.Sunset.Second.ToString());

            res = SunCalculator.GetTimes(DateTime.Now, 55.80030, 49.10827);

            Console.WriteLine("Sunrise time in Kazan:  " + res.Sunrise.Hour.ToString() + ":" + res.Sunrise.Minute.ToString() + ":" + res.Sunrise.Second.ToString());
            Console.WriteLine("Sunset time in Kazan:  " + res.Sunset.Hour.ToString() + ":" + res.Sunset.Minute.ToString() + ":" + res.Sunset.Second.ToString());


            Console.WriteLine(Planets.GetMeanAnomaly(PlanetName.Earth, DateTime.Now));
            Console.WriteLine(Planets.GetEquationOfCenter(PlanetName.Earth, DateTime.Now));
            Console.WriteLine(Planets.GetSiderealTime(PlanetName.Earth, DateTime.Now, -5));
            Console.WriteLine(Planets.GetEquatorialCoordinates(PlanetName.Earth, DateTime.Now));

            Console.WriteLine(SunCalculator.GetSunPosition(DateTime.Now, 55.75700, 37.61500).Altitude);
        }
    }
}
