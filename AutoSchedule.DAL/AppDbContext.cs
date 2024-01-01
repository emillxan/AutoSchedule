using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;

namespace AutoSchedule.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Squad> Squad { get; set; }
    public DbSet<Cabinet> Cabinet { get; set; }
    public DbSet<Subject> Subject { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Squad>(builder =>
        {
            builder.ToTable("Squad").HasKey(x => x.Id);

            builder.HasData(new Squad
            {
                Id = 1,
                Number = "2450r"
            });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Number).HasMaxLength(25).IsRequired();
        });

        modelBuilder.Entity<Cabinet>(builder =>
        {
            builder.ToTable("Cabinet").HasKey(x => x.Id);

            builder.HasData([
                new Cabinet { Id = 1, Number = "309", NeedComputer = true },
                new Cabinet { Id = 2, Number = "311", NeedComputer = true },
                new Cabinet { Id = 3, Number = "312", NeedComputer = false },
                new Cabinet { Id = 4, Number = "313", NeedComputer = true },
                new Cabinet { Id = 5, Number = "320", NeedComputer = false },
                new Cabinet { Id = 6, Number = "321", NeedComputer = true },
                new Cabinet { Id = 7, Number = "322", NeedComputer = true },
                new Cabinet { Id = 8, Number = "323", NeedComputer = true },
                new Cabinet { Id = 9, Number = "306", NeedComputer = true },
                new Cabinet { Id = 10, Number = "304", NeedComputer = false },
            ]);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Number).HasMaxLength(25).IsRequired();
        });

        modelBuilder.Entity<Subject>(builder =>
        {
            builder.ToTable("Subject").HasKey(x => x.Id);

            builder.HasData([
                new Subject { Id = 1, Name = "OOP", NeedComputer = true },
                new Subject { Id = 2, Name = "WEB", NeedComputer = true },
                new Subject { Id = 1, Name = "English", NeedComputer = false },
                new Subject { Id = 1, Name = "Economy", NeedComputer = false },
            ]);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(125).IsRequired();
        });
    }
}
