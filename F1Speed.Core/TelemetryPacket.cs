using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace F1Speed.Core
{
    [Serializable]
    public struct TelemetryPacket : ISerializable
    {
        public TelemetryPacket(SerializationInfo info, StreamingContext context)
        {
            Time = info.GetValue<float>("Time");
            LapTime = info.GetValue<float>("LapTime");
            LapDistance = info.GetValue<float>("LapDistance");
            Distance = info.GetValue<float>("Distance");
            Speed = info.GetValue<float>("Speed");
            Lap = info.GetValue<float>("Lap");
            X = info.GetValue<float>("X");
            Y = info.GetValue<float>("Y");
            Z = info.GetValue<float>("Z");
            WorldSpeedX = info.GetValue<float>("WorldSpeedX");
            WorldSpeedY = info.GetValue<float>("WorldSpeedY");
            WorldSpeedZ = info.GetValue<float>("WorldSpeedZ");
            XR = info.GetValue<float>("XR");
            Roll = info.GetValue<float>("Roll");
            ZR = info.GetValue<float>("ZR");
            XD = info.GetValue<float>("XD");
            Pitch = info.GetValue<float>("Pitch");
            ZD = info.GetValue<float>("ZD");
            SuspensionPositionRearLeft = info.GetValue<float>("SuspensionPositionRearLeft");
            SuspensionPositionRearRight = info.GetValue<float>("SuspensionPositionRearRight");
            SuspensionPositionFrontLeft = info.GetValue<float>("SuspensionPositionFrontLeft");
            SuspensionPositionFrontRight = info.GetValue<float>("SuspensionPositionFrontRight");
            SuspensionVelocityRearLeft = info.GetValue<float>("SuspensionVelocityRearLeft");
            SuspensionVelocityRearRight = info.GetValue<float>("SuspensionVelocityRearRight");
            SuspensionVelocityFrontLeft = info.GetValue<float>("SuspensionVelocityFrontLeft");
            SuspensionVelocityFrontRight = info.GetValue<float>("SuspensionVelocityFrontRight");
            WheelSpeedBackLeft = info.GetValue<float>("WheelSpeedBackLeft");
            WheelSpeedBackRight = info.GetValue<float>("WheelSpeedBackRight");
            WheelSpeedFrontLeft = info.GetValue<float>("WheelSpeedFrontLeft");
            WheelSpeedFrontRight = info.GetValue<float>("WheelSpeedFrontRight");
            Throttle = info.GetValue<float>("Throttle");
            Steer = info.GetValue<float>("Steer");
            Brake = info.GetValue<float>("Brake");
            Clutch = info.GetValue<float>("Clutch");
            Gear = info.GetValue<float>("Gear");
            LateralAcceleration = info.GetValue<float>("LateralAcceleration");
            LongitudinalAcceleration = info.GetValue<float>("LongitudinalAcceleration");
            EngineRevs = info.GetValue<float>("EngineRevs");          
        }

        public float Time;
        public float LapTime;
        public float LapDistance;
        public float Distance;
        [XmlIgnore]
        public float X;
        [XmlIgnore]
        public float Y;
        [XmlIgnore]
        public float Z;        
        public float Speed;
        [XmlIgnore]
        public float WorldSpeedX;
        [XmlIgnore]
        public float WorldSpeedY;
        [XmlIgnore]
        public float WorldSpeedZ;
        [XmlIgnore]
        public float XR;
        [XmlIgnore]
        public float Roll;
        [XmlIgnore]
        public float ZR;
        [XmlIgnore]
        public float XD;
        [XmlIgnore]
        public float Pitch;
        [XmlIgnore]
        public float ZD;
        [XmlIgnore]
        public float SuspensionPositionRearLeft;
        [XmlIgnore]
        public float SuspensionPositionRearRight;
        [XmlIgnore]
        public float SuspensionPositionFrontLeft;
        [XmlIgnore]
        public float SuspensionPositionFrontRight;
        [XmlIgnore]
        public float SuspensionVelocityRearLeft;
        [XmlIgnore]
        public float SuspensionVelocityRearRight;
        [XmlIgnore]
        public float SuspensionVelocityFrontLeft;
        [XmlIgnore]
        public float SuspensionVelocityFrontRight;
        [XmlIgnore]
        public float WheelSpeedBackLeft;
        [XmlIgnore]
        public float WheelSpeedBackRight;
        [XmlIgnore]        
        public float WheelSpeedFrontLeft;
        [XmlIgnore]
        public float WheelSpeedFrontRight;
        [XmlIgnore]
        public float Throttle;
        [XmlIgnore]
        public float Steer;
        [XmlIgnore]
        public float Brake;
        [XmlIgnore]
        public float Clutch;
        [XmlIgnore]
        public float Gear;
        [XmlIgnore]
        public float LateralAcceleration;
        [XmlIgnore]
        public float LongitudinalAcceleration;        
        public float Lap;
        [XmlIgnore]
        public float EngineRevs;
        [XmlIgnore]
        public float SpeedInKmPerHour
        {
            get { return Speed*3.60f;  }
        }

        public override string ToString()
        {
            return "Lap: " + Lap + ", " +
                   "Time: " + Time + ", " +
                   "LapTime: " + LapTime + ", " +
                   "LapDistance: " + LapDistance + ", " +
                   "Distance: " + Distance + ", " +
                   "Speed: " + Speed;
        }        

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Time", Time);
            info.AddValue("LapTime", LapTime);
            info.AddValue("LapDistance", LapDistance);
            info.AddValue("Distance", Distance);
            info.AddValue("X", X);
            info.AddValue("Y", Y);
            info.AddValue("Z", Z);
            info.AddValue("Speed", Speed);
            info.AddValue("WorldSpeedX", WorldSpeedX);
            info.AddValue("WorldSpeedY", WorldSpeedY);
            info.AddValue("WorldSpeedZ", WorldSpeedZ);
            info.AddValue("XR", XR);
            info.AddValue("Roll", Roll);
            info.AddValue("ZR", ZR);
            info.AddValue("XD", XD);
            info.AddValue("Pitch", Pitch);
            info.AddValue("ZD", ZD);
            info.AddValue("SuspensionPositionRearLeft", SuspensionPositionRearLeft);
            info.AddValue("SuspensionPositionRearRight", SuspensionPositionRearRight);
            info.AddValue("SuspensionPositionFrontLeft", SuspensionPositionFrontLeft);
            info.AddValue("SuspensionPositionFrontRight", SuspensionPositionFrontRight);
            info.AddValue("SuspensionVelocityRearLeft", SuspensionVelocityRearLeft);
            info.AddValue("SuspensionVelocityRearRight", SuspensionVelocityRearRight);
            info.AddValue("SuspensionVelocityFrontLeft", SuspensionVelocityFrontLeft);
            info.AddValue("SuspensionVelocityFrontRight", SuspensionVelocityFrontRight);
            info.AddValue("WheelSpeedBackLeft", WheelSpeedBackLeft);
            info.AddValue("WheelSpeedBackRight", WheelSpeedBackRight);
            info.AddValue("WheelSpeedFrontLeft", WheelSpeedFrontLeft);
            info.AddValue("WheelSpeedFrontRight", WheelSpeedFrontRight);
            info.AddValue("Throttle", Throttle);
            info.AddValue("Steer", Steer);
            info.AddValue("Brake", Brake);
            info.AddValue("Clutch", Clutch);
            info.AddValue("Gear", Gear);
            info.AddValue("LateralAcceleration", LateralAcceleration);
            info.AddValue("LongitudinalAcceleration", LongitudinalAcceleration);
            info.AddValue("Lap", Lap);
            info.AddValue("EngineRevs", EngineRevs);
        }        
    }
}
