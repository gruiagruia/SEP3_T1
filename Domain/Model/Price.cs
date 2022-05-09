using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class Price
{
    [Required] public String travelClass { get; set;}
    [Required] public double pricePerSeat { get; set;}
    [Required] public String currency { get; set; }

    public Price()
    {
    }

    public Price(string travelClass, double pricePerSeat, string currency)
    {
        this.travelClass = travelClass;
        this.pricePerSeat = pricePerSeat;
        this.currency = currency;
    }
}