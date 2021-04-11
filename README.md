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
## Reference

### Sunlight times

```c#
Calculator.GetTimes(/*Date*/ date, /*double*/ latitude, /*double*/ longitude, /*double (default=0)*/ height)
```

Returns an object (SunTimes) with the following properties (each is a `DateTime` object):

| Property        | Description                                                              |
| --------------- | ------------------------------------------------------------------------ |
| `Sunrise`       | sunrise (top edge of the sun appears on the horizon)                     |
| `SunriseEnd`    | sunrise ends (bottom edge of the sun touches the horizon)                |
| `GoldenHourEnd` | morning golden hour (soft light, best time for photography) ends         |
| `SolarNoon`     | solar noon (sun is in the highest position)                              |
| `GoldenHour`    | evening golden hour starts                                               |
| `SunsetStart`   | sunset starts (bottom edge of the sun touches the horizon)               |
| `Sunset`        | sunset (sun disappears below the horizon, evening civil twilight starts) |
| `Dusk`          | dusk (evening nautical twilight starts)                                  |
| `NauticalDusk`  | nautical dusk (evening astronomical twilight starts)                     |
| `Night`         | night starts (dark enough for astronomical observations)                 |
| `Nadir`         | nadir (darkest moment of the night, sun is in the lowest position)       |
| `NightEnd`      | night ends (morning astronomical twilight starts)                        |
| `NauticalDawn`  | nautical dawn (morning nautical twilight starts)                         |
| `Dawn`          | dawn (morning nautical twilight ends, morning civil twilight starts)     |
