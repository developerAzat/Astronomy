using System;
using System.Collections.Generic;
using System.Text;

namespace Astronomy
{
    public class MoonTimes
    {
        public DateTime Rise { get; internal set; }

        public DateTime Set { get; internal set; }

        public bool AlwaysUp { get; internal set; }

        public bool AlwaysDown { get; internal set; }
    }
}
