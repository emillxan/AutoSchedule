using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoSchedule.DAL.Configurations;

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
    public DbSet<Faculty> Faculty { get; set; }
    public DbSet<Department> Department { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SquadConfiguration());

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
                new Subject { Id = 1, Name = "OOP", WeeklyFrequency = 3, TotalHours = 75 },
                new Subject { Id = 2, Name = "English", WeeklyFrequency = 2, TotalHours = 60 },
                new Subject { Id = 3, Name = "WEB", WeeklyFrequency = 1, TotalHours = 60 },
                new Subject { Id = 4, Name = "Economy", WeeklyFrequency = 2, TotalHours = 60 },
                new Subject { Id = 5, Name = "ASKP", WeeklyFrequency = 3, TotalHours = 60 },
                new Subject { Id = 6, Name = "Fizik", WeeklyFrequency = 2, TotalHours = 60 },
                new Subject { Id = 7, Name = "E-Comers", WeeklyFrequency = 1, TotalHours = 60 },
                new Subject { Id = 8, Name = "Phylosofi", WeeklyFrequency = 2, TotalHours = 60 },
                new Subject { Id = 9, Name = "C#", WeeklyFrequency = 1, TotalHours = 60 },
                new Subject { Id = 10, Name = "KiberSecurity", WeeklyFrequency = 2, TotalHours = 60 },
                new Subject { Id = 11, Name = "C++", WeeklyFrequency = 1, TotalHours = 60 },
                new Subject { Id = 12, Name = "Python", WeeklyFrequency = 2, TotalHours = 60 },
                new Subject { Id = 13, Name = "Math", WeeklyFrequency = 1, TotalHours = 60 },
                new Subject { Id = 14, Name = "Chemistry", WeeklyFrequency = 2, TotalHours = 60 },
                new Subject { Id = 15, Name = "DB", WeeklyFrequency = 3, TotalHours = 60 },
                new Subject { Id = 16, Name = "An Math", WeeklyFrequency = 3, TotalHours = 60 },
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

        modelBuilder.Entity<Department>(builder =>
        {
            builder.HasOne(d => d.Faculty)  
            .WithMany(f => f.Departments)  
            .HasForeignKey(d => d.FacultyId);
        });
    }
}
