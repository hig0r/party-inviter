using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using PartyInviter.Dtos;
using PartyInviter.Entities;
using PartyInviter.Services;

namespace PartyInviter
{
    public static class RegisterMappingsBuilder
    {
        public static void RegisterMappings(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Guest, GuestDto>()
                .Map(dest => dest.Link,
                    src => $"localhost:4200/i/{MapContext.Current.GetService<HashService>().GenerateHash(src.Id)}");

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}