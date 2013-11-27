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
                    Circuit.NullCircuit,
                    new Circuit() {Name = "Australia", TrackLength = 5301.984f, Order = 1, Filename = "Australia"},
                    new Circuit() {Name = "Malaysia", TrackLength = 5549.814f, Order = 2, Filename  = "Malaysia"},
                    new Circuit() {Name = "China", TrackLength = 5444.122f, Order = 3, Filename = "China"},
                    new Circuit() {Name = "Bahrain", TrackLength = 5409.189f, Order = 4, Filename = "Bahrain"},
                    new Circuit() {Name = "Spain", TrackLength = 4651.133f, Order = 5, Filename = "Spain-Catalunya"},
                    new Circuit() {Name = "Monaco", TrackLength = 3322.418f, Order = 6, Filename = "Monaco"},
                    new Circuit() {Name = "Canada", TrackLength = 4370.895f, Order = 7, Filename = "Canada"},
                    new Circuit() {Name = "Britian", TrackLength = 5896.262f, Order = 8, Filename = "Britian-Silverstone"},                    
                    new Circuit() {Name = "Germany", TrackLength = 5148.048f, Order = 9, Filename = "Germany-Nurburgring"},
                    new Circuit() {Name = "Hungary", TrackLength = 4378.222f, Order = 10, Filename = "Hungary" },
                    new Circuit() {Name = "Belgium", TrackLength = 7003.279f, Order = 11, Filename = "Belgium" },
                    new Circuit() {Name = "Italy", TrackLength = 5797.636f, Order = 12, Filename = "Italy" },
                    new Circuit() {Name = "Singapore", TrackLength = 5064.113f, Order = 13, Filename = "Singapore" },
                    new Circuit() {Name = "Korea", TrackLength = 5586.651f, Order = 14, Filename ="Korea" },
                    new Circuit() {Name = "Japan", TrackLength = 5817.484f, Order = 15, Filename = "Japan" },                    
                    new Circuit() {Name = "India", TrackLength = 5142.777f, Order = 16, Filename = "India" },
                    new Circuit() {Name = "Abu Dahbi", TrackLength = 5542.2f, Order = 17, Filename = "AbuDahbi"},
                    new Circuit() {Name = "United States", TrackLength = 5515.625f, Order = 18, Filename = "COTA"},
                    new Circuit() {Name = "Brazil", TrackLength = 4293.856f, Order = 19, Filename = "Brazil" }
                };
        }

        public static Circuit GetByTrackLength(float trackLength)
        {
            return _circuits.Single(x => Math.Abs(x.TrackLength - trackLength) < 0.002f);
        }        

        public static List<Circuit> GetAll()
        {
            return _circuits.OrderBy(x => x.Order).ToList();
        }
    }
}
