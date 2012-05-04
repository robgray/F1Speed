using Migrator.Framework;
using System.Data;

namespace F1Speed.Schema
{
    [Migration(2)]
    public class _002_CreateLapSampleTable : Migration
    {

        public override void Down()
        {
            Database.RemoveTable("LapSample");
        }

        public override void Up()
        {
            Database.AddTable("LapSample", 
                    new Column("SampleId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                    new Column("Time", DbType.Double),
                    new Column("LapTime", DbType.Double),
                    new Column("LapDistance", DbType.Double),
                    new Column("X", DbType.Double),
                    new Column("Y", DbType.Double),
                    new Column("Z", DbType.Double),
                    new Column("Speed", DbType.Double),
                    new Column("WorldSpeedX", DbType.Double),
                    new Column("WorldSpeedY", DbType.Double),
                    new Column("WorldSpeedZ", DbType.Double),
                    new Column("XR", DbType.Double),
                    new Column("Roll", DbType.Double),
                    new Column("ZR", DbType.Double),
                    new Column("XD", DbType.Double),
                    new Column("Pitch", DbType.Double),
                    new Column("ZD", DbType.Double),
                    new Column("SuspensionPositionRearLeft", DbType.Double),
                    new Column("SuspensionPositionRearRight", DbType.Double),
                    new Column("SuspensionPositionFrontLeft", DbType.Double),
                    new Column("SuspensionPositionFrontRight", DbType.Double),
                    new Column("SuspensionVelocityRearLeft", DbType.Double),
                    new Column("SuspensionVelocityRearRight", DbType.Double),
                    new Column("SuspensionVelocityFrontLeft", DbType.Double),
                    new Column("SuspensionVelocityFrontRight", DbType.Double),
                    new Column("WheelSpeedRearLeft", DbType.Double),
                    new Column("WheelSpeedRearRight", DbType.Double),
                    new Column("WheelSpeedFrontLeft", DbType.Double),
                    new Column("WheelSpeedFrontRight", DbType.Double),
                    new Column("Throttle", DbType.Double),
                    new Column("Steer", DbType.Double),
                    new Column("Brake", DbType.Double),
                    new Column("Clutch", DbType.Double),
                    new Column("Gear", DbType.Double),
                    new Column("LateralAcceleration", DbType.Double),
                    new Column("LongitudinalAcceleration", DbType.Double),
                    new Column("Lap", DbType.Double),
                    new Column("EngineRevs", DbType.Double),
                    new Column("F1SpeedLapId", DbType.Int32, ColumnProperty.NotNull)
                );
        }
    }
}
