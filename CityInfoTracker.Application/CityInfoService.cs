using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CityInfoTracker.Application.DTOs;
using CityInfoTracker.Application.Interfaces;
using CityInfoTracker.Domain.Entities;
using CityInfoTracker.Persistence;

namespace CityInfoTracker.Application
{
    public class CityInfoService: ICityInfoService
    {
        private readonly ITemperatureClient _temperatureClient;
        private readonly ITimeZoneClient _timeZoneClient;
        private readonly ApplicationDbContext _dbContext;

        public CityInfoService(ITemperatureClient temperatureClient, ITimeZoneClient timeZoneClient, ApplicationDbContext dbContext)
        {
            _temperatureClient = temperatureClient;
            _timeZoneClient = timeZoneClient;
            _dbContext = dbContext;
        }

        public async Task<CityInfoDto> Get(int zipCode)
        {
            var tempInfo = await GetTempInfo(zipCode);
            if (tempInfo == null) return null;
            
            string timeZone = await GetTimeZone(tempInfo.Longtitude, tempInfo.Latitude);

            var cityInfo = new CityInfo()
            {
                Name = tempInfo.Name,
                Temperature = tempInfo.Temp,
                Timezone = timeZone,
                DateTime = DateTime.Now
            };
            await _dbContext.CitiesInfo.AddAsync(cityInfo);
            await _dbContext.SaveChangesAsync();
            
            return new CityInfoDto()
            {
                Name = tempInfo.Name,
                Temperature = tempInfo.Temp,
                TimeZone =  timeZone,
                Longtitude =  tempInfo.Longtitude,
                Latitude = tempInfo.Latitude,
            };
        } 

        private async Task<TemperatureInfoDto> GetTempInfo(int zipCode)
        {
            var data = await _temperatureClient.GetData(zipCode);
            if (data == null) return null;

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(data /*, new JsonDocumentOptions() {AllowTrailingCommas = true}*/))
                {
                    var main = doc.RootElement.GetProperty("main");
                    var coord = doc.RootElement.GetProperty("coord");
                    
                    return new TemperatureInfoDto()
                    {
                        Name = doc.RootElement.GetProperty("name").GetString(),
                        Temp = main.GetProperty("temp").GetDouble(),
                        Latitude = coord.GetProperty("lat").GetDouble(),
                        Longtitude = coord.GetProperty("lon").GetDouble()
                    }; 
                }
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
        private async Task<string> GetTimeZone(double longtitude, double latitude)
        {
            var data = await _timeZoneClient.GetData(longtitude, latitude);
            if (data == null) return null;

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(data))
                {
                    return doc.RootElement.GetProperty("timeZoneName").GetString();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}