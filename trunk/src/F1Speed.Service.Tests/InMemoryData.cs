using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Service.Model;

namespace F1Speed.Service.Tests
{
    public abstract class InMemoryData : InMemoryDatabase
    {
        protected InMemoryData()
        {
            var lap = new Lap { LapId = 1, CircuitName = "Australia", Driver = "Rob Gray", LapTime = 82.343f };

            Session.Save(lap);

            Session.Flush();
        }
    }
}
