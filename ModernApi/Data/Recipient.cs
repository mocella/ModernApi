namespace ModernApi.Data
{
    public class Recipient : Entity
    {
        public long          RecipientId     { get; set; }
        public long          MessageId       { get; set; }
        public long          IdentityId      { get; set; }
        public RecipientType RecipientTypeId { get; set; }

        public Message       Message         { get; set; }
        public Identity      Identity        { get; set; }
    }
}
