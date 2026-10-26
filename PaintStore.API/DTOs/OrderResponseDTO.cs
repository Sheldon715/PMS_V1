using System;

namespace PaintStore.API.DTOs;

public class OrderResponseDTO
{
    public DateTime OrderDate;
    public int UserId { get; set; }
    public Boolean Status { get; set; }
    public int OrderId { get; set; }
    public int TotalAmount { get; set; }
}
