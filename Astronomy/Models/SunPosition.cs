using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    /// <summary>
    /// The class contains main astronomical properties of the Sun.
    /// </summary>
    public class SunPosition
    {
        /// <summary>
        /// The angular distance of an object from the local North.
        /// </summary>
        public double Azimuth { get; internal set; }

        /// <summary>
        /// The angular distance of an object above the local horizon.
        /// </summary>
        public double Altitude { get; internal set; }
    }
}
