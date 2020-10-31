using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using very_easy_test_app.Models.Entities;

namespace very_easy_test_app.Models.EntityConfigure
{
    public class EntityConfigureBase<T> : IEntityTypeConfiguration<T>
        where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.id);
            builder.Property(c => c.id).ValueGeneratedNever();
            builder.Property(c => c.title).IsRequired().HasMaxLength(250);
            builder.Property(c => c.allowDelete).IsRequired();

            builder.HasIndex(c => c.allowDelete);
            builder.HasQueryFilter(c => c.allowDelete == false);
        }
    }
}