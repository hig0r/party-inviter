using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PartyInviter.BackgroundServices;
using PartyInviter.Entities;
using PartyInviter.Infra;
using PartyInviter.Services;

namespace PartyInviter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(
                    config =>
                    {
                        config.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        config.SerializerSettings.Converters.Add(new StringEnumConverter
                        {
                            NamingStrategy = new KebabCaseNamingStrategy()
                        });
                        config.SerializerSettings.ContractResolver = new DefaultContractResolver
                        {
                            IgnoreIsSpecifiedMembers = true,
                            NamingStrategy = new CamelCaseNamingStrategy { ProcessDictionaryKeys = true }
                        };
                    });

            services.AddDbContext<PartyDbContext>();
            // TODO: Create interface to test easily later
            services.AddSingleton<HashService>();
            services.AddSingleton<MailSenderService>();
            services.AddHostedService<MailSenderBackgroundService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PartyInviter", Version = "v1" });
            });

            services.RegisterMappings();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PartyInviter v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            var sender = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<MailSenderService>();
            sender.AddGuestToMailQueue(new Guest { Id = 26 }).GetAwaiter().GetResult();
        }
    }
}