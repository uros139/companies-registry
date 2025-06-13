using CompaniesRegistry.Domain;
using CompaniesRegistry.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompaniesRegistry.Infrastructure.Features.Companies;

internal sealed class CompaniesConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(ModelConstants.DefaultStringMaxLength);

        builder.Property(c => c.Exchange)
            .IsRequired()
            .HasMaxLength(ModelConstants.DefaultStringMaxLength);

        builder.Property(c => c.Ticker)
            .IsRequired()
            .HasMaxLength(ModelConstants.DefaultStringMaxLength);

        builder.Property(c => c.Isin)
            .IsRequired()
            .HasMaxLength(ModelConstants.DefaultStringMaxLength);

        builder.Property(c => c.WebSite)
            .HasMaxLength(ModelConstants.DefaultStringMaxLength);

        builder
            .HasIndex(c => c.Isin)
            .IsUnique();

        builder.ToTable("Companies", t =>
        {
            t.HasCheckConstraint("CK_Companies_Isin_AlphaPrefix", "Isin LIKE '[A-Z][A-Z]%'");
        });

    }
}
