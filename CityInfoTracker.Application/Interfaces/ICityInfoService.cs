using System.Threading.Tasks;
using CityInfoTracker.Application.DTOs;

namespace CityInfoTracker.Application.Interfaces
{
    public interface ICityInfoService
    {
        Task<CityInfoDto> Get(int zipCode);
    }
}