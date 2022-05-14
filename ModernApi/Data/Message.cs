namespace ModernApi.Data;

public class Message : Entity
{
    public long MessageId { get; set; }
    public Guid MessageGuid { get; set; }
    public long SenderId { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public List<Recipient>? Recipients { get; set; }
    public Identity? Sender { get; set; }
}