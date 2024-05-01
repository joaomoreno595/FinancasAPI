using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(x => x.CategoryId);
        builder.Property(x => x.Name).HasMaxLength(30);

        builder.HasData(
            new Category() { CategoryId = 1, Name = "Salário" },
            new Category() { CategoryId = 2, Name = "Casa" },
            new Category() { CategoryId = 3, Name = "Carro" },
            new Category() { CategoryId = 4, Name = "Diversos" }
        );
    }
}
