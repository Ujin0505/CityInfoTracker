using System.Threading.Tasks;

namespace CityInfoTracker.Application.Interfaces
{
    public interface ITemperatureClient
    {
        Task<string> GetData(int zipCode);
    }
}