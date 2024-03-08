using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public double TotalPrice { get; set; }

    public Customer Customer { get; set; }
    public bool Status { get; set; } // Sätta 0, 1 till status - mottagen / skickad
    public virtual ICollection<ProductsOrders> ProductOrders { get; set; }

}