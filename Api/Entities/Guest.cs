namespace PartyInviter.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool MessageSent { get; set; }
        public GuestWillAttend? WillAttend { get; set; }
        public int PartyId { get; set; }
        public Party Party { get; set; }
    }

    public enum GuestWillAttend
    {
        Yes,
        Maybe
    }
}