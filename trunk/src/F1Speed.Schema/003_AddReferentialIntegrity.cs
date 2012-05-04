using Migrator.Framework;

namespace F1Speed.Schema
{
    [Migration(3)]
    public class _003_AddReferentialIntegrity : Migration
    {
        public override void Down()
        {
            Database.RemoveForeignKey("LapSampe", "FK_LapSample_F1SpeedLapId");
        }

        public override void Up()
        {
            Database.AddForeignKey("FK_LapSample_F1SpeedLapId", "LapSample", "F1SpeedLapId", "F1SpeedLap", "LapId");
        }
    }
}
