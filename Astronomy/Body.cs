using System;

namespace Astronomy
{
    public class Body
    {
        public DateTime Sunrise { get; internal set; }

        public DateTime Sunset { get; internal set; }

        public DateTime SunriseEnd { get; internal set; }

        public DateTime SunsetStart { get; internal set; }

        public DateTime Dawn { get; internal set; }

        public DateTime Dusk { get; internal set; }

        public DateTime NauticalDawn { get; internal set; }

        public DateTime NauticalDusk { get; internal set; }

        public DateTime NightEnd { get; internal set; }

        public DateTime Night { get; internal set; }

        public DateTime GoldenHourEnd { get; internal set; }

        public DateTime GoldenHour { get; internal set; }

        public DateTime SolarNoon { get; internal set; }

        public DateTime Nadir { get; internal set; }
    }
}
