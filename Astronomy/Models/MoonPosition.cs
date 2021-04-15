using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    /// <summary>
    /// The class contains main astronomical properties of the Moon.
    /// </summary>
    public class MoonPosition
    {
        /// <summary>
        /// The angular distance of an object from the local North.
        /// </summary>
        public double Azimuth { get; internal set; }

        /// <summary>
        /// The angular distance of an object above the local horizon.
        /// </summary>
        public double Altitude { get; internal set; }

        /// <summary>
        /// The distance to the Moon in km.
        /// </summary>
        public double Distance { get; internal set; }

        /// <summary>
        /// The angle between the great circle through a Moon and the zenith.
        /// </summary>
        public double ParallacticAngle { get; internal set; }
    }
}
