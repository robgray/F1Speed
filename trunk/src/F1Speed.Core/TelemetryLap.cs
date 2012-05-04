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
                hasFinished = info.GetValue<bool>("HasFinished");
            }
            catch
            {
                hasFinished = true;
            }
        }

        private List<TelemetryPacket> packets;

        [XmlArray("Packets"), XmlArrayItem("Packet", typeof (TelemetryPacket))] 
        public List<TelemetryPacket> Packets 
        {
            get { return packets; }
            set { packets = value; }
        }

        private string circuitName;
        public string CircuitName 
        {
            get { return circuitName; }
            set { circuitName = value; }
        }

        private string lapType;
        public string LapType 
        {
            get { return lapType;  }
            set { lapType = value; }
        }
        
        public void AddPacket(TelemetryPacket packet)
        {
            Packets.Add(packet);
            while (packets.Any() && packets.First().Distance < 0)
                packets.Remove(packets.First());
        }

        public int LapNumber
        {
            get
            {
                if (Packets.Count() == 0)
                    return 0;

                return (int)Packets.Last().Lap;
            }
        }

        public float LapTime
        {
            get
            {
                return Packets.Count() == 0 ? 0f : Packets.Last().LapTime;
            }
        }

        public bool IsFirstPacketStartLine 
        {
            get
            {
                const float cutoff = (1000 / 60000f) + 0.001f;

                if (!Packets.Any())
                    return false;
                return Packets.First().LapTime < cutoff;
            }
        }

        private bool hasFinished;
        public bool HasLapFinished
        {
            get { return hasFinished;  }            
        }
        
        public TelemetryPacket GetPacketClosestTo(TelemetryPacket packet)
        {
            if (Packets.Count() == 0)
                return packet;

            var closestPackets = Packets.OrderBy(p => Math.Abs(p.LapDistance - packet.LapDistance)).Take(10);

            return closestPackets.First();
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CircuitName", circuitName);
            info.AddValue("LapType", lapType);
            info.AddValue("Packets", packets);
            info.AddValue("HasFinished", hasFinished);            
        }

        public bool IsOutLap
        {
            get { return Packets.All(c => c.LapDistance < 0); }          
        }

        public bool IsCompleteLap
        {
            get { return IsFirstPacketStartLine && HasLapFinished; }
        }

        public void MarkLapCompleted()
        {
            hasFinished = true;
        }
    }
}
