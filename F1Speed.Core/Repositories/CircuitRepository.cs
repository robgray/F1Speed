using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Core.Repositories
{
    public static class CircuitRepository
    {
        private static IList<Circuit> _circuits;

        static CircuitRepository()
        {
            _circuits = new List<Circuit>()
                {
                    new Circuit() {Name = "Unknown", TrackLength = 0f, Order = 0, Filename = "Unknown"},
                    new Circuit() {Name = "Australia", TrackLength = 5301.984f, Order = 1, Filename = "Australia"},
                    new Circuit() {Name = "Malaysia", TrackLength = 5549.814f, Order = 2, Filename  = "Malaysia"},
                    new Circuit() {Name = "Bahrain", TrackLength = 5409.189f, Order = 3, Filename = "Bahrain"},
                    new Circuit() {Name = "China", TrackLength = 5444.122f, Order = 4, Filename = "China"},
                    new Circuit() {Name = "Spain", TrackLength = 4651.133f, Order = 5, Filename = "Spain-Catalunya"},
                    new Circuit() {Name = "Monaco", TrackLength = 3322.418f, Order = 6, Filename = "Monaco"},
                    new Circuit() {Name = "Canada", TrackLength = 4370.895f, Order = 7, Filename = "Canada"},
                    new Circuit() {Name = "Austria", TrackLength = 4320.581f, Order = 8, Filename = "Austria"},
                    new Circuit() {Name = "Britian", TrackLength = 5896.262f, Order = 9, Filename = "Britian-Silverstone"},                    
                    new Circuit() {Name = "Germany", TrackLength = 5148.048f, Order = 10, Filename = "Germany-Nurburgring"},
                    new Circuit() {Name = "Hungary", TrackLength = 4378.222f, Order = 11, Filename = "Hungary" },
                    new Circuit() {Name = "Belgium", TrackLength = 7003.279f, Order = 12, Filename = "Belgium" },
                    new Circuit() {Name = "Italy", TrackLength = 5797.636f, Order = 13, Filename = "Italy" },
                    new Circuit() {Name = "Singapore", TrackLength = 5064.113f, Order = 14, Filename = "Singapore" },
                    new Circuit() {Name = "Japan", TrackLength = 5817.484f, Order = 15, Filename = "Japan" },
                    new Circuit() {Name = "Russia", TrackLength = 5822.213f, Order = 16, Filename = "Russia" },
                    new Circuit() {Name = "United States", TrackLength = 5515.625f, Order = 17, Filename = "COTA"},
                    new Circuit() {Name = "Brazil", TrackLength = 4293.856f, Order = 18, Filename = "Brazil" },
                    new Circuit() {Name = "Abu Dahbi", TrackLength = 5542.2f, Order = 19, Filename = "AbuDahbi"}
                };
        }

        public static Circuit GetByTrackLength(float trackLength)
        {
            Circuit matchedCircuit;
            try
            {
                matchedCircuit = _circuits.OrderBy(x => Math.Abs(x.TrackLength - trackLength)).First();
                if (Math.Abs(matchedCircuit.TrackLength - trackLength) > 1.0f)
                {
                    matchedCircuit = new Circuit() { Name = trackLength.ToString("#m"), TrackLength = trackLength, Order = _circuits.Count, Filename = trackLength.ToString("Unknown (#m)") };
                    _circuits.Add(matchedCircuit);
                }
            }
            catch (InvalidOperationException)
            {
                matchedCircuit = _circuits.ElementAt(0);
            }
            return matchedCircuit;
        }        

        public static List<Circuit> GetAll()
        {
            return _circuits.OrderBy(x => x.Order).ToList();
        }
    }
}
