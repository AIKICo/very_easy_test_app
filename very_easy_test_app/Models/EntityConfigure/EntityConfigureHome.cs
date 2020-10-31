using Microsoft.EntityFrameworkCore.Metadata.Builders;
using very_easy_test_app.Models.Entities;

namespace very_easy_test_app.Models.EntityConfigure
{
    public class EntityConfigureHome : EntityConfigureBase<HomeEntity>
    {
        public override void Configure(EntityTypeBuilder<HomeEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(c => c.HomeOwener)
                .WithMany(c => c.Homes);
        }
    }
}