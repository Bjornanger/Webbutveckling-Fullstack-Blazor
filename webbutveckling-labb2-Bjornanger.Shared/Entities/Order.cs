using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webbutveckling_labb2_Bjornanger.Shared.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public double TotalPrice { get; set; }
   
    public int CustomerId { get; set; }
    public bool Status { get; set; } 
    public virtual ICollection<ProductsOrders> ProductOrders { get; set; }

}