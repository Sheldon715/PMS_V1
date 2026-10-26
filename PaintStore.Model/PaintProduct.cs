using System;

namespace PaintStore.Model;

public class PaintProduct
{
    public int Id { get; set; }
    public readonly DateTime CreatedDate;
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public int Stock { get; set; }
    public int UserId { get; set; }

}
