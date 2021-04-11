using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    public class MoonPosition
    {
        public double Azimuth { get; internal set; }

        public double Altitude { get; internal set; }

        public double Distance { get; internal set; }

        public double ParallacticAngle { get; internal set; }
    }
}
