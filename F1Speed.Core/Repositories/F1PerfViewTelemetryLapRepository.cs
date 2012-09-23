using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;

namespace F1Speed.Core.Repositories
{
    /// <summary>
    /// Exports the files to F1PerfView
    /// </summary>
    public class F1PerfViewTelemetryLapRepository : ITelemetryLapRepository
    {
        private IFileSystemFacade fileSystem;
        private static ILog logger = Logger.Create();

        public F1PerfViewTelemetryLapRepository() : this(new FileSystemFacade()) { }
        
        public F1PerfViewTelemetryLapRepository(IFileSystemFacade fileSystemFacade)
        {
            fileSystem = fileSystemFacade;
        }
        
        private IDictionary<string, string> trackNumbers = new Dictionary<string, string>
                                                               {
                                                                   { "Australia", "1" },
                                                                   { "Bahrain", "19" },
                                                                   { "Malaysia", "2" },
                                                                   { "Shanghai", "3" },
                                                                   { "Barcelona", "4" },
                                                                   { "Monaco", "5" },
                                                                   { "Turkey", "6" },
                                                                   { "Canada", "7" },
                                                                   { "Valencia", "8" },
                                                                   { "Britian", "9" },
                                                                   { "Hockenheimring", "20" },
                                                                   { "Nurburgring", "10" },
                                                                   { "Hungoraring", "11" },
                                                                   { "Belgium", "12" },
                                                                   { "Italy", "13" },
                                                                   { "Singapore", "14" },
                                                                   { "Korea", "15" },
                                                                   { "Brazil", "16" },
                                                                   { "Interlagos", "17" },
                                                                   { "Abu Dahbi", "18" },
                                                                   { "India", "19" },
                                                                   { "United States (COTA)", "19" },
                                                               };

        public void Save(TelemetryLap lap)
        {
            try
            {
                if (!lap.IsFirstPacketStartLine ||
                    !lap.HasLapFinished ||
                    string.IsNullOrEmpty(lap.CircuitName))
                    return;

                var outputBuffer = new List<string>();
                outputBuffer.AddRange(GetHeaderLines(lap));
                outputBuffer.AddRange(GetPacketData(lap));

                // write to file            
                var directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var userFilePath = Path.Combine(directory, "F1Speed", "F1PerfView Data");

                if (!fileSystem.DirectoryExists(userFilePath))
                    fileSystem.CreateDirectory(userFilePath);

                var fileName = userFilePath + @"\" + FileName(lap);

                if (fileSystem.FileExists(fileName))
                    fileSystem.DeleteFile(fileName);

                fileSystem.WriteAllLines(fileName, outputBuffer);
                
            } catch (Exception ex)
            {
                logger.Error("Could not retreive binary telemetry lap", ex);
                throw ex;
            }
        }

        public void Save(TelemetryLap lap, string fileName)
        {
            if (!lap.IsFirstPacketStartLine ||
                !lap.HasLapFinished ||
                string.IsNullOrEmpty(lap.CircuitName))
                return;

            var outputBuffer = new List<string>();
            outputBuffer.AddRange(GetHeaderLines(lap));
            outputBuffer.AddRange(GetPacketData(lap));

            if (fileSystem.FileExists(fileName))
                fileSystem.DeleteFile(fileName);

            fileSystem.WriteAllLines(fileName, outputBuffer);
        }

        private IEnumerable<string> GetHeaderLines(TelemetryLap lap)
        {
            return new []
                       {
                           string.Format("\"F1Speed Export - {0}\",\"{1:yyyy-MM-dd HH:mm}\", {2}, {3}, {4}",
                                         lap.CircuitName, DateTime.Now, lap.LapTime, GetTrackNumber(lap), lap.LapNumber),
                           "\"time\",\"dist\",\"speed\",\"acc_long\",\"acc_lat\",\"revs\",\"throttle\",\"brake\",\"steer\",\"gear\",\"st_lr\",\"st_rr\",\"st_lf\",\"st_rf\",\"ws_lr\",\"ws_rr\",\"ws_lf\",\"ws_rf\",\"pos_x\",\"pos_y\""
                       };
        }

        private string FileName(TelemetryLap lap)
        {
            return string.Format("{0}_{1}.csv", lap.CircuitName, lap.LapTime.ToString("#.000"));
        }

        private string GetTrackNumber(TelemetryLap lap)
        {
            return trackNumbers[lap.CircuitName];
        }

        private IEnumerable<string> GetPacketData(TelemetryLap lap)
        {
            var data = new List<string>();

            foreach(var packet in lap.Packets)
            {
                string dataLine =
                    packet.LapTime.ToString("0.000") + ", " +
                    packet.LapDistance.ToString("0.000") + ", " +
                    packet.Speed.ToString("0.0000") + ", " +
                    packet.LongitudinalAcceleration.ToString("0.000") + ", " +
                    packet.LateralAcceleration.ToString("0.000") + ", " +
                    packet.EngineRevs.ToString("0.000") + ", " +
                    (Math.Truncate(packet.Throttle * 100000) / 1000).ToString("0.000") + ", " +
                    (Math.Truncate(packet.Brake * 100000) / 1000).ToString("0.000") + ", " +
                    packet.Steer.ToString("0.000") + ", " +
                    packet.Gear.ToString("0") + ", " +
                    packet.SuspensionPositionRearLeft.ToString("0.000000") + ", " +
                    packet.SuspensionPositionRearRight.ToString("0.000000") + ", " +
                    packet.SuspensionPositionFrontLeft.ToString("0.000000") + ", " +
                    packet.SuspensionPositionFrontRight.ToString("0.000000") + ", " +
                    packet.WheelSpeedBackLeft.ToString("0.0000") + ", " +
                    packet.WheelSpeedBackRight.ToString("0.0000") + ", " +                    
                    packet.WheelSpeedFrontLeft.ToString("0.0000") + ", " +
                    packet.WheelSpeedFrontRight.ToString("0.0000") + ", " +
                    packet.Z.ToString("0.000") + ", " +
                    packet.X.ToString("0.000");     
                          
                data.Add(dataLine);
            }

            return data;
        }

        #region ITelemetryLapRepository Members

        public TelemetryLap Get(string circuitName, string lapType)
        {
            throw new NotImplementedException();
        }

        public void Delete(TelemetryLap lap)
        {
            try
            {
                var fileName = FileName(lap);
                if (fileSystem.FileExists(fileName))
                    fileSystem.DeleteFile(fileName);
            } catch (Exception ex)
            {
                logger.Error("Could not delete f1perfview telemetry lap", ex);
                throw ex;
            }
        }

        #endregion
    }
}
