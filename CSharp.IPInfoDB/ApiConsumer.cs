using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.IPInfoDB
{
    public class ApiConsumer
    {
        private readonly string _ApiKey;
        private readonly bool _FixCase;
        // Creates a TextInfo based on the "en-US" culture.
        private readonly TextInfo _TextInfo = new CultureInfo("en-US",false).TextInfo;
        const string CITY_PATH = "http://api.ipinfodb.com/v3/ip-city/?format=json&key=";
        const string COUNTRY_PATH = "http://api.ipinfodb.com/v3/ip-country/?format=json&key=";

        public ApiConsumer(string apiKey, bool fixCase = false)
        {
            _ApiKey = apiKey;
            _FixCase = fixCase;
        }

        public CityLocationModel GetCity(string ip)
        {
            return GetCityAsync(ip).Result;
        }

        public async Task<CityLocationModel> GetCityAsync(string ip)
        {
            return await GetLocation<CityLocationModel>(CITY_PATH + _ApiKey + "&ip=" + ip);
        }

        public CountryLocationModel GetCountry(string ip)
        {
            return GetCountryAsync(ip).Result;
        }

        public async Task<CountryLocationModel> GetCountryAsync(string ip)
        {
            return await GetLocation<CountryLocationModel>(COUNTRY_PATH + _ApiKey + "&ip=" + ip);
        }

        private async Task<T> GetLocation<T>(string path) where T : IPInfoDBModel
        {
            //Create HttpClient for making request for profile
            var client = new HttpClient();

            //Get profile response as a JSON string
            var jsonStringTask = client.GetStringAsync(path).ConfigureAwait(continueOnCapturedContext: false);

            //Await
            var jsonString = await jsonStringTask;

            //Convert to JSON string to Coderbits model
            var modelTask = JsonConvert.DeserializeObjectAsync<T>(jsonString);

            //Await
            T model = await modelTask;

            //Return the model
            return FixCase<T>(model);
        }

        private T FixCase<T>(dynamic model) where T : IPInfoDBModel
        {
            if (_FixCase)
            {                
                model.countryName = _TextInfo.ToTitleCase(model.countryName.ToLower());
                try
                {
                    model.cityName = _TextInfo.ToTitleCase(model.cityName.ToLower());
                    model.regionName = _TextInfo.ToTitleCase(model.regionName.ToLower());
                }
                catch(RuntimeBinderException)
                {
                }
            }
            return model;
        }
    }
}
