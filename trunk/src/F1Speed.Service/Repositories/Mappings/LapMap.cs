using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F1Speed.Service.Model;
using FluentNHibernate.Mapping;

namespace F1Speed.Service.Repositories.Mappings
{
    public class LapMap : ClassMap<Lap>
    {
        public LapMap()
        {
            Table("F1SpeedLap");
            Id(x => x.LapId, "LapId");
            Map(x => x.Driver);
            Map(x => x.CircuitName);
            Map(x => x.LapTime);
            HasMany(x => x.Samples).KeyColumn("F1SpeedLapId")
                .Cascade.AllDeleteOrphan();
        }
    }
}
