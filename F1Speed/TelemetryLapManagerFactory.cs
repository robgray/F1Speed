using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core;

namespace F1Speed
{
    public class TelemetryLapManagerFactory
    {
        private static TelemetryLapManager manager;

        public TelemetryLapManager GetManager()
        {
            if (manager == null)
                manager = new TelemetryLapManager();

            return manager;
        }
    }
}
