namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public DbSet<ChatMessages> chatMessages { get; set; }
    public DbSet<Users> users { get; set; }
    public DbSet<Groups> groups { get; set; }
}