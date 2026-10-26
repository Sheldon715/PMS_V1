using System;

namespace PaintStore.Model;

public class Order
{
    public readonly DateTime CreatedDate;
    public int UserId { get; set; }
    public List<PaintProduct> PaintProducts { get; set; }
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate;
    public Boolean Status { get; set; }
    public int OrderId { get; set; }
    public int TotalAmount { get; set; }

}
