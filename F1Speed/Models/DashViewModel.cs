using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Models
{
    public class DashViewModel
    {
        public string CircuitName { get; set; }
        public string LapType { get; set; }

        public float SpeedDelta { get; set; }
        public float TimeDelta { get; set; }
        public bool IsSpeedDeltaPositive { get; set; }
        public bool IsTimeDeltaPositive { get; set; }

        public float WheelspinRearLeft { get; set; }
        public float WheelspinRearRight { get; set; }
        public float WheelspinFrontLeft { get; set; }
        public float WheelspinFrontRight { get; set; }

        public float Throttle { get; set; }
        public float Brake { get; set; }
    }
}
