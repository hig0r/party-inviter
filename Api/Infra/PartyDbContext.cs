using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PartyInviter.Entities;

namespace PartyInviter.Infra
{
    public class PartyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PartyDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Party> Parties { get; set; }
        public DbSet<Guest> Guests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
        }
    }
}