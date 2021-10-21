using PartyInviter.Entities;

namespace PartyInviter.Dtos
{
    public class AnswerInvitationDto
    {
        public string HashId { get; set; }
        public GuestWillAttend WillAttend { get; set; }
    }
}