
using Domain.Model;

namespace Domain.Interfaces;

public interface IAmadeus
{
    public Task<ICollection<string>> GetAirportsAsync(string keyword);
    public Task<Trip> GetTripAsync();
}