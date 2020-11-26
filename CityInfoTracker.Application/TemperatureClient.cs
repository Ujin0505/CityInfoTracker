using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CityInfoTracker.Application
{
    public class TemperatureClient
    {
        private readonly HttpClient _client;
        private string _key;

        public TemperatureClient(HttpClient client, IConfiguration configuration)
        {
            _key = configuration["OpenWeatherApiKey"];
            _client = client;
            _client.BaseAddress = new Uri($"http://api.openweathermap.org");
            _client.DefaultRequestHeaders.Clear();
            /*_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));*/
        }

        public async Task<string> GetData(int zipCode)
        {
            string uri = $"/data/2.5/weather?zip={zipCode}&appid={_key}";
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _client.SendAsync(message);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            
            return null;
        }
    }
}