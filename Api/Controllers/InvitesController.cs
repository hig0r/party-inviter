using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyInviter.Dtos;
using PartyInviter.Infra;
using PartyInviter.Services;

namespace PartyInviter.Controllers
{
    public class InvitesController : ApiController
    {
        private readonly PartyDbContext _dbContext;
        private readonly HashService _hashService;

        public InvitesController(HashService hashService, PartyDbContext dbContext)
        {
            _hashService = hashService;
            _dbContext = dbContext;
        }

        [HttpGet("{hashId}")]
        public async Task<IActionResult> Get(string hashId)
        {
            var guestId = _hashService.DecodeHash(hashId);
            var invitation =
                await _dbContext
                    .Guests
                    .Where(x => x.Id == guestId)
                    .Select(x => new {x.Party.InvitationMessage, AlreadyAnswered = x.WillAttend != null})
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            if (invitation is null)
                return NotFound();
            return Ok(invitation);
        }

        public async Task<IActionResult> Post(AnswerInvitationDto dto)
        {
            var guestId = _hashService.DecodeHash(dto.HashId);
            var guest = await _dbContext.Guests.FirstOrDefaultAsync(x => x.Id == guestId);
            guest.WillAttend = dto.WillAttend;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}