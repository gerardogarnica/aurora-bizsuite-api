namespace Aurora.BizSuite.Items.Infrastructure.Categories;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Keys
        builder
            .HasKey(e => e.Id)
            .HasName("PK_Category");

        // Properties
        builder
            .Property(p => p.Id)
            .HasColumnName("CategoryId")
            .HasConversion(id => id.Value, value => new CategoryId(value))
            .IsRequired();

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.ParentId)
            .HasConversion(id => id.Value, value => new CategoryId(value))
            .IsRequired();

        builder
            .Property(p => p.Notes)
            .HasMaxLength(1000);

        builder.AddAuditableProperties();
    }
}