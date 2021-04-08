using System;
using Astronomy;

namespace TestProg
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = Calculator.GetTimes(DateTime.Now, 55.75700, 37.61500);

            Console.WriteLine(res.Sunrise.Hour.ToString() + ":" + res.Sunrise.Minute.ToString());
        }
    }
}
