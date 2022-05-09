using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class Flight
{
    public int flightId { get; set; }
    [Required] public int aircraftCode { get; set; }
    [Required] public string airline { get; set; }
    [Required] public string origin { get; set; }
    [Required] public string destination { get; set; }
    [Required] public string departureDate { get; set; }
    [Required] public string arrivalDate { get; set; }
    public int duration { get; set; }
    [Required] public int numberOfBookableSeats { get; set; }
    [Required] public string status { get; set; }

    public ICollection<AdditionalService> additionalServices { get; set;}
    
    [Required] public ICollection<Price> prices { get; set;}
    
    public Flight(){}
    
    public Flight(int aircraftCode, string airline, string origin, string destination, string departureDate, string arrivalDate, int duration, int numberOfBookableSeats, string status)
    {
        this.aircraftCode = aircraftCode;
        this.airline = airline;
        this.origin = origin;
        this.destination = destination;
        this.departureDate = departureDate;
        this.arrivalDate = arrivalDate;
        this.duration = duration;
        this.numberOfBookableSeats = numberOfBookableSeats;
        this.status = status;
    }

    public Flight(int aircraftCode, string airline, string origin, string destination, string departureDate, string arrivalDate, int duration, int numberOfBookableSeats, string status, ICollection<AdditionalService> additionalServices, ICollection<Price> prices)
    {
        this.aircraftCode = aircraftCode;
        this.airline = airline;
        this.origin = origin;
        this.destination = destination;
        this.departureDate = departureDate;
        this.arrivalDate = arrivalDate;
        this.duration = duration;
        this.numberOfBookableSeats = numberOfBookableSeats;
        this.status = status;
        this.additionalServices = additionalServices;
        this.prices = prices;
    }

    public Flight(int flightId, int aircraftCode, string airline, string origin, string destination, string departureDate, string arrivalDate, int duration, int numberOfBookableSeats, string status, ICollection<AdditionalService> additionalServices, ICollection<Price> prices)
    {
        this.flightId = flightId;
        this.aircraftCode = aircraftCode;
        this.airline = airline;
        this.origin = origin;
        this.destination = destination;
        this.departureDate = departureDate;
        this.arrivalDate = arrivalDate;
        this.duration = duration;
        this.numberOfBookableSeats = numberOfBookableSeats;
        this.status = status;
        this.additionalServices = additionalServices;
        this.prices = prices;
    }
}