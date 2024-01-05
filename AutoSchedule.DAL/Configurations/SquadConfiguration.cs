using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSchedule.DAL.Configurations;

public class SquadConfiguration : IEntityTypeConfiguration<Squad>
{
    public void Configure(EntityTypeBuilder<Squad> builder)
    {
        builder.ToTable("Squad").HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Number).HasMaxLength(25).IsRequired();


        builder.HasOne(s => s.Department)
        .WithMany(d => d.Squads)
        .HasForeignKey(s => s.DepartmentId);

        builder.HasOne(s => s.Faculty)
        .WithMany(f => f.Squads)
        .HasForeignKey(s => s.FacultyId);


        builder.HasData([
            new Squad { Id = 1, Number = "2450r", SubjectIds = [1, 2, 3, 4, 5] },
            /*                 new Squad { Id = 2, Number = "1451r", SubjectIds = [1,3] },
                           new Squad { Id = 3, Number = "2440r", SubjectIds = [5,6,7] },
                         new Squad { Id = 4, Number = "2450a" },
                           new Squad { Id = 5, Number = "2430a" },
                           new Squad { Id = 6, Number = "1452i" },
                           new Squad { Id = 7, Number = "1450i" },
                           new Squad { Id = 8, Number = "2440r" },*/
        ]);
    }
}
