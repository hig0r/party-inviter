using System.Collections.Generic;

namespace PartyInviter.Dtos
{
    public class NewPartyDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string InvitationMessage { get; set; }
        public ICollection<NewGuestDto> Guests { get; set; }
    }

    public class NewGuestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}