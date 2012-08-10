using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core;
using F1Speed.Core.Repositories;
using NUnit.Framework;

namespace F1Speed.Tests
{
    [TestFixture]
    public class XmlTelemetryLapSerializerFixture
    {
        [Test]
        public void Can_serialize_lap()
        {
            var lap = TelemetryLapHelper.CreatePopulatedLap(1);

            var serializer = new XmlTelemetryLapRepository();
            serializer.Save(lap);
        }

        [Test]
        public void Can_deserialize_lap()
        {
            var serializer = new XmlTelemetryLapRepository();
            var lap = serializer.Get("test", "testtype");            
        }
    }
}


