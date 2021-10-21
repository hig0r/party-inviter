using HashidsNet;
using Microsoft.Extensions.Configuration;

namespace PartyInviter.Services
{
    public class HashService
    {
        private readonly Hashids _hashids;

        public HashService(IConfiguration configuration)
        {
            _hashids = new Hashids(configuration.GetValue<string>("Salt"), 16);
        }

        public string GenerateHash(int id)
        {
            return _hashids.Encode(id);
        }

        public int DecodeHash(string hash)
        {
            return _hashids.Decode(hash)[0];
        }
    }
}