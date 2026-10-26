using System;
using PaintStore.Model;
using Microsoft.EntityFrameworkCore;

namespace PaintStore.API;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<PaintProduct> PaintProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
}
