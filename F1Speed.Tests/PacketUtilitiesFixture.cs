using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using F1Speed.Core;
using NUnit.Framework;

namespace F1Speed.Tests
{
    [TestFixture]
    public class PacketUtilitiesFixture
    {
        private TelemetryPacketGenerator generator;

        [SetUp]
        public void SetUp()
        {
            generator = new TelemetryPacketGenerator(2, 30);
        }

        [Test]
        public void Can_convert_to_and_from_packet()
        {
            var telemetryPacket = generator.GetPacket();

            var bytePacket = PacketUtilities.ConvertPacketToByteArray(telemetryPacket);
            var returnPacket = PacketUtilities.ConvertToPacket(bytePacket);
            
            foreach (var property in returnPacket.GetType().GetFields())
            {
                var one = property.GetValue(telemetryPacket);
                var two = property.GetValue(returnPacket);

                Assert.AreEqual(two, one, property.Name);
            }
        }


    }
}
