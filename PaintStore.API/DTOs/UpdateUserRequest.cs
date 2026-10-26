using System;

namespace PaintStore.API.DTOs;

public class UpdateUserRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
