﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.Data.Mappings;

public class CompanyMap : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Company");

        //PrimaryKey and IDENTITY_COLUMN(1, 1)
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        // Relationships
        builder.HasMany(x => x.Vehicles)
            .WithOne(x => x.CompanyId)
            .HasForeignKey("VehicleLicensePlate")
            .HasConstraintName("FK_Company_VehicleLicensePlate")
            .OnDelete(DeleteBehavior.Cascade);           
            
    }
}
