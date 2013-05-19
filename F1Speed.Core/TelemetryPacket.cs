using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using F1Speed.Core.Helpers;

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
            
            NewField1 = info.GetValue<float>("NewField1");
            RacePosition = info.GetValue<float>("RacePosition");
            KersRemaining = info.GetValue<float>("KersRemaining");
            KersRecharge = info.GetValue<float>("KersRecharge");
            DrsStatus = info.GetValue<float>("DrsStatus");
            Difficulty = info.GetValue<float>("Difficulty");
            Assists = info.GetValue<float>("Assists");
            FuelRemaining = info.GetValue<float>("FuelRemaining");
            SessionType = info.GetValue<float>("SessionType");
            NewField10 = info.GetValue<float>("NewField10");
            Sector = info.GetValue<float>("Sector");
            TimeSector1 = info.GetValue<float>("TimeSector1");
            TimeSector2 = info.GetValue<float>("TimeSector2");
            BrakeTemperatureRearLeft = info.GetValue<float>("BrakeTemperatureRearLeft");
            BrakeTemperatureRearRight = info.GetValue<float>("BrakeTemperatureRearRight");
            BrakeTemperatureFrontLeft = info.GetValue<float>("BrakeTemperatureFrontLeft");
            BrakeTemperatureFrontRight = info.GetValue<float>("BrakeTemperatureFrontRight");
            NewField18 = info.GetValue<float>("NewField18");
            NewField19 = info.GetValue<float>("NewField19");
            NewField20 = info.GetValue<float>("NewField20");
            NewField21 = info.GetValue<float>("NewField21");
            CompletedLapsInRace = info.GetValue<float>("CompletedLapsInRace");
            TotalLapsInRace = info.GetValue<float>("TotalLapsInRace");
            TrackLength = info.GetValue<float>("TrackLength");
            PreviousLapTime = info.GetValue<float>("PreviousLapTime");            
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

        /* New Fields in Patch 12 */
        [XmlIgnore]
        public float NewField1;     // Always 1?
        [XmlIgnore]
        public float RacePosition;     // Position in race
        [XmlIgnore]
        public float KersRemaining;     // Kers Remaining
        [XmlIgnore]
        public float KersRecharge;     // Always 400000? 
        [XmlIgnore]
        public float DrsStatus;     // Drs Status
        [XmlIgnore]
        public float Difficulty;     // 2 = Medium or Easy, 1 = Hard, 0 = Expert
        [XmlIgnore]
        public float Assists;     // 0 = All assists are off.  1 = some assist is on.
        [XmlIgnore]
        public float FuelRemaining;      // Not sure if laps or Litres?
        [XmlIgnore]
        public float SessionType;   // 9.5 = race, 10 = time trail / time attack, 170 = quali, practice, championsmode
        [XmlIgnore]
        public float NewField10;
        [XmlIgnore]
        public float Sector;    // Sector (0, 1, 2)
        [XmlIgnore]
        public float TimeSector1;    // Time Intermediate 1
        [XmlIgnore]
        public float TimeSector2;    // Time Intermediate 2
        [XmlIgnore]
        public float BrakeTemperatureRearLeft;
        [XmlIgnore]
        public float BrakeTemperatureRearRight;
        [XmlIgnore]
        public float BrakeTemperatureFrontLeft;
        [XmlIgnore]
        public float BrakeTemperatureFrontRight;
        [XmlIgnore]
        public float NewField18;    // Always 0?
        [XmlIgnore]
        public float NewField19;    // Always 0?
        [XmlIgnore]
        public float NewField20;    // Always 0?
        [XmlIgnore]
        public float NewField21;    // Always 0?
        [XmlIgnore]
        public float CompletedLapsInRace;    // Number of laps Completed (in GP only)
        [XmlIgnore]
        public float TotalLapsInRace;    // Number of laps in GP (GP only)
        [XmlIgnore]
        public float TrackLength;    // Track Length
        [XmlIgnore]
        public float PreviousLapTime;    // Lap time of previous lap

        /* End new Fields */

        [XmlIgnore]
        public float SpeedInKmPerHour
        {
            get { return Speed * 3.60f; }
        }

        [XmlIgnore]
        public bool IsSittingInPits
        {
            get { return Math.Abs(LapTime - 0) < Constants.Epsilon && Math.Abs(Speed - 0) < Constants.Epsilon; }
        }

        [XmlIgnore]
        public bool IsInPitLane
        {
            get { return Math.Abs(LapTime - 0) < Constants.Epsilon; }
        }

        [XmlIgnore]
        public string SessionTypeName
        {
            get
            {
                if (Math.Abs(this.SessionType - 9.5f) < 0.0001f)
                    return "Race";
                if (Math.Abs(this.SessionType - 10f) < 0.0001f)
                    return "Time Trial";
                if (Math.Abs(this.SessionType - 170f) < 0.0001f)
                    return "Qualifying or Practice";                
                return "Other";
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var fields = this.GetType().GetFields();
            foreach (var field in fields)
            {            
                sb.AppendFormat("{0}({1}) : ", field.Name, field.GetValue(this));            
            }

            var props = this.GetType().GetProperties();
            foreach (var prop in props)
                sb.AppendFormat("{0}({1}) : ", prop.Name, prop.GetValue(this, null));

            return sb.ToString();
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

            info.AddValue("NewField1", NewField1);
            info.AddValue("RacePosition", RacePosition);
            info.AddValue("KersRemaining", KersRemaining);
            info.AddValue("KersRecharge", KersRecharge);
            info.AddValue("DrsStatus", DrsStatus);
            info.AddValue("Difficulty", Difficulty);
            info.AddValue("Assists", Assists);
            info.AddValue("FuelRemaining", FuelRemaining);
            info.AddValue("SessionType", SessionType);
            info.AddValue("NewField10", NewField10);
            info.AddValue("Sector", Sector);
            info.AddValue("TimeSector1", TimeSector1);
            info.AddValue("TimeSector2", TimeSector2);
            info.AddValue("BrakeTemperatureRearLeft", BrakeTemperatureRearLeft);
            info.AddValue("BrakeTemperatureRearRight", BrakeTemperatureRearRight);
            info.AddValue("BrakeTemperatureFrontLeft", BrakeTemperatureFrontLeft);
            info.AddValue("BrakeTemperatureFrontRight", BrakeTemperatureFrontRight);
            info.AddValue("NewField18", NewField18);
            info.AddValue("NewField19", NewField19);
            info.AddValue("NewField20", NewField20);
            info.AddValue("NewField21", NewField21);
            info.AddValue("CompletedLapsInRace", CompletedLapsInRace);
            info.AddValue("TotalLapsInRace", TotalLapsInRace);
            info.AddValue("TrackLength", TrackLength);
            info.AddValue("PreviousLapTime", PreviousLapTime);
        }
    }
}
