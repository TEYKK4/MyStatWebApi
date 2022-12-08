using Microsoft.EntityFrameworkCore;
using MyStatWebApi.Models;

namespace MyStatWebApi.Services;

public class MyStatDbContext : DbContext
{
    public MyStatDbContext(DbContextOptions<MyStatDbContext> options) : base(options)
    {
        Database.EnsureCreated();
        // Database.EnsureDeleted();
    }

    public DbSet<Homework> Homeworks { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Homework>().HasData(new List<Homework>
        {
            new()
            {
                Id = 1,
                Description = "hello",
                DateTime = DateTime.Now,
                UserId = 1
            },
            new()
            {
                Id = 2,
                Description = "hello",
                DateTime = DateTime.Now,
                UserId = 1
            },
            new()
            {
                Id = 3,
                Description = "hello",
                DateTime = DateTime.Now,
                UserId = 1
            },
            new()
            {
                Id = 4,
                Description = "hello",
                DateTime = DateTime.Now,
                UserId = 2
            },
            new()
            {
                Id = 5,
                Description = "hello",
                DateTime = DateTime.Now,
                UserId = 2
            },
            new()
            {
                Id = 6,
                Description = "hello",
                DateTime = DateTime.Now,
                UserId = 2
            },
            new()
            {
                Id = 7,
                Description = "hello",
                DateTime = DateTime.Now,
                UserId = 2
            },
            new()
            {
                Id = 8,
                Description = "hello",
                DateTime = DateTime.Now,
                UserId = 2
            },
            new()
            {
                Id = 9,
                Description = "hello",
                DateTime = DateTime.Now,
                UserId = 2
            },
        });

        ISha256Encoder encoder = new Sha256Encoder();
        
        modelBuilder.Entity<User>().HasData(new List<User>
        {
            new()
            {
                Id = 1,
                Name = "qaqa",
                Login = "qaqa2022",
                Password = encoder.ComputeSha256Hash("qaqaqaqa"),
                Surname = "qaqasov"
            },
            new()
            {
                Id = 2,
                Login = "vadim",
                Name = "Vadim",
                Password = encoder.ComputeSha256Hash("vadimka2022"),
                Surname = "Siga"
            }
        });
    }
}