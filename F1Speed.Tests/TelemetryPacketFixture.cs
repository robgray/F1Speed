using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core;
using NUnit.Framework;

namespace F1Speed.Tests
{
    [TestFixture]
    public class TelemetryPacketFixture
    {
        [Test]
        public void SpeedInKmPerHour_is_correct()
        {
            var packet = new TelemetryPacket {Speed = 100f };
            
            Assert.AreEqual(360, packet.SpeedInKmPerHour);
        }
    }
}
