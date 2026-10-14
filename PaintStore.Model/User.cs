using System;

namespace PaintStore.Model;

public class User
{
    public int Id { get; private set; }
    public readonly DateTime CreatedDate;
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }

}