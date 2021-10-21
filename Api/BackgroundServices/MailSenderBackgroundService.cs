using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PartyInviter.Services;

namespace PartyInviter.BackgroundServices
{
    public class MailSenderBackgroundService : BackgroundService
    {
        private readonly MailSenderService _mailSenderService;

        public MailSenderBackgroundService(MailSenderService mailSenderService)
        {
            _mailSenderService = mailSenderService;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _mailSenderService.SendNextEmail(stoppingToken);
            }
        }
    }
}