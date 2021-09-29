using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SPDB.AzureFunction.MallMonitor.BlobTrigger
{
    public class AzureLogAnalyticsAPI
    {
        // Send a request to the POST API endpoint
        public static int PostData(string tableName, string dataJson)
        {
            // Log Analytics workspace ID
            string customerId = Environment.GetEnvironmentVariable("LogAnalyticsWorkspaceId");
            // For sharedKey, use either the primary or the secondary Connected Sources client authentication key   
            string sharedKey = Environment.GetEnvironmentVariable("LogAnalyticsKey");

            // Create a hash for the API signature 
            var dateString = DateTime.UtcNow.ToString("r");
            var jsonBytes = Encoding.UTF8.GetBytes(dataJson);
            string stringToHash = "POST\n" + jsonBytes.Length + "\napplication/json\n" + "x-ms-date:" + dateString + "\n/api/logs";
            string hashedString = BuildSignature(stringToHash, sharedKey);
            string signature = "SharedKey " + customerId + ":" + hashedString;

            string LogAnalyticsAPIUrl = "https://" + customerId + ".ods.opinsights.azure.cn/api/logs?api-version=2016-04-01";

            var result = LogAnalyticsAPIUrl
                          .WithHeader("Accept", "application/json")
                          .WithHeader("Log-Type", tableName)
                          .WithHeader("x-ms-date", dateString)
                          .WithHeader("time-generated-field", "")
                          .WithHeader("Content-Type", "application/json")
                          .WithHeader("Authorization", signature)
                          .SendStringAsync(HttpMethod.Post, dataJson)
                          .Result
                          .StatusCode;
            return result;
        }

        // Build the API signature
        private static string BuildSignature(string message, string secret)
        {
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = Convert.FromBase64String(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hash = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
