using System;

namespace F1Speed.Core
{
    public class SectorTiming
    {
        public SectorTiming(float currentTime, float fastestTime)
        {
            CurrentTime = currentTime;
            FastestTime = fastestTime;
        }

        public float CurrentTime { get; protected set; }
        public float FastestTime { get; protected set; }
        public string Delta
        {
            get
            {                
                if (Math.Abs(DeltaTime - 0) < Constants.Epsilon)
                    return "0";
                
                return Math.Abs(DeltaTime).AsGapString(); 
            }
        }

        public float DeltaTime
        {
            get
            {
                return ((FastestTime - CurrentTime) * 100) / 100;
            }
        }
    }
}
