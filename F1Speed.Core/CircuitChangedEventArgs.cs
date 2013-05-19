using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F1Speed.Core
{
    public class CircuitChangedEventArgs 
    {
        public string CircuitName { get; set; }
        public string LapType { get; set; }
    }
}
