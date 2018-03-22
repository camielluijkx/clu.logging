using clu.logging.library.net350.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System;
using System.Net;

namespace clu.logging.library.net350
{
    public static class JsonClient
    {
        public static object Get(string url)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolTypeExtensions.Tls12;

                var client = new WebClient { Headers = { [HttpRequestHeader.ContentType] = "application/json" } };

                var response = client.DownloadString(new Uri(url));

                //return JsonConvert.DeserializeObject(response);
                return response;
            }
            catch (Exception ex) // [TODO] improve console logging
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occurred: {ex.ToExceptionMessage()}");
                Console.BackgroundColor = ConsoleColor.Black;

                throw ex;
            }
        }

        public static object Post(object data, string url)
        {
            try
            {
                var client = new WebClient { Headers = { [HttpRequestHeader.ContentType] = "application/json" } };

                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var serialisedData = JsonConvert.SerializeObject(data, serializerSettings);

                var response = client.UploadString(new Uri(url), serialisedData);

                return JsonConvert.DeserializeObject(response);
            }
            catch (Exception ex) // [TODO] improve console logging
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occurred: {ex.ToExceptionMessage()}");
                Console.BackgroundColor = ConsoleColor.Black;

                throw ex;
            }
        }
    }
}
