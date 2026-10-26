using System;

namespace PaintStore.API.DTOs;

public class UpdatePaintProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
