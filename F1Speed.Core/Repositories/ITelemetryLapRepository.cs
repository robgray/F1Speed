using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Core.Repositories
{
    public interface ITelemetryLapRepository
    {
        TelemetryLap Get(Circuit circuit, string lapType);
        void Save(TelemetryLap lap);        
        void Delete(TelemetryLap lap);
    }
}
