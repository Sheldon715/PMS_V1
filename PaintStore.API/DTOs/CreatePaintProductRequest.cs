using System;

namespace PaintStore.API.DTOs;

public class CreatePaintProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public int Stock { get; set; }
}
