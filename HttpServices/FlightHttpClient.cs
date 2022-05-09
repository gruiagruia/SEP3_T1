using System.Text;
using Domain.Interfaces;
using Domain.Model;
using System.Text.Json;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HttpServices;

public class FlightHttpClient : IFlight
{ 
    public async Task<Flight> CreateFlightAsync(Flight flight)
    {
        using HttpClient client = new();
        string flightAsJson = JsonConvert.SerializeObject(flight, Formatting.Indented);

        StringContent content = new(flightAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync($"http://localhost:8080/flights", content);

        string responseContent = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine("HTTP Response: " + responseContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
    
        Flight returned = JsonSerializer.Deserialize<Flight>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
     
        return returned;
    }

    public Task<Flight> UpdateFlightAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public Task<Flight> DeleteFlightAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<ICollection<Flight>> GetAllFlightsAsync()
    {
        using HttpClient client = new ();
        HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:8080/flights/all");
        string content = await responseMessage.Content.ReadAsStringAsync();
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {responseMessage.StatusCode}, {content}");
        }
        
        ICollection<Flight> flights = JsonSerializer.Deserialize<ICollection<Flight>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return flights;
    } 
    
    
    public async Task<ICollection<Flight>> GetFlightsByParamAsync(string origin, string destination,
        string departureDate,
        string arrivalDate, bool oneWay,
        string travelClass, int passengers)
    {
        using HttpClient client = new();
        HttpResponseMessage responseMessage = await client.GetAsync(
            $"http://localhost:8080/flights/?origin=SYD&destination=BKK&departDate=01-11-2021&oneWay=true&travelClass=ECONOMY&adults=1");
        string content = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {responseMessage.StatusCode}, {content}");
        }

        ICollection<Flight> flights = JsonSerializer.Deserialize<ICollection<Flight>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return flights;
    }
}