namespace ModernApi.Data;

public class Identity : Entity
{
    public long IdentityId { get; set; }
    public string EmailAddress { get; set; }
    public List<Message> Messages { get; set; }
    public List<Recipient> Recipients { get; set; }
}