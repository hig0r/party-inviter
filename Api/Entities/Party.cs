using System.Collections.Generic;

namespace PartyInviter.Entities
{
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InvitationMessage { get; set; }
        public ICollection<Guest> Guests { get; set; }
    }
}