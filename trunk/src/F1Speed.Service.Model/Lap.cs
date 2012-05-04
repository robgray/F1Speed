using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace F1Speed.Service.Model
{
    [DataContract]
    public class Lap
    {
        public Lap()
        {
            Samples = new List<LapSample>();
        }

        [DataMember]
        public virtual int LapId { get; set; }

        public virtual string Driver { get; set; }

        [DataMember]
        public virtual string CircuitName { get; set; }

        [DataMember]
        public virtual float LapTime { get; set; }

        [DataMember]
        public virtual IEnumerable<LapSample> Samples { get; set; }
    }
}
