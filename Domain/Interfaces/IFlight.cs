using Domain.Model;

namespace Domain.Interfaces;

public interface IFlight
{ 
    public Task<Flight> CreateFlightAsync(Flight flight);
    public Task<Flight> UpdateFlightAsync(int id);
    public Task<Flight> DeleteFlightAsync(int id);
    public Task<ICollection<Flight>> GetFlightsByParamAsync(string origin, string destination, string departureDate,
        string returnDate, bool oneWay, string travelClass, int passengers);
    public Task<ICollection<Flight>> GetAllFlightsAsync();
    
}