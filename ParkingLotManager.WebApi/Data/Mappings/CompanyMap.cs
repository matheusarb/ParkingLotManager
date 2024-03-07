using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingLotManager.WebApi.Models;

namespace ParkingLotManager.WebApi.Data.Mappings;

public class CompanyMap : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Company");

        //PrimaryKey and IDENTITY_COLUMN(1, 1)
        builder.HasKey(x => x.Name);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.Telephone)
            .IsRequired()
            .HasColumnName("Telephone")
            .HasColumnType("SMALLINT")
            .HasMaxLength(12);


        //builder.ComplexProperty(x => x.Cnpj)
        //    .IsRequired();
        // Relationships
        //builder.HasMany(x => x.Vehicles)
        //    .WithOne(x => x.CompanyId)
        //    .HasForeignKey("VehicleLicensePlate")
        //    .HasConstraintName("FK_Company_VehicleLicensePlate")
        //    .OnDelete(DeleteBehavior.Cascade);

    }
}
