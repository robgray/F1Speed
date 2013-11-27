using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace F1Speed.Core
{
    public static class Extensions
    {        
        public static string AsTimeString(this float timeInSeconds, bool hideIfZero = false)
        {
            if (timeInSeconds <= 0)
            {
                if (hideIfZero)
                    return "";
                return "--:--.---";
            }

            var ts = TimeSpan.FromSeconds((double)timeInSeconds);
            return ts.ToString(@"m\:ss\.fff");
        }

        public static string AsGapString(this float gapInSeconds, bool hideIfZero = false, bool excludeSign = false)
        {
            if (Math.Abs(gapInSeconds - 0f) < 0.0001)
            {
                if (hideIfZero)
                    return "";
                return "-.---";
            }

            var gs = string.Format("{0:f3}", Math.Abs(gapInSeconds));
            if (excludeSign)
                return gs;
            return (gapInSeconds < 0 ? "-" : "+") + gs;
        }

        public static T GetValue<T>(this SerializationInfo info, string fieldName)
        {
            return (T) info.GetValue(fieldName, typeof (T));
        }    
    }
}
