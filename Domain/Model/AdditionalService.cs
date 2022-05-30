using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class AdditionalService
{
    [Required] int additionalServiceId { get; set; }
    [Required] public string type { get; set; }
    [Required] public bool available { get; set; }
    [Required] public double price { get; set; }

    [Required] public int flight_id { get; set;}
    public AdditionalService()
    {
    }
    
    public AdditionalService(string type, bool available, double price)
    {
        this.type = type;
        this.available = available;
        this.price = price;
    }
    
    public String toString()
    {
        return "Additional Service{" +
               "additionalServiceId=" + additionalServiceId +
               "type=" + type +
               ", available='" + available + '\'' +
               ", price='" + price + '\'' +
               ", flight_id='" + flight_id + '\'' +
               '}';
    }
}