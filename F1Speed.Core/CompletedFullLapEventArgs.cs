using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Core
{
    public class CompletedFullLapEventArgs
    {
        public TelemetryLap CompletedLap { get; set; }
        public int PreviousLapNumber { get; set; }
        public int CurrentLapNumber { get; set; }
    }
}
