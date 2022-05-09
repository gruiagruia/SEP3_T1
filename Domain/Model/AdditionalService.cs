using System.ComponentModel.DataAnnotations;

namespace Domain.Model;

public class AdditionalService
{
    [Required] public String type { get; set; }
    [Required] public bool available { get; set; }
    [Required] public double price { get; set; }

    [Required] public String currency { get; set;}
    public AdditionalService()
    {
    }
    
    public AdditionalService(string type, bool available, double price)
    {
        this.type = type;
        this.available = available;
        this.price = price;
    }
}