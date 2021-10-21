using System.Collections.Generic;
using PartyInviter.Entities;

namespace PartyInviter.Dtos
{
    public class GetPartyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InvitationMessage { get; set; }
        public ICollection<GuestDto> Guests { get; set; }
    }

    public class GuestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool MessageSent { get; set; }
        public GuestWillAttend? WillAttend { get; set; }
        public string Link { get; set; }
    }
}