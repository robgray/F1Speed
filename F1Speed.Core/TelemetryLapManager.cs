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

    public enum WheelspinWheel
    {
        FrontLeft,
        FrontRight,
        RearLeft,
        RearRight       
    };

    public delegate void CompletedFullLapEventHandler(object sender, CompletedFullLapEventArgs e);
    public delegate void LapEventHandler(object sender, LapEventArgs e);
    public delegate void PacketEventHandler(object sender, PacketEventArgs e);
    public delegate void SectorChangedEventHandler(object sender, SectorChangedEventArgs e);
    public delegate void CircuitChangedEventHandler(object sender, CircuitChangedEventArgs e);

    public class TelemetryLapManager
    {
        private static object syncLock = new object();

        private readonly static ILog logger = Logger.Create();

        private float _lastLapTime = 0;
        private float _lastLapFuel = 0;

        private Circuit _currectCircuit = Circuit.NullCircuit;
        private string _lapType;

        private readonly ITelemetryLapRepository _fastestLapRepository;        
        private readonly IEnumerable<ITelemetryLapRepository> _currentLapExporters;
        private readonly IList<TelemetryLap> _laps;

        public event CompletedFullLapEventHandler CompletedFullLap;
        public event LapEventHandler ReturnedToGarage;
        public event LapEventHandler SetFastestLap;
        public event LapEventHandler StartedOutLap;
        public event LapEventHandler FinishedOutLap;
        public event LapEventHandler RemovedLap;
        public event LapEventHandler StartedLap;
        public event PacketEventHandler PacketProcessed;
        public event CircuitChangedEventHandler CircuitChanged;
        public event SectorChangedEventHandler SectorChanged;

        public TelemetryLap ReferenceLap { get; private set; }
        public TelemetryLap FastestLap { get; set; }

        private bool _hasDataBeenReceived;

        protected void OnPacketProcessed(TelemetryPacket packet)
        {
            logger.Debug(packet.ToString());

            if (PacketProcessed != null)
                PacketProcessed(this, new PacketEventArgs { Packet = packet });
        }

        protected void OnStartedLap(TelemetryLap lap)
        {
            if (StartedLap != null)
                StartedLap(this, new LapEventArgs { Lap = lap });
        }

        protected void OnRemovedLap(TelemetryLap lap)
        {
            if (RemovedLap != null)
                RemovedLap(this, new LapEventArgs { Lap = lap });
        }

        protected void OnStartedOutLap(TelemetryLap lap)
        {
            if (StartedOutLap != null)
                StartedOutLap(this, new LapEventArgs { Lap = lap });
        }

        protected void OnFinishedOutLap(TelemetryLap lap)
        {
            if (FinishedOutLap != null)
                FinishedOutLap(this, new LapEventArgs { Lap = lap });
        }

        protected void OnCompletedFullLap(CompletedFullLapEventArgs e)
        {
            if (CompletedFullLap != null)
                CompletedFullLap(this, e);
        }

        protected void OnReturnedToGarage(TelemetryLap lap)
        {
            if (ReturnedToGarage != null)
                ReturnedToGarage(this, new LapEventArgs { Lap = lap });
        }

        protected void OnCircuitChanged(string circuitName, string lapType)
        {
            if (CircuitChanged != null)
                CircuitChanged(this, new CircuitChangedEventArgs { CircuitName = circuitName, LapType = lapType});
        }

        protected void OnSectorChanged(int sector, float time, float fastest)
        {
            if (SectorChanged != null)
            {
                SectorChanged(this, new SectorChangedEventArgs
                    {
                        Sector = sector,
                        Time = time,
                        Fastest = fastest,
                        Delta = fastest - time
                    });
            }
        }

        protected void OnSetFastestLap(LapEventArgs e, TelemetryLap oldFastestLap)
        {
            logger.Info(string.Format("Set new Fastest Lap.  (old={0}.  new={1})",
                           oldFastestLap != null ? oldFastestLap.LapTime.AsTimeString() : "Nothing",
                           e.Lap.LapTime.AsTimeString()));

            if (SetFastestLap != null)
                SetFastestLap(this, e);
        }

        public TelemetryLapManager()
        {
            _laps = new List<TelemetryLap>();
            _fastestLapRepository = new BinaryTelemetryLapRepository();             
            _currentLapExporters = new List<ITelemetryLapRepository>
                                      {
                                          new F1PerfViewTelemetryLapRepository()                                         
                                      };

            _hasDataBeenReceived = false;
        }

        public ComparisonModeEnum ComparisonMode { get; private set; }

        public void SetReferenceLap(TelemetryLap referenceLap)
        {
            ReferenceLap = referenceLap;
            if (referenceLap != null)
            {
                logger.Info("Reference lap loaded for " + referenceLap.CircuitName + " '" + referenceLap.LapType + "'");
                ComparisonMode = ComparisonModeEnum.Reference;
            }
            else
            {
                logger.Info("Reference lap cleared");
                ComparisonMode = ComparisonModeEnum.FastestLap;
            }
        }

        public float CurrentThrottle
        {
            get
            {

                return LatestPacket.Throttle;
            }
        }

        public Circuit Circuit { get { return _currectCircuit; }}

        public TelemetryPacket LatestPacket
        {
            get
            {
                lock (syncLock)
                {
                    if (CurrentLap == null || !CurrentLap.Packets.Any())
                        return new TelemetryPacket();

                    return CurrentLap.Packets.Last();
                }
            }
        }

        public float CurrentBrake
        {
            get { return LatestPacket.Brake; }
        }

        public float CurrentWheelSpin(WheelspinWheel wheel)
        {
            switch (wheel)
            {
                case WheelspinWheel.FrontLeft:
                    return LatestPacket.WheelSpeedFrontLeft - LatestPacket.Speed;
                case WheelspinWheel.FrontRight:
                    return LatestPacket.WheelSpeedFrontRight - LatestPacket.Speed;
                case WheelspinWheel.RearLeft:
                    return LatestPacket.WheelSpeedBackLeft - LatestPacket.Speed;
                case WheelspinWheel.RearRight:
                    return LatestPacket.WheelSpeedBackRight - LatestPacket.Speed;
                default:
                    return 0.0f;
            }
        }

        public string GetCurrentData(string fieldName)
        {
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                var type = typeof(TelemetryPacket);
                var value = type.GetFields().First(x => x.Name == fieldName).GetValue(LatestPacket);

                return value.ToString();
            }
            return string.Empty;
        }

        public void ChangeCircuit(Circuit newCircuit, string lapType)
        {
            logger.Info("Current circuit changed to " + newCircuit.Name + " '" + (lapType ?? "(none)") + "'");

            ReferenceLap = null;
            ComparisonMode = ComparisonModeEnum.FastestLap;
            this._currectCircuit = newCircuit;            
            this._lapType = lapType ?? "";
            _laps.Clear();

            LoadFastestLap();

            OnCircuitChanged(this._currectCircuit.Name, this._lapType);
        }

        public bool HasDataBeenReceived
        {
            get { return _hasDataBeenReceived; }
            set
            {
                if (value == _hasDataBeenReceived)
                    return;

                _hasDataBeenReceived = value;
                if (_hasDataBeenReceived)
                    logger.Info("Data connection established");
            }
        }

        public bool HasSectorChanged(TelemetryPacket newPacket, TelemetryPacket previousPacket)
        {
            var previousSector = (int) previousPacket.Sector;
            var newSector = (int) newPacket.Sector;

            return (newSector != previousSector);
        }

        public void ProcessIncomingPacket(TelemetryPacket packet)
        {
            lock (syncLock)
            {
                HasDataBeenReceived = true;
                if (F1SpeedSettings.LogPacketData)
                    OnPacketProcessed(packet);

                CheckCircuit(packet);
                
                if (HasLapChanged(packet))
                {
                    if (!packet.IsInPitLane)
                    {
                        _lastLapTime = packet.PreviousLapTime;
                        _lastLapFuel = packet.FuelRemaining;
                        CurrentLap.MarkLapCompleted();
                    }
                    else
                        OnReturnedToGarage(CurrentLap);

                    if (CurrentLap.IsCompleteLap)
                    {
                        if (string.IsNullOrEmpty(CurrentLap.CircuitName) && string.IsNullOrEmpty(CurrentLap.LapType))
                            CurrentLap.LapType = _lapType;
                        
                        foreach (var exporter in _currentLapExporters)
                            exporter.Save(CurrentLap);

                        if (IsCurrentLapFastestLap)
                        {
                            OnSetFastestLap(new LapEventArgs { Lap = CurrentLap }, FastestLap);

                            FastestLap = CurrentLap;
                            SaveFastestLap();
                        }

                        OnCompletedFullLap(new CompletedFullLapEventArgs {
                            CompletedLap = CurrentLap,
                            CurrentLapNumber = (int)packet.Lap,
                            PreviousLapNumber = CurrentLap.LapNumber
                        });
                    }
                    else
                    {
                        if (CurrentLap.IsOutLap)
                            OnFinishedOutLap(CurrentLap);

                        // Lap is invalid
                        //_laps.Remove(CurrentLap);

                        //OnRemovedLap(CurrentLap);
                    }

                    // Start new current lap
                    _laps.Add(new TelemetryLap(_currectCircuit, _lapType));
                    CurrentLap.AddPacket(packet);
                    OnStartedLap(CurrentLap);
                }
                else
                {
                    CurrentLap.AddPacket(packet);
                }
            }  // end lock
        }

        private bool IsCurrentLapFastestLap
        {
            get { return FastestLap == null || CurrentLap.LapTime < FastestLap.LapTime; }
        }

        public string LapType { get { return _lapType;  } }

        private void CheckCircuit(TelemetryPacket packet)
        {
            var circuit = CircuitRepository.GetByTrackLength(packet.TrackLength);
            if (circuit.Name != _currectCircuit.Name || _lapType != packet.SessionTypeName)
            {
                this.ChangeCircuit(circuit, packet.SessionTypeName);
            }
        }

        protected bool HasLapChanged(TelemetryPacket packet)
        {
            if (CurrentLap == null)
            {
                logger.Info("Lap has changed.  No previous lap");
                return true;
            }

            if (packet.Lap > CurrentLap.LapNumber || packet.Lap < CurrentLap.LapNumber ||
                (Math.Abs(packet.Lap - CurrentLap.LapNumber) < Constants.Epsilon && CurrentLap.Distance < 0 && packet.Distance > 0))
            {
                logger.Info("Lap has changed - current lap number changed");
                return true;
            }

            return packet.LapTime <= 0 && LatestPacket.LapTime > 0;
        }

        protected TelemetryLap CurrentLap
        {
            get
            {
                if (_laps.Count == 0)
                    _laps.Add(new TelemetryLap(_currectCircuit, _lapType));

                return _laps.Last();
            }
        }

        public ISectorTiming CurrentLapSectorInformation
        {
            get { return CurrentLap; }
        }

        public ISectorTiming FastestLapSectorInformation
        {
            get { return FastestLap; }
        }

        public ISectorTiming PreviousLapSectorInformation
        {
            get
            {
                if (_laps.Count > 1)
                {
                    return _laps[_laps.Count - 2];
                }
                return null;
            }
        }

        public void SaveFastestLap()
        {
            _fastestLapRepository.Save(FastestLap);
        }

        public void LoadFastestLap()
        {
            MigrateXmlLapsToBinary();

            var fastestLap = _fastestLapRepository.Get(_currectCircuit, _lapType);
            FastestLap = fastestLap;
        }

        private void MigrateXmlLapsToBinary()
        {
            var xmlRepo = new XmlTelemetryLapRepository();
            var xmlFastestLap = xmlRepo.Get(Circuit, _lapType);
            if (xmlFastestLap == null) return;
       
            xmlFastestLap.LapType = _lapType;
            _fastestLapRepository.Save(xmlFastestLap);
            xmlRepo.Delete(xmlFastestLap);
        }

        public string GetSpeedDelta()
        {
            lock (syncLock)
            {
                if (ComparisonLap == null || CurrentLap == null || !CurrentLap.Packets.Any())
                    return "--";

                var comparisonLapPacket = ComparisonLap.GetPacketClosestTo(LatestPacket);

                var difference = LatestPacket.SpeedInKmPerHour - comparisonLapPacket.SpeedInKmPerHour;
                return string.Format("{0}{1:0.0}",
                                     difference > 0 ? "+" : "", difference);
            }
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
            lock (syncLock)
            {
                if (ComparisonLap == null || CurrentLap == null || !CurrentLap.Packets.Any())
                    return 0f;

                var currentPacket = LatestPacket;
                var comparisonLapPacket = ComparisonLap.GetPacketClosestTo(currentPacket);

                var difference = currentPacket.LapTime - comparisonLapPacket.LapTime;
                return -difference;
            }
        }

        public SectorTiming Sector1
        {
            get
            {
                if (FastestLap == null)                
                    return new SectorTiming(0,0);
                
                if (CurrentLap.CurrentSector > 1)
                    return new SectorTiming(CurrentLap.Sector1Time, FastestLap.Sector1Time);

                if (PreviousLapSectorInformation == null)
                    return new SectorTiming(0, FastestLap.Sector1Time);

                return new SectorTiming(PreviousLapSectorInformation.Sector1Time, FastestLap.Sector1Time);
            }
        }

        public SectorTiming Sector2
        {
            get 
            {
                if (FastestLap == null)
                    return new SectorTiming(0, 0);

                if (CurrentLap.CurrentSector > 2)   
                    return new SectorTiming(CurrentLap.Sector2Time, FastestLap.Sector2Time);

                if (PreviousLapSectorInformation == null)
                    return new SectorTiming(0, FastestLap.Sector2Time);       
         
                return new SectorTiming(PreviousLapSectorInformation.Sector2Time, FastestLap.Sector2Time);
            }
        }
        public SectorTiming Sector3
        {
            get 
            {
                if (FastestLap == null)
                    return new SectorTiming(0, 0);

                if (PreviousLapSectorInformation == null)
                    return new SectorTiming(0, FastestLap.Sector3Time);

                return new SectorTiming(PreviousLapSectorInformation.Sector3Time, FastestLap.Sector3Time);
            }
        }

        public string ComparisonLapTime
        {
            get
            {
                return ComparisonLap == null ? 0f.AsTimeString() : ComparisonLap.LapTime.AsTimeString();
            }
        }

        public string CurrentLapTime
        {
            get
            {
                return !CurrentLap.IsFirstPacketStartLine ? 0f.AsTimeString() : CurrentLap.LapTime.AsTimeString();
            }
        }

        public string AverageLapTime
        {
            get
            {
                return _laps.Any(lap => lap.HasLapFinished) ? _laps.Where(lap => lap.HasLapFinished).Average(lap => lap.LapTime).AsTimeString() : 0f.AsTimeString();
            }
        }

        public string LastLapTime
        {
            get { return _lastLapTime.AsTimeString(); }
        }

        public float LastLapFuel
        {
            get { return _lastLapFuel; }
        }

        public int LapsRemaining
        {
            get
            {
                int totalLaps = (int)LatestPacket.TotalLapsInRace;
                int completedLaps = (int)LatestPacket.CompletedLapsInRace;
                return totalLaps - completedLaps;
            }
        }

        public void ClearReferenceLap()
        {
            SetReferenceLap(null);
        }
    }
}
