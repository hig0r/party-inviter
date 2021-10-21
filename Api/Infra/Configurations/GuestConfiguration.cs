using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyInviter.Entities;

namespace PartyInviter.Infra.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.Property(x => x.Id).UseIdentityAlwaysColumn();
            builder.Property(x => x.Name).HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(50);
        }
    }
}