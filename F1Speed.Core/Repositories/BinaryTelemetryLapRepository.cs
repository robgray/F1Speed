using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Deployment.Application;

namespace F1Speed.Core.Repositories
{
    public class BinaryTelemetryLapRepository : TelemetryLapRepository, ITelemetryLapRepository
    {        
        public override void Save(TelemetryLap lap)
        {
            Save(lap, GetFileName(lap.Circuit, lap.LapType));
        }

        public void Save(TelemetryLap lap, string fileName)
        {
            try
            {

                if (lap == null)
                    return;


                if (File.Exists(fileName))
                    File.Delete(fileName);

                using (Stream stream = File.Open(fileName, FileMode.Create))
                {
                    var binFormatter = new BinaryFormatter();
                    binFormatter.Serialize(stream, lap);
                    stream.Close();
                }
            } catch (Exception ex)
            {
                logger.Error("Count not save binary telemetry lap", ex);
                throw ex;
            }
        }

        public override TelemetryLap Get(Circuit circuit, string lapType)
        {
            return Get(GetFileName(circuit, lapType));
        }

        public TelemetryLap Get(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    return null;

                using (var stream = File.Open(fileName, FileMode.Open))
                {
                    try
                    {
                        var binFormatter = new BinaryFormatter();
                        var lap = (TelemetryLap) binFormatter.Deserialize(stream);
                        return lap;
                    }
                    catch (TargetInvocationException tex)
                    {
                        logger.Error("Could not retreive binary telemetry lap - new fields could not be retrieved", tex);                        
                    }
                    catch (SerializationException dex)
                    {
                        logger.Error("Could not retreive binary telemetry lap", dex);
                        throw dex;                        
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Could not retreive binary telemetry lap", ex);
                        throw ex;
                    }
                    finally
                    {
                        stream.Close();
                    }
                    return null;
                }
            } catch (Exception ex)
            {
                logger.Error("Could not retreive binary telemetry lap", ex);
                throw ex;
            }
        }

        public override string GetFileExtension()
        {
            return "f1s";
        }
    }
}
