using System;
using PaintStore.Model;

namespace PaintStore.API.DTOs;

public class CreateOrderRequest
{
    public int UserId { get; set; }
    public List<PaintProduct> PaintProducts { get; set; }
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public int Quantity { get; set; }
}
