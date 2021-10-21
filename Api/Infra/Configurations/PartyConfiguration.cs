using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartyInviter.Entities;

namespace PartyInviter.Infra.Configurations
{
    public class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.Property(x => x.Id).UseIdentityAlwaysColumn();
            builder.Property(x => x.Name).HasMaxLength(20);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.InvitationMessage).HasMaxLength(500);
            builder.HasMany(x => x.Guests).WithOne(x => x.Party);
        }
    }
}