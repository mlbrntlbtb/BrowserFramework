using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OSH_Client
{
    public static class OSH
    {
        static readonly HttpClient client = new HttpClient();
        public delegate List<ControlRecord> PushFunc();
        public static void Push(string configFilePath, PushFunc pushFunc)
        {
            Task.Run(async () =>
            {
                try
                {

                    if (!File.Exists(configFilePath)) return;

                    var config = XDocument.Load(configFilePath);
                    var api = config.Descendants("api").FirstOrDefault();

                    if (api == null) return;

                    var host = api.Attribute("host")?.Value;
                    var port = api.Attribute("port")?.Value;
                    var endpoint = api.Attribute("endpoint")?.Value;

                    if (host == null || port == null || endpoint == null) return;

                    if (endpoint.StartsWith("/")) endpoint = endpoint.Substring(1);

                    var OSH_URL = $"http://{host}:{port}/{endpoint}";
                    var controlRecords = pushFunc(); 
                    if(controlRecords.Count > 0)
                    {
                        var json = JsonConvert.SerializeObject(controlRecords);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(OSH_URL, data);
                        string result = response.Content.ReadAsStringAsync().Result;
                    }
                }
                catch { }
            });
        }
    }
}
