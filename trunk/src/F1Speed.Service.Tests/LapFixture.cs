using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using F1Speed.Service.Model;
using NUnit.Framework;

namespace F1Speed.Service.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class LapFixture : InMemoryData
    {   
        [Test]
        public void Can_retrieve_lap_from_database()
        {
            var lap = Session.Get<Lap>(1);
            Assert.IsNotNull(lap);
            Assert.AreEqual(1, lap.LapId);
        }        

        [Test]
        public void Can_save_Lap_to_Database()
        {
            var samples = new List<LapSample>()
                              {
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f},
                                  new LapSample() { Lap = 1, LapDistance = 0.0023f, Brake = 0f, Throttle  = 0.998f, X = 122, Y = -432, LapTime = 0.002f, Time = 3.022f}
                              };

            var lap = new Lap { CircuitName = "Australia", Driver = "Robert Gray", Samples = samples, LapTime = samples.Last().LapTime };

            Session.Save(lap);
            Session.Flush();
            Session.Clear();

            var fromDb = Session.Get<Lap>(lap.LapId);
            Assert.AreNotSame(lap, fromDb);
            Assert.AreEqual(lap.CircuitName, fromDb.CircuitName);
            Assert.AreEqual(lap.Driver, fromDb.Driver);
            Assert.AreEqual(lap.LapTime, fromDb.LapTime);
        }
    }
}
