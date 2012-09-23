using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace F1Speed.Core
{
    [Serializable]
    public class TelemetryLap : ISerializable
    {
        public TelemetryLap()
        {
            
        }

        public TelemetryLap(string circuitName, string lapType)
        {
            Packets = new List<TelemetryPacket>();
            CircuitName = circuitName;
            LapType = lapType;
        }

        public TelemetryLap(SerializationInfo info, StreamingContext context) 
        {
            Packets = info.GetValue<List<TelemetryPacket>>("Packets");
            CircuitName = info.GetValue<string>("CircuitName");
            LapType = info.GetValue<string>("LapType");
            try
            {
                _hasFinished = info.GetValue<bool>("HasFinished");
            }
            catch
            {
                _hasFinished = true;
            }
        }

        private List<TelemetryPacket> _packets;

        [XmlArray("Packets"), XmlArrayItem("Packet", typeof (TelemetryPacket))] 
        public List<TelemetryPacket> Packets 
        {
            get { return _packets; }
            set { _packets = value; }
        }

        private string _circuitName;
        public string CircuitName 
        {
            get { return _circuitName; }
            set { _circuitName = value; }
        }

        private string _lapType;
        public string LapType 
        {
            get { return _lapType;  }
            set { _lapType = value; }
        }
        
        public void AddPacket(TelemetryPacket packet)
        {
            Packets.Add(packet);
            //while (_packets.Any() && _packets.First().Distance < 0)
            //    _packets.Remove(_packets.First());
        }

        public int LapNumber
        {
            get
            {
                if (!Packets.Any())
                    return 0;

                return (int)Packets.Last().Lap;
            }
        }

        public float LapTime
        {
            get
            {
                return !Packets.Any() ? 0f : Packets.Last().LapTime;
            }
        }

        public bool IsFirstPacketStartLine 
        {
            get
            {
                const float cutoff = (1000 / 60000f) + 0.001f;

                if (!Packets.Any())
                    return false;
                var first = Packets.First();
                return first.LapTime > 0f && first.LapTime < cutoff;                
            }
        }

        private bool _hasFinished;
        public bool HasLapFinished
        {
            get { return _hasFinished;  }            
        }
        
        public TelemetryPacket GetPacketClosestTo(TelemetryPacket packet)
        {
            if (!Packets.Any())
                return packet;

            var closestPackets = Packets.OrderBy(p => Math.Abs(p.LapDistance - packet.LapDistance)).Take(10);

            return closestPackets.First();
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CircuitName", _circuitName);
            info.AddValue("LapType", _lapType);
            info.AddValue("Packets", _packets);
            info.AddValue("HasFinished", _hasFinished);            
        }

        public bool IsOutLap
        {
            // An outlap can either be less than 0 or less than 1
            get { return Packets.All(c => c.Distance < 1 && Math.Abs(c.LapTime - 0) < Constants.Epsilon); }          
        }

        public bool IsCompleteLap
        {
            get { return IsFirstPacketStartLine && HasLapFinished; } 
        }

        public void MarkLapCompleted()
        {
            _hasFinished = true;
        }

        public float Distance 
        { 
            get
            {
                if (!Packets.Any())
                    return 0f;
                return Packets.Last().Distance;
            }
        }
    }
}
