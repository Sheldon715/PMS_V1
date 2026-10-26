using System;

namespace PaintStore.Model;

public class User
{
    public int Id { get; set; }
    public readonly DateTime CreatedDate;
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

}