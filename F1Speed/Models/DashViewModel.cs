using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Models
{
    public class DashViewModel
    {
        public string SpeedDelta { get; set; }
        public float TimeDelta { get; set; }
        public bool IsSpeedDeltaPositive { get; set; }
        public bool IsTimeDeltaPositive { get; set; }
        
    }
}
