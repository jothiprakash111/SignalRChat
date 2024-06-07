namespace WebApi.Helpers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;
using System;

public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    protected readonly IConfiguration Configuration;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<ChatMessages> chatMessages { get; set; }
    public DbSet<Groups> groups { get; set; }
}