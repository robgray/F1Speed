using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Core
{
    public class SectorChangedEventArgs
    {
        public int Sector { get; set;  }
        public float Time { get; set; }
        public float Delta { get; set; }
        public float Fastest { get; set; }
    }
}
