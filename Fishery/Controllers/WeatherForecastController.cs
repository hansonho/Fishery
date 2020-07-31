using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fishery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        const string clientId = "7807fb46-5f7f-11ea-bc55-0242ac130003";
        const string clientSecret = "015de4ec-8adc-492d-84a4-9c66706c79f5";

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            DeviceKeyPair[] deviceKeyPairs = new DeviceKeyPair[] {
                new DeviceKeyPair{ Senser ="IN21IIMSN-0353010301/", IdNumber = 401300, Name = "風向", Unit = "deg"  },
                new DeviceKeyPair{ Senser ="IN21IIMSN-0353-TEST1/", IdNumber = 400200, Name = "濕度", Unit = "%"  },
                new DeviceKeyPair{ Senser ="IN21IIMSN-0353-TEST2/", IdNumber = 400100, Name = "溫度", Unit="℃"   },
                new DeviceKeyPair{ Senser ="IN21IIMSN-0353-TEST3/", IdNumber = 401500, Name = "PM2.5", Unit = "μg/m3"   },
                new DeviceKeyPair{ Senser ="IN21IIMSN-0353-TEST6/", IdNumber = 401200, Name = "大氣壓力" , Unit = "kg/cm2"  },
                new DeviceKeyPair{ Senser ="IN21IIMSN-0353010201/", IdNumber = 401400, Name = "風速", Unit ="m/s"   },
                new DeviceKeyPair{ Senser ="IN21IIMSN-0353010101/", IdNumber = 402900, Name = "雨量" , Unit = "mm/hr"  }
            };

            string url = $"https://harbor-auth.insynerger.com:9999/auth/oauth/token?client_id={clientId}&client_secret={clientSecret}&grant_type=password&username=apiuser&password=apiuser@2020&apsystem=ILC";

            var hash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));

            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.Headers.Set("Authorization", "Basic " + hash);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";

            var response = httpWebRequest.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string result = sr.ReadToEnd();
            sr.Close();
            dynamic res = JsonConvert.DeserializeObject<dynamic>(result);
            string accessToken = res.access_token;

            List<string> re = new List<string>();
            foreach (DeviceKeyPair a in deviceKeyPairs)
            {
                try
                {
                    re.Add(GetData(a, accessToken));
                }
                catch (Exception)
                {
                }
            }
            var rrr = new { data = re };

            return JsonConvert.SerializeObject(rrr);

        }


        private string GetData(DeviceKeyPair deviceKeyPair, string token)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create("https://harbor.insynerger.com/api/incommon/v2/devices/sensor/" + deviceKeyPair.Senser);
            httpWebRequest.Headers.Set("Authorization", "Bearer " + token);
            httpWebRequest.Method = "GET";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            var response = httpWebRequest.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string result = sr.ReadToEnd();
            sr.Close();

            dynamic data = JsonConvert.DeserializeObject<dynamic>(result);
            var values = data.data.attributes as JArray;
            var targetData = values.FirstOrDefault(a => a.Value<int>("id") == deviceKeyPair.IdNumber);

            return $"{deviceKeyPair.Name} : {targetData.Value<JToken>("attrValue").Value<string>("value")} {deviceKeyPair.Unit}";
        }

        public class DeviceKeyPair
        {
            public string Senser { get; set; }
            public int IdNumber { get; set; }
            public string Name { get; set; }
            public string Unit { get; set; }
        }
    }
}
