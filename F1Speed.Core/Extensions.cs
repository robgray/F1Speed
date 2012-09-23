using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace F1Speed.Core
{
    public static class Extensions
    {
        public static string AsTimeString(this float timeInSeconds)
        {
            if (timeInSeconds <= 0)
                return "0:00.0000";

            var ts = TimeSpan.FromSeconds((double) timeInSeconds);
            return ts.ToString(@"m\:ss\.fff");
        }

        public static T GetValue<T>(this SerializationInfo info, string fieldName)
        {
            return (T) info.GetValue(fieldName, typeof (T));
        }    
    }
}
