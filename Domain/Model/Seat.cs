namespace Domain.Model;

public class Seat
{
    public int seatId { get; set; }
    public string travelClass { get; set; }
    public double pricePerSeat { get; set; }
    //public string currency { get; set; }
    public int numberOfBookableSeats { get; set; }
    public int flight_id { get; set; }

    public Seat()
    {
    }
    
    public Seat(string travelClass, double pricePerSeat, int numberOfBookableSeats)
    {
        this.travelClass = travelClass;
        this.pricePerSeat = pricePerSeat;
        this.numberOfBookableSeats = numberOfBookableSeats;
    }
    
    public String toString()
    {
        return "Seat{" +
               "seatId=" + seatId +
               "travelClass=" + travelClass +
               ", pricePerSeat='" + pricePerSeat + '\'' +
               ", numberOfBookableSeats='" + numberOfBookableSeats + '\'' +
               ", flight_id='" + flight_id + '\'' +
               '}';
    }
}