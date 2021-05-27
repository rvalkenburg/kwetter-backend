using Kwetter.Services.ProfileService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kwetter.Services.ProfileService.Persistence.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DisplayName)
                .IsRequired();
            builder.Property(x => x.Avatar)
                .IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.Email);
            builder.Property(x => x.GoogleId)
                .IsRequired();
            builder.Property(x => x.DateOfCreation);
        }
    }
}