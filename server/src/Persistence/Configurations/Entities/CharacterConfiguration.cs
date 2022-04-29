using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Entities
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasIndex(c => c.Nickname);

            builder.Property(c => c.Nickname)
                .HasMaxLength(12)
                .IsRequired();

            builder
                .HasOne(c => c.Creator)
                .WithMany(u => u.CreatedCharacters)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}