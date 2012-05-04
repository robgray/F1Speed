using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Service.Model;
using FluentNHibernate.Mapping;

namespace F1Speed.Service.Repositories.Mappings
{
    public class LapSampleMap : ClassMap<LapSample>
    {
        public LapSampleMap()
        {
            Id(x => x.SampleId);
            Map(x => x.Time);
            Map(x => x.LapTime);
            Map(x => x.LapDistance);
            Map(x => x.X);
            Map(x => x.Y);
            Map(x => x.Z);
            Map(x => x.Speed);
            Map(x => x.WorldSpeedX);
            Map(x => x.WorldSpeedY);
            Map(x => x.WorldSpeedZ);
            Map(x => x.XR);
            Map(x => x.Roll);
            Map(x => x.ZR);
            Map(x => x.XD);
            Map(x => x.Pitch);
            Map(x => x.ZD);
            Map(x => x.SuspensionPositionRearRight);
            Map(x => x.SuspensionPositionRearLeft);
            Map(x => x.SuspensionPositionFrontRight);
            Map(x => x.SuspensionPositionFrontLeft);
            Map(x => x.SuspensionVelocityFrontLeft);
            Map(x => x.SuspensionVelocityRearLeft);
            Map(x => x.SuspensionVelocityRearRight);
            Map(x => x.SuspensionVelocityFrontRight);
            Map(x => x.WheelSpeedRearRight);
            Map(x => x.WheelSpeedRearLeft);
            Map(x => x.WheelSpeedFrontLeft);
            Map(x => x.WheelSpeedFrontRight);
            Map(x => x.Throttle);
            Map(x => x.Steer);
            Map(x => x.Brake);
            Map(x => x.Clutch);
            Map(x => x.Gear);
            Map(x => x.LateralAcceleration);
            Map(x => x.LongitudinalAcceleration);            
            Map(x => x.Lap);
            Map(x => x.EngineRevs);            
        }
    }
}
