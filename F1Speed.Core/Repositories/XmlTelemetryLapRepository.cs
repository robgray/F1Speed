using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace F1Speed.Core.Repositories
{
    public class XmlTelemetryLapRepository : TelemetryLapRepository, ITelemetryLapRepository
    {
        public XmlTelemetryLapRepository() { }
        
        public override void Save(TelemetryLap lap)
        {
            try
            {
                if (lap == null)
                    return;

                var filename = GetFileName(lap);
                if (File.Exists(filename))
                    File.Delete(filename);

                var xmlSer = new XmlSerializer(typeof (TelemetryLap));
                using (var memStm = new MemoryStream())
                {

                    xmlSer.Serialize(memStm, lap);

                    using (var stmR = new StreamReader(memStm))
                    {
                        memStm.Position = 0;
                        File.AppendAllText(filename, stmR.ReadToEnd(), Encoding.UTF8);
                    }
                }
            } catch (Exception ex)
            {
                logger.Error("Could not save xml telemetry lap", ex);
                throw ex;
            }
        }

        public override TelemetryLap Get(string circuitName, string lapType)
        {
            try
            {
                var filename = GetFileName(circuitName, lapType);
                if (!File.Exists(filename))
                    return null;

                string fastestLapData = File.ReadAllText(filename);

                using (var reader = new StringReader(fastestLapData))
                {
                    var serializer = new XmlSerializer(typeof (TelemetryLap));
                    using (var xmlReader = new XmlTextReader(reader))
                    {
                        try
                        {
                            var fastestLap = (TelemetryLap) serializer.Deserialize(xmlReader);                            
                            return fastestLap;
                        }
                        catch
                        {
                        }
                        return null;
                    }
                }
            } catch (Exception ex)
            {
                logger.Error("Could not retreive csv telemetry lap", ex);
                throw ex;
            }
        }

        public override string GetFileExtension()
        {
            return "xml";
        }        
    }
}
