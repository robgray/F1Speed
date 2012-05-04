using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace F1Speed.Service.Model
{
    [DataContract]
    public class LapSample
    {
        [DataMember]
        public virtual int SampleId { get; set; }
        [DataMember]
        public virtual float Time { get; set; }
        [DataMember]
        public virtual float LapTime { get; set; }
        [DataMember]
        public virtual float LapDistance { get; set; }
        [DataMember]
        public virtual float Distance { get; set; }
        [DataMember]
        public virtual float X { get; set; }
        [DataMember]
        public virtual float Y { get; set; }
        [DataMember]
        public virtual float Z { get; set; }
        [DataMember]
        public virtual float Speed { get; set; }
        [DataMember]
        public virtual float WorldSpeedX { get; set; }
        [DataMember]
        public virtual float WorldSpeedY { get; set; }
        [DataMember]
        public virtual float WorldSpeedZ { get; set; }
        [DataMember]
        public virtual float XR { get; set; }
        [DataMember]
        public virtual float Roll { get; set; }
        [DataMember]
        public virtual float ZR { get; set; }
        [DataMember]
        public virtual float XD { get; set; }
        [DataMember]
        public virtual float Pitch { get; set; }
        [DataMember]
        public virtual float ZD { get; set; }
        [DataMember]
        public virtual float SuspensionPositionRearLeft { get; set; }
        [DataMember]
        public virtual float SuspensionPositionRearRight { get; set; }
        [DataMember]
        public virtual float SuspensionPositionFrontLeft { get; set; }
        [DataMember]
        public virtual float SuspensionPositionFrontRight { get; set; }
        [DataMember]
        public virtual float SuspensionVelocityRearLeft { get; set; }
        [DataMember]
        public virtual float SuspensionVelocityRearRight { get; set; }
        [DataMember]
        public virtual float SuspensionVelocityFrontLeft { get; set; }
        [DataMember]
        public virtual float SuspensionVelocityFrontRight { get; set; }
        [DataMember]
        public virtual float WheelSpeedRearLeft { get; set; }
        [DataMember]
        public virtual float WheelSpeedRearRight { get; set; }
        [DataMember]
        public virtual float WheelSpeedFrontLeft { get; set; }
        [DataMember]
        public virtual float WheelSpeedFrontRight { get; set; }
        [DataMember]
        public virtual float Throttle { get; set; }
        [DataMember]
        public virtual float Steer { get; set; }
        [DataMember]
        public virtual float Brake { get; set; }
        [DataMember]
        public virtual float Clutch { get; set; }
        [DataMember]
        public virtual float Gear { get; set; }
        [DataMember]
        public virtual float LateralAcceleration { get; set; }
        [DataMember]
        public virtual float LongitudinalAcceleration { get; set; }
        [DataMember]
        public virtual float Lap { get; set; }
        [DataMember]
        public virtual float EngineRevs { get; set; }
    }
}
