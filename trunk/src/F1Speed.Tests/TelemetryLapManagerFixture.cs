using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core;
using NUnit.Framework;

namespace F1Speed.Tests
{
    [TestFixture]
    public class TelemetryLapManagerFixture
    {
        private TelemetryLapManager manager;

        [SetUp]
        public void SetUp()
        {
            manager = new TelemetryLapManager();
            var lap = TelemetryLapHelper.CreatePopulatedLap(1f);
            foreach (var packet in lap.Packets)
            {
                manager.AddPacket(packet);
            }            
        }

        [Test]
        public void Pit_exit_lap_is_dropped()
        {
            
        }

        [Test]
        public void AddPacket_sets_fastest_lap_when_starting_second_full_lap()
        {
            var manager = new TelemetryLapManager();
            var lap = TelemetryLapHelper.CreatePopulatedLap(1f);
            foreach(var packet in lap.Packets)
            {
                manager.AddPacket(packet);
            }

            Assert.IsTrue(lap.IsFirstPacketStartLine);  // Lap we just added must be a full lap
            Assert.IsNull(manager.FastestLap);

            manager.AddPacket(new TelemetryPacket { Lap = 2f, LapDistance = 0.342f, LapTime = 0.01605f });

            Assert.IsNotNull(manager.FastestLap);               
        }

        [Test]
        public void AddPacket_does_not_set_previous_lap_as_fastest_if_not_full_lap()
        {
            var manager = new TelemetryLapManager();
            var lap = TelemetryLapHelper.CreatePopulatedLap(0f, false);
            foreach (var packet in lap.Packets)
            {
                manager.AddPacket(packet);
            }
        }

        [Test]
        public void GetSpeedDelta_returns_blank_when_no_fastest_lap()
        {
            Assert.IsNull(manager.FastestLap);
            Assert.AreEqual("--", manager.GetSpeedDelta());
        }

        [Test]
        public void GetSpeedDelta_returns_negative_delta_when_fastest_lap_exists()
        {
            manager.AddPacket(new TelemetryPacket { Lap = 2f, LapDistance = 0.388f, Speed = 80.123f, LapTime = 0.01604f });
            
            Assert.IsNotNull(manager.FastestLap);
            var delta = manager.GetSpeedDelta();
            Assert.AreEqual("-1.7", delta);
        }

        [Test]
        public void GetSpeedDelta_returns_positive_delta_when_fastest_lap_exists()
        {
            manager.AddPacket(new TelemetryPacket { Lap = 2f, LapDistance = 0.388f, Speed = 80.723f, LapTime = 0.01604f });

            Assert.IsNotNull(manager.FastestLap);
            var delta = manager.GetSpeedDelta();
            Assert.AreEqual("+0.5", delta);
        }        
    }
}
