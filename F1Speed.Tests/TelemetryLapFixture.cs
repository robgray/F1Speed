using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core;
using NUnit.Framework;

namespace F1Speed.Tests
{
    [TestFixture]
    public class TelemetryLapFixture
    {
        [Test]
        public void AddPacket_adds_packets()
        {
            var tLap = new TelemetryLap(Circuit.NullCircuit, "LapType");
            var origCount = tLap.Packets.Count();

            tLap.AddPacket(new TelemetryPacket());
            var newCount = tLap.Packets.Count();
            
            Assert.AreEqual(origCount + 1, newCount);
        }

        [Test]
        public void HasStartLinePacket_is_true_when_first_packet_time_is_less_than_one_sixtieth_of_second()
        {
            const float cutoff = (1000 / 60000f) + 0.001f;

            // Samples are taken 60 times per second, so first sample must be < 0.018 into the lap.
            // time differs slightly because last sample of last lap might have been nearly 0.017 before end of that lap.
            var tPacket = new TelemetryPacket() {LapTime = cutoff * 0.95f };
            var tLap = new TelemetryLap(Circuit.NullCircuit, "LapType");
            tLap.AddPacket(tPacket);

            Assert.IsTrue(tLap.IsFirstPacketStartLine);
        }

        [Test]
        public void HasStartLinePacket_is_false_when_first_packet_time_is_greater_than_one_sixtieth_of_second()
        {
            const float cutoff = (1000 / 60000f) + 0.001f;

            // Samples are taken every 0.017 seconds, so first sample must be < 0.018 into the lap.
            // time differs slightly because last sample of last lap might have been nearly 0.017 before end of that lap.
            var tPacket = new TelemetryPacket() { LapTime = cutoff * 1.1f };
            var tLap = new TelemetryLap(Circuit.NullCircuit, "LapType");
            tLap.AddPacket(tPacket);

            Assert.IsFalse(tLap.IsFirstPacketStartLine);
        }

        [Test]
        public void GetPacketClosestTo_returns_packet_closest_to_supplied_packet()
        {
            var tLap = new TelemetryLap(Circuit.NullCircuit, "LapType");
            tLap.AddPacket(new TelemetryPacket{ LapDistance = 0.529f});
            tLap.AddPacket(new TelemetryPacket { LapDistance = 1.875f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 3.221f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 4.567f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 5.931f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 7.262f });  // should be closest to this
            tLap.AddPacket(new TelemetryPacket { LapDistance = 8.611f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 9.961f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 11.311f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 12.661f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 14.013f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 15.365f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 16.715f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 18.067f });
            tLap.AddPacket(new TelemetryPacket { LapDistance = 19.419f });

            var tPacketCompare = new TelemetryPacket { LapDistance = 7.811f };

            var foundPacket = tLap.GetPacketClosestTo(tPacketCompare);

            Assert.AreEqual(7.262f,foundPacket.LapDistance);
        }
    }
}
