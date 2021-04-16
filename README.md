# Astronomy

Astronomy is a tiny .net standart 2.0 library for calculating sun position, sunlight phases (times for sunrise, sunset, dusk, etc.), moon position and lunar phase for the given location and time, created by Azat Salakhutdinov (@developerAzat), Aigul Sibgatullina (@Aigul9), Ilnar Aglyamzyanov (@ilnarag21).

Most calculations are based on the formulas given in the excellent Astronomy Answers articles about position of the sun and the planets.

## Usage example

```C#
//Moscow
var res = SunCalculator.GetTimes(DateTime.Now, 55.75700, 37.61500);

Console.WriteLine("Sunrise time in Moscow:  " + res.Sunrise.Hour.ToString() + ":" + res.Sunrise.Minute.ToString() + ":" + res.Sunrise.Second.ToString());
Console.WriteLine("Sunset time in Moscow:  " + res.Sunset.Hour.ToString() + ":" + res.Sunset.Minute.ToString() + ":" + res.Sunset.Second.ToString());

res = SunCalculator.GetTimes(DateTime.Now, 55.80030, 49.10827);

Console.WriteLine("Sunrise time in Kazan:  " + res.Sunrise.Hour.ToString() + ":" + res.Sunrise.Minute.ToString() + ":" + res.Sunrise.Second.ToString());
Console.WriteLine("Sunset time in Kazan:  " + res.Sunset.Hour.ToString() + ":" + res.Sunset.Minute.ToString() + ":" + res.Sunset.Second.ToString());
```
## Reference

### Sunlight times

```C#
SunCalculator.GetTimes(/*DateTime*/ date, /*double*/ latitude, /*double*/ longitude, /*double (default=0)*/ height)
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

### Sun position

```C#
SunPosition GetSunPosition(DateTime date, double lat, double lng)
```

Returns an object with the following properties:

 * `Altitude`: sun altitude above the horizon in radians,
 e.g. `0` at the horizon and `PI/2` at the zenith (straight over your head)
 * `Azimuth`: sun azimuth in radians (direction along the horizon, measured from south to west),
 e.g. `0` is south and `Math.PI * 3/4` is northwest


### Moon position

```C#
MoonPosition GetMoonPosition(DateTime date, double lat, double lng)
```

Returns an object with the following properties:

 * `Altitude`: moon altitude above the horizon in radians
 * `Azimuth`: moon azimuth in radians
 * `Distance`: distance to moon in kilometers
 * `ParallacticAngle`: parallactic angle of the moon in radians


### Moon illumination

```C#
Tuple<double, double, double> GetMoonIllumination(DateTime date)
```

Returns an object with the following properties:

 * `fraction`: illuminated fraction of the moon; varies from `0.0` (new moon) to `1.0` (full moon)
 * `phase`: moon phase; varies from `0.0` to `1.0`, described below
 * `angle`: midpoint angle in radians of the illuminated limb of the moon reckoned eastward from the north point of the disk;
 the moon is waxing if the angle is negative, and waning if positive

Moon phase value should be interpreted like this:

| Phase | Name            |
| -----:| --------------- |
| 0     | New Moon        |
|       | Waxing Crescent |
| 0.25  | First Quarter   |
|       | Waxing Gibbous  |
| 0.5   | Full Moon       |
|       | Waning Gibbous  |
| 0.75  | Last Quarter    |
|       | Waning Crescent |

By subtracting the `parallacticAngle` from the `angle` one can get the zenith angle of the moons bright limb (anticlockwise).
The zenith angle can be used do draw the moon shape from the observers perspective (e.g. moon lying on its back).

### Moon rise and set times

```C#
MoonTimes GetTimes(DateTime date, double lat, double lng)
```

Returns an object with the following properties:

 * `Rise`: moonrise time as `Date`
 * `Set`: moonset time as `Date`
 * `AlwaysUp`: `true` if the moon never rises/sets and is always _above_ the horizon during the day
 * `AlwaysDown`: `true` if the moon is always _below_ the horizon