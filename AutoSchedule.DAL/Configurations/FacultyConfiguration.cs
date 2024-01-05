using AutoSchedule.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchedule.DAL.Configurations;

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.ToTable("Faculties");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(f => f.Departments)
            .WithOne(d => d.Faculty)
            .HasForeignKey(d => d.FacultyId);

        builder.HasMany(f => f.Squads)
            .WithOne(s => s.Faculty)
            .HasForeignKey(s => s.FacultyId);
    }
}
