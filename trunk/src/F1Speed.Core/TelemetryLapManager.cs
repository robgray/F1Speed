using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core.Repositories;
using log4net;

namespace F1Speed.Core
{
    public enum ComparisonModeEnum
    {
        FastestLap,
        Reference
    };

    public class TelemetryLapManager
    {
        private static ILog logger = Logger.Create();

        private string circuitName;
        private string lapType;
        
        private readonly ITelemetryLapRepository fastestLapRepository;
        private readonly IEnumerable<ITelemetryLapRepository> currentLapExporters;
        private readonly IList<TelemetryLap> laps;
        
        public TelemetryLap ReferenceLap { get; private set; }
        public TelemetryLap FastestLap { get; set; }

        private bool hasDataBeenReceived;

        public TelemetryLapManager()
        {            
            laps = new List<TelemetryLap>();
            fastestLapRepository = new BinaryTelemetryLapRepository();

            currentLapExporters = new List<ITelemetryLapRepository>
                                      {
                                          new F1PerfViewTelemetryLapRepository()                                         
                                      };

            hasDataBeenReceived = false;
        }

        public ComparisonModeEnum ComparisonMode { get; private set; } 

        public void SetReferenceLap(TelemetryLap referenceLap)
        {
            

            ReferenceLap = referenceLap;
            if (referenceLap != null)
            {
                logger.Debug("Reference lap loaded for " + referenceLap.CircuitName + " '" + referenceLap.LapType + "'");
                ComparisonMode = ComparisonModeEnum.Reference;
            }
            else
            {
                logger.Debug("Reference lap cleared");
            }
        }

        public float CurrentThrottle
        {
            get
            {

                return LatestPacket.Throttle;
            }
        }

        public TelemetryPacket LatestPacket
        {
            get
            {
                if (CurrentLap == null || !CurrentLap.Packets.Any())
                    return new TelemetryPacket();

                return CurrentLap.Packets.Last();
            }
        }

        public float CurrentBrake
        {
            get { return LatestPacket.Brake; }
        }

        public void ChangeCircuit(string circuitName, string lapType)
        {
            logger.Debug("Current circuit changed to " + circuitName + " '" + lapType + "'");


            ReferenceLap = null;
            ComparisonMode = ComparisonModeEnum.FastestLap;
            this.circuitName = circuitName;
            this.lapType = lapType;
            LoadFastestLap();
        }

        public bool HasDataBeenReceived
        {
            get { return hasDataBeenReceived; }
            set
            {
                if (value == hasDataBeenReceived)
                    return;

                hasDataBeenReceived = value;
                if (hasDataBeenReceived)
                    logger.Debug("Data connection established");
            }
        }

        public void AddPacket(TelemetryPacket packet)
        {
            HasDataBeenReceived = true;
            if (F1SpeedSettings.LogPacketData)
                logger.Debug(packet.ToString());

                   
            if (HasLapChanged(packet))
            {
                logger.Debug("Old Lap=" + CurrentLap.LapNumber + "   New Lap=" + packet.Lap);

                // If laptime 0 car is back in garage.
                if (packet.LapTime > 0) 
                    CurrentLap.MarkLapCompleted();
                
                if (CurrentLap.IsCompleteLap)
                {
                    if (string.IsNullOrEmpty(CurrentLap.CircuitName))
                        CurrentLap.CircuitName = circuitName;
                    if (string.IsNullOrEmpty(CurrentLap.LapType))
                        CurrentLap.LapType = lapType;

                    foreach (var exporter in currentLapExporters) exporter.Save(CurrentLap);                         
                    
                    if (FastestLap == null || CurrentLap.LapTime < FastestLap.LapTime)
                    {
                        logger.Debug(string.Format("Set new Fastest Lap.  (old={0}.  new={1})",
                                                   FastestLap != null ? FastestLap.LapTime.AsTimeString() : "Nothing",
                                                   CurrentLap.LapTime.AsTimeString()));
                        FastestLap = CurrentLap;
                        SaveFastestLap();                       
                    }                                                     
                } else
                {
                    // Lap is invalid
                    laps.Remove(CurrentLap);
                }

                // Start new current lap
                laps.Add(new TelemetryLap(circuitName, lapType));                                         
            }           
            
            CurrentLap.AddPacket(packet);
        }

        protected bool HasLapChanged(TelemetryPacket packet)
        {
            if (CurrentLap == null)
            {
                logger.Debug("Lap has changed.  No previous lap");
                return true;
            }

            if (packet.Lap != CurrentLap.LapNumber)
            {
                logger.Debug("Lap has changed - current lap number changed");
                return true;
            }

            if (packet.LapTime == 0 && LatestPacket.LapTime > 0)
                return true;
           
            return false;
        }

        protected TelemetryLap CurrentLap
        {
            get
            {
                if (laps.Count == 0)
                    laps.Add(new TelemetryLap(circuitName, lapType));

                return laps.Last();
            }
        }

        public void SaveFastestLap()
        {
            fastestLapRepository.Save(FastestLap);
        }

        public void LoadFastestLap()
        {
            MigrateXmlLapsToBinary();

            var fastestLap = fastestLapRepository.Get(circuitName, lapType);            
            FastestLap = fastestLap;
        }

        private void MigrateXmlLapsToBinary()
        {
            var xmlRepo = new XmlTelemetryLapRepository();
            var xmlFastestLap = xmlRepo.Get(circuitName, lapType);
            if (xmlFastestLap != null)
            {
                xmlFastestLap.CircuitName = circuitName;
                xmlFastestLap.LapType = lapType;
                fastestLapRepository.Save(xmlFastestLap);
                xmlRepo.Delete(xmlFastestLap);
            }
        }

        public string GetSpeedDelta()
        {
            if (ComparisonLap == null || CurrentLap == null || !CurrentLap.Packets.Any())
                return "--";

            var comparisonLapPacket = ComparisonLap.GetPacketClosestTo(LatestPacket);

            var difference = LatestPacket.SpeedInKmPerHour - comparisonLapPacket.SpeedInKmPerHour;
            return string.Format("{0}{1:0.0}",
                                 difference > 0 ? "+" : "", difference);
        }

        public TelemetryLap ComparisonLap
        {
            get
            {
                return ComparisonMode == ComparisonModeEnum.Reference ? ReferenceLap : FastestLap;
            }
        }

        public float GetTimeDelta()
        {
            if (ComparisonLap == null || CurrentLap == null || !CurrentLap.Packets.Any())
                return 0f;

            var currentPacket = LatestPacket;
            var comparisonLapPacket = ComparisonLap.GetPacketClosestTo(currentPacket);

            var difference = currentPacket.LapTime - comparisonLapPacket.LapTime;
            return -difference;
        }
    
        public string ComparisonLapTime
        {
            get
            {
                if (ComparisonLap == null)
                    return "";

                return ComparisonLap.LapTime.AsTimeString();
            }
        }

        public string CurrentLapTime
        {
            get 
            {
                if (!CurrentLap.IsFirstPacketStartLine)
                    return "";

                return CurrentLap.LapTime.AsTimeString();
            }
        }

        public string AverageLapTime
        {
            get
            {
                if (!laps.Any(lap => lap.HasLapFinished))
                    return "";

                return laps.Where(lap => lap.HasLapFinished).Average(lap => lap.LapTime).AsTimeString();
            }
        }
    }
}
