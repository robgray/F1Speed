using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace F1Speed.Core.Repositories
{
    public class CsvTelemetryLapRepository : TelemetryLapRepository, ITelemetryLapRepository
    {
        private IFileSystemFacade fileSystem;

        public CsvTelemetryLapRepository() : this(new FileSystemFacade()) { }

        public CsvTelemetryLapRepository(IFileSystemFacade fileSystemFacade)
        {
            fileSystem = fileSystemFacade;
        }

        public override void Save(TelemetryLap lap)
        {
            Save(lap, GetFileName(lap));
        }

        public void Save(TelemetryLap lap, string fileName)
        {
            try
            {
                if (lap == null)
                    return;

                var outputBuffer = new List<string>();
                outputBuffer.AddRange(GetHeaderLines(lap));
                outputBuffer.AddRange(GetPacketData(lap));

                if (!fileSystem.DirectoryExists(DataFolder()))
                    fileSystem.CreateDirectory(DataFolder());

                if (fileSystem.FileExists(fileName))
                    fileSystem.DeleteFile(fileName);

                fileSystem.WriteAllLines(fileName, outputBuffer);
            } catch (Exception ex)
            {
                logger.Error("Could not save csv telemetry lap", ex);
                throw ex;
            }
        }

        private IEnumerable<string> GetHeaderLines(TelemetryLap lap)
        {
            return new[]
                       {
                           "\"time\",\"laptime\",\"lapdistance\",\"distance\",\"x\",\"y\",\"z\",\"speed\",\"worldspeedx\",\"worldspeedy\",\"worldspeedz\",\"xr\",\"roll\",\"zr\",\"xd\",\"pitch\","+
                           "\"zd\",\"suspensionpositionrearleft\"," +
                           "\"suspensionpositionrearright\",\"suspensionpositionfrontleft\",\"suspensionpositionfrontright\",\"suspensionvelocityrearleft\",\"suspensionvelocityrearright\"," +
                           "\"suspensionvelocityfrontleft\",\"suspensionvelocityfrontright\",\"wheelspeedrearleft\",\"wheelspeedrearright\",\"wheelspeedfrontleft\",\"wheelspeedfrontright\"," +
                           "\"throttle\",\"steer\",\"brake\",\"clutch\",\"gear\",\"lateralacceleration\",\"longitudinalacceleration\",\"lap\",\"enginerevs\""
                       };
        }

        protected override string GetFileName(TelemetryLap lap)
        {
            return string.Format(@"{0}\{1}_{2}_{3:0.000}.{4}", DataFolder(), 
                lap.CircuitName, lap.LapType, lap.LapTime, GetFileExtension());
        }

        private IEnumerable<string> GetPacketData(TelemetryLap lap)
        {
            var data = new List<string>();
            foreach(var packet in lap.Packets)
            {
                data.Add(
                        packet.Time + ", " +
                        packet.LapTime + ", " +
                        packet.LapDistance + ", " +
                        packet.Distance + ", " +
                        packet.X + ", " +
                        packet.Y + ", " +
                        packet.Z + ", " +
                        packet.Speed + ", " +
                        packet.WorldSpeedX + ", " +
                        packet.WorldSpeedY + ", " +
                        packet.WorldSpeedZ + ", " +
                        packet.XR + ", " +
                        packet.Roll + ", " +
                        packet.ZR + ", " +
                        packet.XD + ", " +
                        packet.Pitch + ", " +
                        packet.ZD + ", " +
                        packet.SuspensionPositionRearLeft + ", " +
                        packet.SuspensionPositionRearRight + ", " +
                        packet.SuspensionPositionFrontLeft + ", " +
                        packet.SuspensionPositionFrontRight + ", " +
                        packet.SuspensionVelocityRearLeft + ", " +
                        packet.SuspensionVelocityRearRight + ", " +
                        packet.SuspensionVelocityFrontLeft + ", " +
                        packet.SuspensionVelocityFrontRight + ", " +
                        packet.WheelSpeedBackLeft + ", " +
                        packet.WheelSpeedBackRight + ", " +
                        packet.WheelSpeedFrontLeft + ", " +
                        packet.WheelSpeedFrontRight + ", " +
                        packet.Throttle + ", " +
                        packet.Steer + ", " +
                        packet.Brake + ", " +
                        packet.Clutch + ", " +
                        packet.Gear + ", " +
                        packet.LateralAcceleration + ", " +
                        packet.LongitudinalAcceleration + ", " +
                        packet.Lap + ", " +
                        packet.EngineRevs
                    );
            }

            return data;
        }

        public override TelemetryLap Get(string circuitName, string lapType)
        {
            throw new NotImplementedException();
        }

        public override string GetFileExtension()
        {
            return "csv";
        }
    }
}
