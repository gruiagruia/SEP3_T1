namespace Domain.Model;

public class Trip
{
    public bool oneWay { get; set; }
    public DateTime returnDate { get; set; }
    public int passengers { get; set; }
    public string travelClass { get; set; }
    public double grandTotal { get; set; }
    public string currency { get; set; }
    public ICollection<Flight> flights { get; set; }

    public Trip(bool oneWay, DateTime returnDate, int passengers, string travelClass, double grandTotal, string currency, ICollection<Flight> flights)
    {
        this.oneWay = oneWay;
        this.returnDate = returnDate;
        this.passengers = passengers;
        this.travelClass = travelClass;
        this.grandTotal = grandTotal;
        this.currency = currency;
        this.flights = flights;
    }
}