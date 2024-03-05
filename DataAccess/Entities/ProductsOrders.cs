using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities;

public class ProductsOrders
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public Order Order { get; set; }
    public int OrderId { get; set; }
    public int Amount { get; set; }


}