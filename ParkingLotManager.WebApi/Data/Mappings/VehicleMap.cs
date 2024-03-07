using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.Data.Mappings;

public class VehicleMap : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {        
        builder.ToTable("Vehicle");

        // PrimaryKey
        builder.HasKey(x => x.LicensePlate);
        builder.Property(x => x.LicensePlate)
            .IsRequired()
            .HasColumnName("LicensePlate")
            .HasColumnType("VARCHAR")
            .HasMaxLength(8);
        
        builder.Property(x => x.Brand)
            .IsRequired()
            .HasColumnName("Brand")
            .HasColumnType("VARCHAR")
            .HasMaxLength(30);
        
        builder.Property(x => x.Model)
            .IsRequired()
            .HasColumnName("Model")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Color)
            .IsRequired()
            .HasColumnName("Color")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<int>()
            .HasColumnName("Type");

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

        //Index
        builder.HasIndex(x => x.LicensePlate, "IX_Vehicle_LicensePlate");
    }
}
