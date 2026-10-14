using System;

namespace PaintStore.Model;

public class Order
{
    public readonly DateTime CreatedDate;
    public int UserId { get; private set; }
    public List<PaintProduct> PaintProducts { get; private set; }
    public int Id { get; private set; }
    public decimal TotalPrice { get; private set; }

}
