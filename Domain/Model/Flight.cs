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
    [Required] public string status { get; set; }
    [Required] public string currency { get; set; }

    public ICollection<AdditionalService> additionalServices { get; set;}
    
    public ICollection<Seat> seats { get; set; }

    public Flight(){}
    
    public Flight(int aircraftCode, string airline, string origin, string destination, string departureDate, string arrivalDate, int duration, string status, string currency)
    {
        this.aircraftCode = aircraftCode;
        this.airline = airline;
        this.origin = origin;
        this.destination = destination;
        this.departureDate = departureDate;
        this.arrivalDate = arrivalDate;
        this.duration = duration;
        this.status = status;
        this.currency = currency;
    }

    public Flight(int aircraftCode, string airline, string origin, string destination, string departureDate, string arrivalDate, int duration, string status, ICollection<AdditionalService> additionalServices)
    {
        this.aircraftCode = aircraftCode;
        this.airline = airline;
        this.origin = origin;
        this.destination = destination;
        this.departureDate = departureDate;
        this.arrivalDate = arrivalDate;
        this.duration = duration;
        this.status = status;
        this.additionalServices = additionalServices;
    }

    public Flight(int flightId, int aircraftCode, string airline, string origin, string destination, string departureDate, string arrivalDate, int duration, string status, ICollection<AdditionalService> additionalServices, ICollection<Seat> seats)
    {
        this.flightId = flightId;
        this.aircraftCode = aircraftCode;
        this.airline = airline;
        this.origin = origin;
        this.destination = destination;
        this.departureDate = departureDate;
        this.arrivalDate = arrivalDate;
        this.duration = duration;
        this.status = status;
        this.additionalServices = additionalServices;
        this.seats = seats;
    }

    public Flight(int flightId, int aircraftCode, string airline, string origin, string destination, string departureDate, string arrivalDate, int duration, string status)
    {
        this.flightId = flightId;
        this.aircraftCode = aircraftCode;
        this.airline = airline;
        this.origin = origin;
        this.destination = destination;
        this.departureDate = departureDate;
        this.arrivalDate = arrivalDate;
        this.duration = duration;
        this.status = status;
    }

    public String toString()
    {
        return "Flight{" +
               "flightId=" + flightId +
               "aircraftCode=" + aircraftCode +
               ", airline='" + airline + '\'' +
               ", origin='" + origin + '\'' +
               ", destination='" + destination + '\'' +
               ", departureDate=" + departureDate +
               ", arrivalDate=" + arrivalDate +
               ", duration=" + duration +
               ", status='" + status + '\'' +
               '}';
    }
}