using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Entities
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Body)
                .HasMaxLength(1000);

            builder
                .HasOne(r => r.Author)
                .WithMany(u => u.Reviews)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(r => r.Character)
                .WithMany(c => c.Reviews)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}