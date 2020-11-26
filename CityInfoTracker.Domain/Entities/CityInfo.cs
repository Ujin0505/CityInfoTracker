using System;

namespace CityInfoTracker.Domain.Entities
{
    public class CityInfo
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public double Temperature { get; set; }
        public string Timezone { get; set; }
    }
}