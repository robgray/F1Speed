using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace F1Speed.Service.Model
{
    [ServiceContract(Name = "F1SpeedService",
    Namespace = "http://f1speed.atomicf1.com/f1speedservice/")]
    public interface IF1SpeedService
    {
        [OperationContract(IsOneWay = true)]
        void LogLap(string userKey, Lap lap);
    }
}
