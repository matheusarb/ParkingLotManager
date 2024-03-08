using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.Data.Mappings;

public class VehicleMap : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicle");

        builder.HasKey(x => x.LicensePlate);
        builder.Property(x => x.LicensePlate)
            .IsRequired()
            .HasColumnName("LicensePlate")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(8);

        builder.Property(x => x.Brand)
            .IsRequired()
            .HasColumnName("Brand")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Model)
            .IsRequired()
            .HasColumnName("Model")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Color)
            .IsRequired()
            .HasColumnName("Color")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(40);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<int>()
            .HasColumnName("Type")
            .HasColumnType("INT");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("CreatedAt")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValue(DateTime.Now.ToUniversalTime());

        builder.Property(x => x.LastUpdateDate)
            .IsRequired()
            .HasColumnName("LastUpdateDate")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValue(DateTime.Now.ToUniversalTime());

        // Relationships
        builder.HasOne(x => x.Company)
            .WithMany(x => x.Vehicles)
            .HasConstraintName("FK_Vehicles_Company")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
