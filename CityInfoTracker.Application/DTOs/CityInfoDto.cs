using System;

namespace CityInfoTracker.Application.DTOs
{
    public class CityInfoDto
    {
        public string Name { get; set; }
        public double Temperature { get; set; }
        public string TimeZone { get; set; }
        public double Longtitude { get; set; }

        public double Latitude { get; set; }
    }
}