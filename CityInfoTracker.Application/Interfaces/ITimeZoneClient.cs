using System.Threading.Tasks;

namespace CityInfoTracker.Application.Interfaces
{
    public interface ITimeZoneClient
    {
        Task<string> GetData(double longtitude, double latitude);
    }
}