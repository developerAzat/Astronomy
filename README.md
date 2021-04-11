# Astronomy

Astronomy is a tiny .net standart 2.0 library for calculating sun position, sunlight phases (times for sunrise, sunset, dusk, etc.), moon position and lunar phase for the given location and time, created by Azat Salakhutdinov (@developerAzat).

Most calculations are based on the formulas given in the excellent Astronomy Answers articles about position of the sun and the planets. You can read about different twilight phases calculated by SunCalc in the Twilight article on Wikipedia.

## Usage example

```c#
//Moscow
var res = Calculator.GetTimes(DateTime.Now, 55.75700, 37.61500);

Console.WriteLine("Sunrise time in Moscow:  " + res.Sunrise.Hour.ToString() + ":" + res.Sunrise.Minute.ToString() + ":" + res.Sunrise.Second.ToString());
Console.WriteLine("Sunset time in Moscow:  " + res.Sunset.Hour.ToString() + ":" + res.Sunset.Minute.ToString() + ":" + res.Sunset.Second.ToString());

res = Calculator.GetTimes(DateTime.Now, 55.80030, 49.10827);

Console.WriteLine("Sunrise time in Kazan:  " + res.Sunrise.Hour.ToString() + ":" + res.Sunrise.Minute.ToString() + ":" + res.Sunrise.Second.ToString());
Console.WriteLine("Sunset time in Kazan:  " + res.Sunset.Hour.ToString() + ":" + res.Sunset.Minute.ToString() + ":" + res.Sunset.Second.ToString());
```
