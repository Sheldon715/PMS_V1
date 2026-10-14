using System;

namespace PaintStore.Model;

public class PaintProduct
{
    public int Id { get; private set; }
    public readonly DateTime CreatedDate;
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int UserId { get; set; }

}
