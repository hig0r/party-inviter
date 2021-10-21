using Microsoft.AspNetCore.Mvc;

namespace PartyInviter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}