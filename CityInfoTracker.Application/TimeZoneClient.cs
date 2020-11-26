using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CityInfoTracker.Application
{
    public class TimeZoneClient
    {
        private readonly string _key;
        private readonly HttpClient _client;

        public TimeZoneClient(HttpClient client, IConfiguration configuration)
        {
            _key = configuration["GoogleTimeZoneApiKey"];
            _client = client;
            _client.BaseAddress = new Uri($"https://maps.googleapis.com/");
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<string> GetData(double longtitude, double latitude)
        {
            string uri = $"/maps/api/timezone/json?location={longtitude.ToString(CultureInfo.InvariantCulture)},{latitude.ToString(CultureInfo.InvariantCulture)}&timestamp=0&key={_key}";
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _client.SendAsync(message);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            
            return null;
        }
    }
}