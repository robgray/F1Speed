using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core;

namespace F1Speed.Models
{
    public class SectorTimeViewModel
    {
        public SectorTimeViewModel(SectorTiming sectorTiming)
        {
            FastestTime = sectorTiming.FastestTime.AsGapString(hideIfZero: false, excludeSign: true);
            CurrentTime = sectorTiming.CurrentTime.AsGapString(hideIfZero: false, excludeSign: true);
            DeltaTime = sectorTiming.Delta;
            IsFaster = sectorTiming.DeltaTime >= 0;
        }

        public string FastestTime { get; protected set; }
        public string DeltaTime { get; protected set; }
        public string CurrentTime { get; protected set; }
        public bool IsFaster { get; protected set; }
    }
}
