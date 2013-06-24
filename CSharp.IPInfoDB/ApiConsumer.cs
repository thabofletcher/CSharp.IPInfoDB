using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.IPInfoDB
{
    public class ApiConsumer
    {
        private readonly string _ApiKey;
        const string PATH = "http://api.ipinfodb.com/v3/ip-city/?format=json&key=";

        public ApiConsumer(string apiKey)
        {
            _ApiKey = apiKey;
        }

        public ApiModel GetLocation(string ip)
        {
            return GetLocationAsync(ip).Result;
        }

        public async Task<ApiModel> GetLocationAsync(string ip)
        {
            //Create HttpClient for making request for profile
            var client = new HttpClient();

            //Get profile response as a JSON string
            var jsonStringTask = client.GetStringAsync(PATH + _ApiKey + "&ip=" + ip)
                                       .ConfigureAwait(continueOnCapturedContext: false);
        
            //Await
            var jsonString = await jsonStringTask;

            //Convert to JSON string to Coderbits model
            var coderbitsModelTask = JsonConvert.DeserializeObjectAsync<ApiModel>(jsonString);

            //Await
            var coderbitsModel = await coderbitsModelTask;

            //Return the model
            return coderbitsModel;
        }
    }
}
