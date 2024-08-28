namespace Infrastructure.Configurations.DbConfigurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Email)
               .IsUnique();

        builder.Property(x => x.Password)
            .HasMaxLength(100)
            .IsRequired();

        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.Property(x => x.City)
            .HasMaxLength(100)
            .IsRequired();

            addressBuilder.Property(x => x.StreetNo)
            .HasMaxLength(100)
            .IsRequired();
        });

    }
}