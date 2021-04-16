using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    /// <summary>
    /// The class contains moonlight phases.
    /// </summary>
    public class MoonTimes
    {
        /// <summary>
        /// Moonrise time.
        /// </summary>
        public DateTime Rise { get; internal set; }

        /// <summary>
        /// Moonset time.
        /// </summary>
        public DateTime Set { get; internal set; }

        /// <summary>
        /// The period of time when moon is above the horizon during the day.
        /// </summary>
        public bool AlwaysUp { get; internal set; }

        /// <summary>
        /// The period of time when moon is below the horizon during the day.
        /// </summary>
        public bool AlwaysDown { get; internal set; }
    }
}
