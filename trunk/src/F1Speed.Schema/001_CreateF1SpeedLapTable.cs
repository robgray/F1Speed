using Migrator.Framework;
using System.Data;

namespace F1Speed.Schema
{
    [Migration(1)]
    public class _001_CreateTelemetryLapTable : Migration 
    {
        public override void Down()
        {
            Database.RemoveTable("F1SpeedLap");
        }

        public override void Up()
        {
            Database.AddTable("F1SpeedLap",
                              new Column("LapId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                              new Column("Driver", DbType.String, 100, ColumnProperty.NotNull),
                              new Column("CircuitName", DbType.String, 100, ColumnProperty.NotNull),
                              new Column("LapTime", DbType.Double, ColumnProperty.NotNull)
                );
        }
    }
}
