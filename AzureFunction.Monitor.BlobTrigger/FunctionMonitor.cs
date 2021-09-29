using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SPDB.AzureFunction.MallMonitor.BlobTrigger
{
    public static class FunctionMonitor
    {
        [FunctionName("FunctionMonitor")]
        public static void Run([BlobTrigger("metrics/{name}", Connection = "MonitorBlobConnection")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            StreamReader reader = new StreamReader(myBlob);
            string content = reader.ReadToEnd();
            reader.Close();
            if (content.Length > 10)
            {
                var tableName = "";
                if (name.Contains("/proxy/"))
                {
                    tableName = Environment.GetEnvironmentVariable("LogAnalyticsTableNameTableProxy");
                }
                else if (name.Contains("/mysql/"))
                {
                    tableName = Environment.GetEnvironmentVariable("LogAnalyticsTableNameTableMySqlCluster");
                }
                else if (name.Contains("/aksengine/"))
                {
                    tableName = Environment.GetEnvironmentVariable("LogAnalyticsTableNameTableAksEngine");
                }

                var postResult = AzureLogAnalyticsAPI.PostData(tableName, content);
                log.LogInformation($"Complete. Log Analytics API Post Result:{postResult}.");
            }
            else
            {
                log.LogInformation($"Skip Blob. Invalid data. Content:{content}.");
            }
        }
    }
}
