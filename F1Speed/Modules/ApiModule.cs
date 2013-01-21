using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core;
using F1Speed.Models;
using Nancy;

namespace F1Speed.Modules
{
    public class ApiModule : NancyModule
    {
        private readonly TelemetryLapManager _telemetryLapManager;
        
        public ApiModule(TelemetryLapManagerFactory managerFactory) : base("/api")
        {
            _telemetryLapManager = managerFactory.GetManager();

            Get["/packet"] = parameters =>
                {
                    var model = new DashViewModel
                        {
                            SpeedDelta = _telemetryLapManager.GetSpeedDelta(),
                            TimeDelta = _telemetryLapManager.GetTimeDelta(),
                            IsSpeedDeltaPositive = _telemetryLapManager.GetSpeedDelta().Substring(0, 1) == "+",
                            IsTimeDeltaPositive = _telemetryLapManager.GetTimeDelta() > 0                            
                        };
                    
                    return Response.AsJson(model);
                };
        }
    }
}
