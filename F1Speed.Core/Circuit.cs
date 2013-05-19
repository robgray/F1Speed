using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Core
{
    public class Circuit
    {
        public static Circuit NullCircuit = new Circuit() {Name = "", TrackLength = 0f, Order = 0, Filename = ""};

        public string Name { get; set; }
        public float TrackLength { get; set; }

        public int Order { get; set; }

        public string Filename { get; set; }        
    }
}
