using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core;
using F1Speed.Models;
using Nancy;
using F1Speed.Core;

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
                    var speedDelta = _telemetryLapManager.GetSpeedDelta();
                    var timeDelta = _telemetryLapManager.GetTimeDelta();

                    if (speedDelta == "--")                    
                        speedDelta = "+0";
                    
                    var speedPositive = speedDelta.Substring(0, 1) == "+";
                    var timePositive = timeDelta > 0;
                    
                    var speedAbs = speedDelta.Substring(1);

                    var model = new DashViewModel
                        {
                            CircuitName = _telemetryLapManager.Circuit.Name, 
                            LapType = _telemetryLapManager.LapType ?? "",
                            SpeedDelta = float.Parse(speedAbs),
                            TimeDelta = timeDelta,
                            IsSpeedDeltaPositive = speedPositive,
                            IsTimeDeltaPositive = timePositive,
                            WheelspinRearLeft = _telemetryLapManager.CurrentWheelSpin(WheelspinWheel.RearLeft),
                            WheelspinRearRight = _telemetryLapManager.CurrentWheelSpin(WheelspinWheel.RearRight),
                            WheelspinFrontLeft = _telemetryLapManager.CurrentWheelSpin(WheelspinWheel.FrontLeft),
                            WheelspinFrontRight = _telemetryLapManager.CurrentWheelSpin(WheelspinWheel.FrontRight),
                            Throttle = _telemetryLapManager.CurrentThrottle,
                            Brake = _telemetryLapManager.CurrentBrake,                            
                            CurrentLap = _telemetryLapManager.CurrentLapTime,
                            LastLap = _telemetryLapManager.LastLapTime,
                            Sector1 = new SectorTimeViewModel(_telemetryLapManager.Sector1),
                            Sector2 = new SectorTimeViewModel(_telemetryLapManager.Sector2),
                            Sector3 = new SectorTimeViewModel(_telemetryLapManager.Sector3)                                
                        };

                    if (_telemetryLapManager.FastestLap == null)
                    {
                        model.FastestLap = 0f.AsTimeString();
                    }
                    else
                    {
                        model.FastestLap = _telemetryLapManager.FastestLap.LapTime.AsTimeString();
                    }
                    
                    return Response.AsJson(model);
                };
        }
    }
}
