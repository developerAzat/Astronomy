using System;

namespace Astronomy
{
    /// <summary>
    /// The class contains sunlight phases.
    /// </summary>
    public class SunTimes
    {
        /// <summary>
        /// Sunrise time.
        /// </summary>
        public DateTime Sunrise { get; internal set; }

        /// <summary>
        /// Sunset time.
        /// </summary>
        public DateTime Sunset { get; internal set; }

        /// <summary>
        /// Sunrise end time.
        /// </summary>
        public DateTime SunriseEnd { get; internal set; }

        /// <summary>
        /// Sunset start time.
        /// </summary>
        public DateTime SunsetStart { get; internal set; }

        /// <summary>
        /// Dawn time.
        /// </summary>
        public DateTime Dawn { get; internal set; }

        /// <summary>
        /// Dusk time.
        /// </summary>
        public DateTime Dusk { get; internal set; }

        /// <summary>
        /// The beginning of the evening astronomical twilight.
        /// </summary>
        public DateTime NauticalDawn { get; internal set; }

        /// <summary>
        /// Early morning nautical twilight.
        /// </summary>
        public DateTime NauticalDusk { get; internal set; }

        /// <summary>
        /// End time of the night.
        /// </summary>
        public DateTime NightEnd { get; internal set; }

        /// <summary>
        /// Start time of the night.
        /// </summary>
        public DateTime Night { get; internal set; }

        /// <summary>
        /// End of soft light, the most suitable time for photography.
        /// </summary>
        public DateTime GoldenHourEnd { get; internal set; }

        /// <summary>
        /// Start of soft light, the most suitable time for photography.
        /// </summary>
        public DateTime GoldenHour { get; internal set; }

        /// <summary>
        /// The sun is at its highest point.
        /// </summary>
        public DateTime SolarNoon { get; internal set; }

        /// <summary>
        /// The sun is in the lowest point.
        /// </summary>
        public DateTime Nadir { get; internal set; }
    }
}
