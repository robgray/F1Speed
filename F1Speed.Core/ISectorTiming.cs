using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Core
{
    public interface ISectorTiming
    {
        float Sector1Time { get; }
        float Sector2Time { get; }
        float Sector3Time { get; }
        int CurrentSector { get; }
        float LapTime { get; }
    }
}
