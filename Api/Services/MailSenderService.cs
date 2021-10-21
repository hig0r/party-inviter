using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PartyInviter.Entities;
using PartyInviter.Infra;

namespace PartyInviter.Services
{
    public class MailSenderService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MailSenderService> _logger;
        private readonly HashService _hashService;
        private readonly IConfiguration _configuration;
        private readonly Channel<int> _queue;
        private readonly SmtpClient _smtpClient;

        public MailSenderService(IServiceScopeFactory scopeFactory, ILogger<MailSenderService> logger, HashService hashService, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _hashService = hashService;
            _configuration = configuration;
            var options = new BoundedChannelOptions(500)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<int>(options);
            // TODO: replace smtpclient with mailkit since smtpclient is now obsolete.
            _smtpClient = new SmtpClient
            {
                Host = _configuration.GetValue<string>("Smtp:Host"),
                Port = _configuration.GetValue<int>("Smtp:Port"),
                EnableSsl = _configuration.GetValue<bool>("Smtp:EnableSsl"),
                Credentials = new NetworkCredential(
                    _configuration.GetValue<string>("Smtp:Login"), 
                    _configuration.GetValue<string>("Smtp:Password"))
            };
        }

        public ValueTask AddGuestToMailQueue(Guest guest)
        {
            return _queue.Writer.WriteAsync(guest.Id);
        }

        public async Task AddGuestsToMailQueue(IEnumerable<Guest> guests)
        {
            foreach (var guest in guests)
            {
                await AddGuestToMailQueue(guest);
            }
        }

        public async Task SendNextEmail(CancellationToken ct)
        {
            var guestId = await _queue.Reader.ReadAsync(ct);
            _logger.LogDebug($"Sending email to guest {guestId}");
            using var scope = _scopeFactory.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetRequiredService<PartyDbContext>();
            var guest = await dbContext.Guests.Include(x => x.Party).FirstOrDefaultAsync(x => x.Id == guestId, ct);
            if (guest is null) return;
            var link = $"localhost:4200/i/{_hashService.GenerateHash(guest.Id)}";
            await _smtpClient.SendMailAsync(new MailMessage
            {
                From = new MailAddress(_configuration.GetValue<string>("Smtp:Login")),
                Subject = $"Convite: {guest.Party.Name}",
                Body = $"Você recebeu um convite para o(a) {guest.Party.Name}.Acesse {link} para visualizar o convite!",
                To = { guest.Email }
            }, ct);
            guest.MessageSent = true;
            dbContext.Update(guest);
            await dbContext.SaveChangesAsync();
        }
    }
}