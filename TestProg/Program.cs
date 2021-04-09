using System;
using Astronomy;

namespace TestProg
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = Calculator.GetTimes(DateTime.Now, 55.75700, 37.61500);

            Console.WriteLine("Sunrise time:  " + res.Sunrise.Hour.ToString() + ":" + res.Sunrise.Minute.ToString() + ":" + res.Sunrise.Second.ToString());

            Console.WriteLine(Planets.GetMeanAnomaly(PlanetName.Earth, DateTime.Now));
            Console.WriteLine(Planets.GetEquationOfCenter(PlanetName.Earth, DateTime.Now));
            Console.WriteLine(Planets.GetSiderealTime(PlanetName.Earth, DateTime.Now, -5));
            Console.WriteLine(Planets.GetEquatorialCoordinates(PlanetName.Earth, DateTime.Now));
        }
    }
}
