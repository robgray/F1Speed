using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace F1Speed.Core.Helpers
{
    public static class Extensions
    {
        public static bool EntryExists(this SerializationInfo info, string fieldName)
        {
            SerializationInfoEnumerator e = info.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Name.ToUpper() == fieldName.ToUpper())
                    return true;
            }
            return false;
        }
    }
}
