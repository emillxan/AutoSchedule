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
    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<Lesson> Lesson { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Squad>(builder =>
        {
            builder.ToTable("Squad").HasKey(x => x.Id);

            builder.HasData([
                new Squad { Id = 1, Number = "2450r", SubjectIds = [1,2] },
                new Squad { Id = 2, Number = "1451r", SubjectIds = [1,3] },
                new Squad { Id = 3, Number = "2440r", SubjectIds = [5,6,7] },
/*                new Squad { Id = 4, Number = "2450a" },
                new Squad { Id = 5, Number = "2430a" },
                new Squad { Id = 6, Number = "1452i" },
                new Squad { Id = 7, Number = "1450i" },
                new Squad { Id = 8, Number = "2440r" },*/
            ]);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Number).HasMaxLength(25).IsRequired();
        });

        modelBuilder.Entity<Cabinet>(builder =>
        {
            builder.ToTable("Cabinet").HasKey(x => x.Id);

            builder.HasData([
                new Cabinet { Id = 1, Number = "309", },
                new Cabinet { Id = 2, Number = "311", },
                new Cabinet { Id = 3, Number = "312", },
                new Cabinet { Id = 4, Number = "313", },
                new Cabinet { Id = 5, Number = "320", },
                new Cabinet { Id = 6, Number = "321", },
                new Cabinet { Id = 7, Number = "322", },
                new Cabinet { Id = 8, Number = "323", },
                new Cabinet { Id = 9, Number = "306", },
                new Cabinet { Id = 10, Number = "304", },
            ]);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Number).HasMaxLength(25).IsRequired();
        });

        modelBuilder.Entity<Subject>(builder =>
        {
            builder.ToTable("Subject").HasKey(x => x.Id);

            builder.HasData([
                new Subject { Id = 1, Name = "OOP", },
                new Subject { Id = 2, Name = "English",  },
                new Subject { Id = 3, Name = "WEB",  },
                new Subject { Id = 4, Name = "Economy",  },
                new Subject { Id = 5, Name = "ASKP", },
                new Subject { Id = 6, Name = "Fizik", },
                new Subject { Id = 7, Name = "E-Comers", },
                new Subject { Id = 8, Name = "Phylosofi", },
                new Subject { Id = 9, Name = "C#", },
                new Subject { Id = 10, Name = "KiberSecurity", },
                new Subject { Id = 11, Name = "C++", },
                new Subject { Id = 12, Name = "Python", },
                new Subject { Id = 13, Name = "Math", },
                new Subject { Id = 14, Name = "Chemistry", },
                new Subject { Id = 15, Name = "DB", },
                new Subject { Id = 16, Name = "An Math", },
            ]);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(125).IsRequired();
        });

        modelBuilder.Entity<Teacher>(builder =>
        {
            builder.ToTable("Teacher").HasKey(x => x.Id);

            builder.HasData([
                new Teacher { Id = 1, Name = "Fidan", SubjectIds = [1, 2, 3] },
                new Teacher { Id = 2, Name = "Fuad", SubjectIds = [4, 5] },
                new Teacher { Id = 3, Name = "Vagif", SubjectIds = [6, 7, 8] },
                new Teacher { Id = 4, Name = "Elvin", SubjectIds = [9] },
                new Teacher { Id = 5, Name = "Solmaz", SubjectIds = [10, 11, 12, 13] },
                new Teacher { Id = 6, Name = "Teacher", SubjectIds = [14, 15, 16] },
            ]);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        });


      
    }
}
