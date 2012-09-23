using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Core;

namespace F1Speed.Tests
{
    public class TelemetryPacketGenerator
    {
        private TelemetryPacket _current;
        private readonly Random _randomGenerator;
        private readonly int _lowerRange;
        private readonly int _upperRange;        
        private int _packetNumber;

        public TelemetryPacketGenerator(int lowerRange, int upperRange)
        {            
            _packetNumber = 0;

            _lowerRange = lowerRange;
            _upperRange = upperRange;
            _randomGenerator = new Random();
            _current = new TelemetryPacket
                           {
                               Time = ElapsedTime                               
                           };
        }

        private float Modifier
        {
            get
            {
                var sign = _randomGenerator.NextDouble() > 0.5 ? 1 : -1;
                return _randomGenerator.Next(_lowerRange, _upperRange)*sign/100f;
            }
        }

        private float ElapsedTime
        {
            get
            {
                const float frequency = 1/60;

                return ++_packetNumber*frequency;
            }

        }


    }
}
