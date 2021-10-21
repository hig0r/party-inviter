using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyInviter.Dtos;
using PartyInviter.Entities;
using PartyInviter.Infra;
using PartyInviter.Services;

namespace PartyInviter.Controllers
{
    public class PartiesController : ApiController
    {
        private readonly PartyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MailSenderService _mailSenderService;

        public PartiesController(PartyDbContext dbContext, IMapper mapper, MailSenderService mailSenderService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mailSenderService = mailSenderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ps = await _dbContext.Parties.ProjectToType<GetPartiesDto>().ToListAsync();
            return Ok(ps);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _dbContext.Parties.Include(x => x.Guests).FirstOrDefaultAsync(x => x.Id == id);
            if (p is null)
                return NotFound();
            return Ok(_mapper.From(p).AdaptToType<GetPartyDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewPartyDto dto)
        {
            var p = dto.Adapt<Party>();
            _dbContext.Add(p);
            await _dbContext.SaveChangesAsync();
            await _mailSenderService.AddGuestsToMailQueue(p.Guests);
            return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
        }
    }
}