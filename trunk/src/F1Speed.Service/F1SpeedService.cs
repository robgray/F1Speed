using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using F1Speed.Service.Model;
using F1Speed.Service.Repositories;

namespace F1Speed.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class F1SpeedService : IF1SpeedService
    {
        private ILapRepository _lapRepository;

        public F1SpeedService()
        {
            _lapRepository = new LapRepository();
        }

        public void LogLap(string userKey, Lap lap)
        {
            throw new NotImplementedException();
        }
    }
}
