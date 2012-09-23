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
        private TelemetryLapManager _manager;

        [SetUp]
        public void SetUp()
        {
            _manager = new TelemetryLapManager();
            foreach (var packet in TelemetryLapHelper.CreateOutLap().Packets) _manager.ProcessIncomingPacket(packet);
            foreach (var packet in TelemetryLapHelper.CreatePopulatedLap(lapNumber: 1f).Packets) _manager.ProcessIncomingPacket(packet);
        }        

        [Test]
        public void Start_of_first_full_lap_does_not_set_fastest_lap()
        {
            var manager = new TelemetryLapManager();
            
            foreach (var packet in TelemetryLapHelper.CreateOutLap().Packets) manager.ProcessIncomingPacket(packet);

            var fullLap = TelemetryLapHelper.CreatePopulatedLap(lapNumber: 1f);
            Assert.IsTrue(fullLap.IsFirstPacketStartLine);  // Lap we just added must be a full lap

            manager.ProcessIncomingPacket(fullLap.Packets[0]);

            Assert.IsNull(manager.FastestLap);
        }

        [Test]
        public void Starting_second_full_lap_sets_fastest_lap()
        {                        
            var lap = TelemetryLapHelper.CreatePopulatedLap(lapNumber: 2f);
            
            Assert.AreEqual(1f, _manager.LatestPacket.Lap);
            Assert.IsTrue(lap.IsFirstPacketStartLine);  // Lap we just added must be a full lap
            Assert.IsNull(_manager.FastestLap);

            // Incoming packet is a new lap
            _manager.ProcessIncomingPacket(lap.Packets[0]);

            Assert.IsNotNull(_manager.FastestLap);
            Assert.AreEqual(1, _manager.FastestLap.LapNumber);
            Assert.AreEqual(2f, _manager.LatestPacket.Lap);
        }

        [Test]
        public void GetSpeedDelta_returns_blank_when_no_fastest_lap()
        {
            Assert.IsNull(_manager.FastestLap);
            Assert.AreEqual("--", _manager.GetSpeedDelta());
        }

        [Test]
        public void GetSpeedDelta_returns_negative_delta_when_fastest_lap_exists_and_new_packet_is_slower()
        {
            _manager.ProcessIncomingPacket(new TelemetryPacket { Lap = 2f, LapDistance = 0.388f, Speed = 80.123f, LapTime = 0.01604f });
            
            Assert.IsNotNull(_manager.FastestLap);
            var delta = _manager.GetSpeedDelta();
            Assert.AreEqual("-1.7", delta);
        }

        [Test]
        public void GetSpeedDelta_returns_positive_delta_when_fastest_lap_exists_and_new_packet_is_faster()
        {
            _manager.ProcessIncomingPacket(new TelemetryPacket { Lap = 2f, LapDistance = 0.388f, Speed = 80.723f, LapTime = 0.01604f });

            Assert.IsNotNull(_manager.FastestLap);
            var delta = _manager.GetSpeedDelta();
            Assert.AreEqual("+0.5", delta);
        }

        [Test]
        public void Returning_to_pits_does_not_set_previous_lap_as_fastest()
        {
            // set outlap
            var manager = new TelemetryLapManager();
            foreach (var packet in TelemetryLapHelper.CreateOutLap().Packets) manager.ProcessIncomingPacket(packet);

            // Add nearly all of a lap
            var firstLap = TelemetryLapHelper.CreatePopulatedLap(lapNumber: 1f, completeLap: true);
            for (var i = 0; i < firstLap.Packets.Count - 2; i++) manager.ProcessIncomingPacket(firstLap.Packets[i]);

            Assert.IsNull(manager.FastestLap);

            var garagePacket = new TelemetryPacket {Lap = 0f, Speed = 0f, LapDistance = -0.001f};     
            Assert.IsTrue(garagePacket.IsSittingInPits);      
            manager.ProcessIncomingPacket(garagePacket);
            
            Assert.IsNull(manager.FastestLap);
        }
    }
}
